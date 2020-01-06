dotnet user-secrets init
dotnet user-secrets set ConnectionStrings.BiodZebraContext ""

dotnet tool restore
dotnet-ef dbcontext scaffold name=BiodZebraContext Microsoft.EntityFrameworkCore.SqlServer -o ./Data/Models -c BiodZebraContext -f