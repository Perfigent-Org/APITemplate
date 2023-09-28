param (
    [string]$modelsDir,
    [string]$outputRepositoriesDir,
    [string]$namespace = "APICoreTemplate.Core.Data"
)
if (-not $modelsDir -or -not $outputRepositoriesDir) {
    Write-Host "Please provide both modelsDir and outputRepositoriesDir."
    exit
}
else{
    if (!(Test-Path -Path $outputRepositoriesDir -PathType Container)) {
            New-Item -ItemType Directory -Force -Path $outputRepositoriesDir
        }
}

$models = Get-ChildItem -Path $modelsDir -Filter *.cs

foreach ($model in $models) {
    $modelContent = Get-Content $model.FullName
    if ($modelContent -match 'public class (\w+) : IEntity' -or $modelContent -match 'public class (\w+)\s*:\s*IEntity') {
        $modelName = $matches[1]

        # Skip history models
        if ($modelName -match 'History') {
            continue
        }

        # Find corresponding history model
        $historyModelName = "${modelName}History"
        if ($models.Name -notcontains "$historyModelName.cs") {
            $historyModelName = $null
        }

        # Generate Interface
        $interfaceContent = @"
using $namespace.Models;
using $namespace.Models.History;

namespace $namespace.Repository.Interfaces
{
    public interface I${modelName}Repository : IRepositoryBase<$modelName>, IHistoryRepositoryBase<$historyModelName>
    {
    }
}
"@
        $outputInterfacesDir = "$outputRepositoriesDir\Interfaces"

        if (!(Test-Path -Path $outputInterfacesDir -PathType Container)) {
            New-Item -ItemType Directory -Force -Path $outputInterfacesDir
        }
        $interfaceContent | Out-File -FilePath "$outputInterfacesDir\I${modelName}Repository.cs"

        # Generate Repository
        $repositoryContent = @"
using $namespace.Models;
using $namespace.Models.History;
using $namespace.Repository.Interfaces;
using System.Data;

namespace $namespace.Repository
{
    public class ${modelName}Repository : RepositoryBase<$modelName>, I${modelName}Repository
    {
        private readonly IDbConnection _connection;
        public IHistoryRepository<$historyModelName> History { get; }

        public ${modelName}Repository(IDbConnection connection)
            : base(connection)
        {
            _connection = connection;
            History = new HistoryRepository<$historyModelName>(connection);
        }
    }
}
"@
        $repositoryContent | Out-File -FilePath "$outputRepositoriesDir\${modelName}Repository.cs"
    }
}
