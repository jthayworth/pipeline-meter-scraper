# Pipeline Meter Scraper

This is a c# project that will scrape information from various different pipeline meter listings, transform the returned data into a generic model, and insert into database. This implementation is assuming a postgresql database.

## Setup

The following setup is required to run this program locally:

1. [Dotnet 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. [VS Code](https://code.visualstudio.com/)

## Job Settings

Each job requires the following environment variables to be set.

#### Generic Variables

| Variable Name | Description                                                                                                       |
| ------------- | ----------------------------------------------------------------------------------------------------------------- |
| `DB_USERNAME` | The username for authenticating into the database                                                                 |
| `DB_PASSWORD` | The password for authenticating into the database                                                                 |
| `CHROME_PATH` | The path to the chrome app/exe. You can navigate [here](chrome://version) in Chrome to find the `Executable Path` |

#### Job Specific Variables

| Variable Name         | Description                                                   |
| --------------------- | ------------------------------------------------------------- |
| `JOB_TYPE`            | The type of job to run. This points to a specific runner      |
| `JOB_RESOURCE_URL`    | The URL of the page where a download button can be clicked    |
| `JOB_DOWNLOADED_NAME` | The filename with extension (`.csv`)                          |
| `JOB_RETRIES`         | The number of times you want to try the job before failing    |
| `JOB_RETRY_INTERVAL`  | The amount of time in seconds to wait in between each attempt |
