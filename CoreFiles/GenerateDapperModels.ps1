param (
    [string]$outputDir,
    [string]$connectionString,
    [string]$namespace = "APICoreTemplate.Core.Data"
)

if (-not $outputDir -or -not $connectionString) {
    Write-Host "Please provide both outputDir and connectionString."
    exit
}
else{
    if (!(Test-Path -Path $outputDir -PathType Container)) {
            New-Item -ItemType Directory -Force -Path $outputDir
        }
}


# Load SqlClient Assembly
#Add-Type -AssemblyName "System.Data.SqlClient"

function Get-CSharpType ([string]$sqlType) {
    switch ($sqlType) {
        "bigint" { return "long" }
        "smallint" { return "short" }
        "int" { return "int" }
        "uniqueidentifier" { return "Guid" }
        "smalldatetime" { return "DateTime" }
        "datetime" { return "DateTime" }
        "datetime2" { return "DateTime" }
        "date" { return "DateTime" }
        "time" { return "TimeSpan" }
        "float" { return "double" }
        "real" { return "float" }
        "numeric" { return "decimal" }
        "decimal" { return "decimal" }
        "money" { return "decimal" }
        "smallmoney" { return "decimal" }
        "bit" { return "bool" }
        "tinyint" { return "byte" }
        "image" { return "byte[]" }
        "binary" { return "byte[]" }
        "varbinary" { return "byte[]" }
        "timestamp" { return "byte[]" }
        "nvarchar" { return "string" }
        "varchar" { return "string" }
        "text" { return "string" }
        "ntext" { return "string" }
        "xml" { return "string" }
        "char" { return "string" }
        "nchar" { return "string" }
        default { return "object" }
    }
}

# Connect to the database
$connection = New-Object System.Data.SqlClient.SqlConnection
$connection.ConnectionString = $connectionString
$connection.Open()

# Fetch tables
$tables = $connection.GetSchema('Tables') | Where-Object { $_.TABLE_TYPE -eq 'BASE TABLE' }

foreach ($table in $tables) {
    $tableName = $table.TABLE_NAME
    $className = $tableName -replace " ", "_"
    
    # Start building the class
    $classDef = @()
	$classDef += "using System;"
	$classDef += "using System.ComponentModel.DataAnnotations.Schema;"
	$classDef += ""
    $classDef += "namespace $namespace.Models"
    $classDef += "{"
	$classDef += "    [Table("$tableName")]"
    $classDef += "    public class $className : IEntity"
    $classDef += "    {"

    # Fetch columns for the table
    $query = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '$tableName'"
    $command = $connection.CreateCommand()
    $command.CommandText = $query
    $reader = $command.ExecuteReader()

    while ($reader.Read()) {
        $columnName = $reader["COLUMN_NAME"]
        $dataType = Get-CSharpType $reader["DATA_TYPE"]
        $classDef += "        public $dataType $columnName { get; set; }"
    }

    $reader.Close()
    $classDef += "    }"
    $classDef += "}"
    
    # Save the class to a file
    $classDef -join [Environment]::NewLine | Out-File -Encoding UTF8 "$outputDir\$className.cs"
}

$connection.Close()