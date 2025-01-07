Instructions:

1. Go to this link: https://github.com/jayceedillan/BlogApp via your browser and download the repository,
  or open your terminal and type:
  git clone https://github.com/jayceedillan/BlogApp

2. Navigate to the folder and open the solution file: BlogApp.sln

3. Open the appsettings.json file under BlogApp.Web, and locate the "ConnectionStrings" section:

	"ConnectionStrings": {
		"blogDb": "Server=LAPTOP-UOOAD2TP\\SQLEXPRESS01;Database=blogDb;User Id=admin;Password=Test@12345!;TrustServerCertificate=True;"
	}
	Update the User Id and Password to match your local machine's database credentials.

4. Open the Package Manager Console and execute the following commands:
	a. dotnet ef migrations add initial --project BlogApp.Infrastructure
	b. dotnet ef database update --project BlogApp.Infrastructure

5. Run the application.

6. Use the following credentials to log in:
	Username: admin@example.com
	Password: AdminPassword123!