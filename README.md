This program is designed to work with (ticker_persister)[https://github.com/se3125/ticker_persister]. It reads the persisted data from that program and logs it to the console. 


Prerequisites:
  * The program reads data from a SQLite database. You must have Sqlite running locally, with a database matching the DB_STRING constant 
    (this is set to `@"Data Source=C:\Users\saebert\Documents\db\tickerdata.db"` in the code, but should be updated with your local db location).
  * An environment running .NET 5 to compile and run the application


Running the application:
  * From the application directory, first build with: `dotnet build`
  * To run the application, run: `dotnet run TickerReader.cs <NUMBER OF RECORDS TO RETRIEVE>`; for example: `dotnet run TickerReader.cs 25`. Omitting the argument will read a default of 10 elements.
