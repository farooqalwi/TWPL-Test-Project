# TWPL Test Project
PROJECT DETAILS

Description

ASP .Net C# 
Upload data from an excel sheet on a web page, on button click bulk upload to the table in an sql database, show it into a Grid-view and provide basic add / edit / delete / list functionality.

Steps

.Make a Project in ASP .Net using C# in Visual Studio
.On Page Load,
       .check if Database with name Fleet is available in db, if not then create db.
       .check if Table with name Vehicle available in db, if not then create table with following columns
            .VehicleID  bigint  PK  Auto-Increment
            .RegNo   varchar(50)
            .Make     varchar(50)
            .Model    varchar(50)
            .Color     varchar(50)
            .EngineNo    varchar(50)
            .ChasisNo    varchar(50)
            .DateOfPurchase    datetime
            .Active   bit

Note: Creation of DB and table should be done by code not manually

       .If records are available in the table then show them in Grid-View with following options
            .Sorting
            .Editing
            .Deletion
            .Search box (search data in all columns and highlight searched records)
            .Add new record option
            .Upload from excel  file - show proper error message with details
        if unsuccessful (file not found, file format error etc.)
            .Get an excel file from user (browse function)
            .Fill records in separate grid,
            .On press Upload Button,
            .Bulk Insertion will be done,
            .After successful operation, refresh Grid-View
       .Export to Excel option (through a button)
       .Proper Error Handling must be done so system does not through unhandled exceptions
       .Web page should be Responsive
       .Please submit the following so we can compile and check it's functionality as well as error handling capabilities
            .Document if needed to run OR any Description you want to share
            .Source Code
            .DB Script (which will be a part of your source code)

Evaluation Criterion
1. Complete functionality (45)
2. Error handling (15)
3. Project Documentation (05)
4. User interface (how logical and easy to use can you produce in this short time) (20)
5. Coding standards  (15)
