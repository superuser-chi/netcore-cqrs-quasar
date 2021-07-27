# Setup

1. Resttore the project using `dotnet restore`

1. Perform migrations using `dotnet-ef migrations add -s API -p Persistence`

1. CD into API folder and do the following:

   - Initialize secrets using `dotnet user-secrets init`
   - Add initial password using `dotnet user-screts set SeedUserPW <YOUR_ PASSWORD>`
   - Seed user user data by running `dotnet run /seed`
