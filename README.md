# UtgKata
 
The solution:

To build a console app to read in a CSV file from a directory. The contents will then need to be sent to a REST API endpoint, in JSON format, and saved to a SQL database.

CSV file will contain the following columns:
 - Customer Ref
 - Customer Name
 - Address Line 1
 - Address Line 2
 - Town
 - County
 - Country
 - Postcode

Loop through the rows of the CSV file and send each row to a REST API POST endpoint, in JSON format.

The REST API should then save the content to a database table. Format can match the CSV file.
Create a REST API GET endpoint to retrieve the customer information, passing in a customer ref to filter the data.

Contents should be returned in JSON format

---

## Overview of solution

 - **Used .NET Core 3.1**

 - **UtgKata.Lib.Tests:** XUnit tests for UtgKata.Lib project (test CSV reader etc)
 - **UtgKata.Api:** Web API project
 - **UtgKata.Api.Tests:** XUnit tests for API project (test controllers etc)
 - **UtgKata.Console:** Console application which reads CSV file and posts through to API endpoint
 - **UtgKata.Data:** Entity Framework set up, repository and entites 
 - **UtgKata.Lib:** CSV reader 
 
There are 2 startup projects (Web API and Console). The console app will wait 5 seconds before attempting to read the CSV file and post each record via the API in an attempt to let the API project spin up. Polly is also used to retry the API endpoint in case the Web API takes longer than expected to load.

---

## Database

The MS SQL database is called `utgkata` and the connection string is `Data Source=localhost;Initial Catalog=utgkata;Integrated Security=True`. EF Core has been used to interact with the database.

When the console starts up, Entity Framework will try and create the database if it does not exist:

```
// Check the database is created
var optsBuilder = new DbContextOptionsBuilder<UtgKataDbContext>();
optsBuilder.UseSqlServer(DbContextSettings.ConnectionString);

using (var ctx = new UtgKataDbContext(optsBuilder.Options))
{
    await ctx.Database.EnsureCreatedAsync();
}
```

The `IDesignTimeDbContextFactory` has been used so that EF migrations did not have to be run on the startup project, and could instead be used by a .NET standard project.

---

## CSV Reader and Mapping

It is assumed the CSV file will always have a header, and that data starts on the second row.

A CSV reader has been written in a way so that a columns are mapped to properties of a model. A class which inherits from `CsvModelReaderBase` can have properties with
the `[CsvHeaderMapping]` attribute (the name of the column in the CSV file).

```
public class CustomerRecordModel : CsvReaderModelBase
{
    [CsvHeadingMapper(HeadingName = "customer_ref)]
    public string CustomerRef { get; set; }

    [CsvHeadingMapper(HeadingName = "first_name")]
    public string FirstName { get; set; }

    [CsvHeadingMapper(HeadingName = "last_name")]
    public string LastName { get; set; }
}
```

The CSV row mapper is responsible for assigning the data from the CSV file to the correct property on the model, and this is achieved by using reflection. 

```
// Initialise the CSV mapper and the CSV reader
var csvMapper = new CsvRowMapper<CustomerRecordModel>();
var csvParser = new CsvReader<CustomerRecordModel>(csvMapper, @"c:/temp/data.csv");

// Read the CSV file and return the mapped models
var csvModels = await csvParser.ReadCsvAsync();
var models = csvModels.ToList();
```

---

## The API response object

The response from the API will typically look like:

```
{
   "hasError":false,
   "errorDetails":null,
   "response":{...}
}
```

The **hasError** property is a boolean to tell you if there is an error or not. If there is, the **errorDetails** should tell you about the error. The object return is in the **response** property.

Successful example:

```
{
   "hasError":false,
   "errorDetails":null,
   "response":{
      "id":79,
      "createdAt":"2021-01-18T20:32:05.45",
      "customerRef":"A1",
      "firstName":"John",
      "lastName":"Smith",
      "address1":"1 Chapel Lane",
      "address2":"Coppull",
      "town":"Chorley",
      "county":"Lancashire",
      "country":"UK",
      "postCode":"PR7 1AB"
   }
}
```

Error example:

```
{
  "hasError": true,
  "errorDetails": {
    "relatedId": 8020,
    "relatedType": "UtgKata.Data.Models.Customer",
    "errorMessage": "The requested entity with an id of 8020 was not found"
  },
  "response": null
}
```
This is rendered using the `GeneralResponseViewResultFilterAttribute` result filter and is added to the controllers at startup so it used globally.