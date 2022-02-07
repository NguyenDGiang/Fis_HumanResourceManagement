dotnet ef dbcontext scaffold  "data source=127.0.0.1\\LOCAL_SQL_SERVER,62192;initial catalog=HRM;persist security info=True;user id=sa2;password=123456;multipleactiveresultsets=True;" Microsoft.EntityFrameworkCore.SqlServer -c DataContext  -o Models -f --no-build --use-database-names --json
$content = Get-Content -Path 'Models\DataContext.cs' -Encoding UTF8
$content = $content -replace "using System;", "using System;using Thinktecture;"
$content = $content -replace "modelBuilder.Entity<ActionDAO>", "modelBuilder.ConfigureTempTable<long>();modelBuilder.ConfigureTempTable<Guid>();modelBuilder.Entity<ActionDAO>"
$content | Set-Content -Path "Models\DataContext.cs"  -Encoding UTF8