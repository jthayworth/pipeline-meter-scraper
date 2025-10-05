# Pipeline Meter Scraper

This is a c# project that will scrape information from various different pipeline meter listings, transform the returned data into a generic model, and insert into database. This implementation is assuming a postgresql database.

## Setup

The following setup is required to run this program locally:

1. [Dotnet 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. [VS Code](https://code.visualstudio.com/)
3. [C# VS Code Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
4. [.NET Install Tool VS Code Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)

## Job Settings

Each job requires the following environment variables to be set. These variables would either go in a `yaml` file if you are running this in a docker container for a cronjob or a `.env` file for running locally.

#### Generic Variables

| Variable Name   | Description                                                                                    |
| --------------- | ---------------------------------------------------------------------------------------------- |
| `DB_CONNECTION` | Base connection string for the database                                                        |
| `DB_USERNAME`   | The username for authenticating into the database                                              |
| `DB_PASSWORD`   | The password for authenticating into the database                                              |
| `CHROME_PATH`   | The path to the chrome app/exe. IN chrome go to chrome://version to find the `Executable Path` |

#### Job Specific Variables

| Variable Name         | Description                                                   |
| --------------------- | ------------------------------------------------------------- |
| `JOB_TYPE`            | The type of job to run. This points to a specific runner      |
| `JOB_RESOURCE_URL`    | The URL of the page where a download button can be clicked    |
| `JOB_DOWNLOADED_NAME` | The filename with extension (`.csv`)                          |
| `JOB_RETRIES`         | The number of times you want to try the job before failing    |
| `JOB_RETRY_INTERVAL`  | The amount of time in seconds to wait in between each attempt |

## How This Works

### Runners

Each site with a unique UI will require a runner to be created. See [KinderMorganRunner](https://github.com/jthayworth/pipeline-meter-scraper/blob/main/src/Scraper/Implementation/KinderMorganRunner.cs) for an example.

### Models

Each runner will require it's own model with annotations for the CSV parser. See [KinderMorganRaw.cs](https://github.com/jthayworth/pipeline-meter-scraper/blob/main/src/Scraper/Models/KinderMorganRaw.cs). You can name the properties of the model however you want, but you will need to annotate them.

| Annotation | Example                           | Description                                                                            |
| ---------- | --------------------------------- | -------------------------------------------------------------------------------------- |
| Name       | `[Name("NAME_OF_COLUMN_IN_CSV")]` | This tells the CSV parser which column in the data to map to the property in the class |
| Ignore     | `[Ignore]`                        | This tells the CSV parser to ignore the property when mapping the data to the class    |

### TableItem Model

This model probably isn't fully stubbed out, I just did the properties I thought were important, feel free to add more if you need them. Within this class, there should be a method for each runner model that takes an item of that type and fills out the `TableItem` class with the values from that type. See [TableItem.cs](https://github.com/jthayworth/pipeline-meter-scraper/blob/main/src/Scraper/Models/TableItem.cs) for an example.

### Dependency Injection

Once a runner and model have been created, you need to add the runner to the dependency injection setup in the `ConfigureServices` section of [Program.cs](https://github.com/jthayworth/pipeline-meter-scraper/blob/main/src/Scraper/Program.cs) so it can be used.

```c#
  services.AddKeyedScoped<IRunner, YOUR_RUNNER>("YOUR_RUNNER_NAME");
```

> [!TIP]
> The name of the service supplied in the dependency injection is the value that will be used for the `JOB_TYPE` environment variable
> For example, the KinderMorganRunner name is "kinder-morgan".
