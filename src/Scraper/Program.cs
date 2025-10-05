using dotenv.net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scraper.Utils;
using Scraper.Contracts;
using Scraper.Implementation;
using Scraper.Models;

// load env vars from .env file
DotEnv.Load();

// set up host with config and dependency injection
var host = Host.CreateDefaultBuilder()
  .ConfigureAppConfiguration(config =>
  {
    config.SetBasePath(AppContext.BaseDirectory);
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
  })
  .ConfigureServices((hostContext, services) =>
  {
    // gets the db connection info so we can inject it into the Sql service
    var dbUsername = Environment.GetEnvironmentVariable("db_username") ?? string.Empty;
    var dbPassword = Environment.GetEnvironmentVariable("db_password") ?? string.Empty;
    var baseConnection = hostContext.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

    services.AddSingleton<DbSettings>(new DbSettings
    {
      BaseConnection = baseConnection,
      Username = dbUsername.Replace("\"", "\"\""),
      Password = dbPassword.Replace("\"", "\"\"")
    });

    services.AddTransient<ISql, Sql>();

    // register the different runners here.
    services.AddKeyedScoped<IRunner, KinderMorganRunner>("kinder-morgan");
  })
  .Build();

try
{
  var jobSettings = new JobSettings();

  // get the runner base on the job type from env vars
  var runner = host.Services.GetKeyedService<IRunner>(jobSettings.Type);
  if (runner == null) throw new Exception("Runner of type not found");

  var isSuccess = false;
  var attempts = 0;

  Logger.Log($"Starting job: {jobSettings.Type}");


  // attempt to run the job, retrying if it fails up to the max retries
  while (!isSuccess && attempts < jobSettings.Retries)
  {
    try
    {
      await runner.Run(jobSettings.ResourceUrl, jobSettings.DownloadedName);
      isSuccess = true;
    }
    catch (System.Exception ex)
    {
      Logger.Log($"Attempt {attempts + 1} failed: {ex.Message}");
      attempts++;
      // job failed, wait to retry per the retry interval based on env var
      await Task.Delay(jobSettings.RetryInterval * 1000);
    }
  }

  if (isSuccess)
  {
    Logger.Log("Job completed successfully.");
  }
  else
  {
    Logger.Log("Job failed after maximum retries.");
  }
}
catch (Exception ex)
{
  Logger.Log(ex);
}


