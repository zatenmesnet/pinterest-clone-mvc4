A pinterest clone written in C# & MVC4, using the dapper.NET ORM made popular by StackOverflow.  Posts get stored on S3.

Note:  You'll need to add in your own AuthConfig.cs file to the App\_Start directory.  This is not included on SVN for privacy reasons (facebook and s3 tokens).

Install:<br>
- Download SQL Express<br>
- Create table "TT"<br>
- Run Sql.sql<br>
- Easiest way to run both the REST service and MVC app is to open the project twice in VS, and unload the REST app in one of them.  You'll probably need to replace the port numbers in the project with whatever your local system picks.