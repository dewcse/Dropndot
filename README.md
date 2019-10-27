                                                 Dropndot (User Login Portal)

1.	This project builds on ASP.NET CORE 2.2 Version. So, run this project from VS 2017 or 2019 or any IDE which have capability to run
    a .NET Core 2.2 version project. Also, you can download .NET Core SDK from bellow link
    (https://dotnet.microsoft.com/download/dotnet-core/2.2) for install in your machine. 

2.	After opening the project, change “Server” name with your machine “Server” name from ConnectionStrings in appsettings.json for 
    stabilize a connection with your database.

3.	As this project created by using EntityFramework Code First Approach so, after opening project run following command from 
    Package Manager Console to create database at your Server.
   
     Update-Database

4.	Also configure your mailServer with replacing existing on from EmailSettings in appsettings.json.

5.	If you face any problem regards sending mail from your gmail account after replacing  SenderName, Sender & Password from 
    EmailSettings in appsettings.json  then “Allow less secure apps: ON” from given link (https://myaccount.google.com/lesssecureapps)



