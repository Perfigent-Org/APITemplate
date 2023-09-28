param (
    [string]$outputDir,
    [string]$connectionString,
    [string]$namespace = "APICoreTemplate.Core.Data",
)

if (-not $outputDir -or -not $connectionString) {
    Write-Host "Please provide both outputDir and connectionString."
    exit
}
else{
    if (!(Test-Path -Path $outputDir -PathType Container)) {
            New-Item -ItemType Directory -Force -Path $outputDir
        }

    if (!(Test-Path -Path "$outputDir\History" -PathType Container)) {
            New-Item -ItemType Directory -Force -Path "$outputDir\History"
        }
}

# Load SqlClient Assembly
# Add-Type -AssemblyName "System.Data.SqlClient"

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
    $tableSchema = $table.TABLE_SCHEMA

    # Skip history tables in the main loop
    if ($tableSchema -eq "History") {
        continue
    }

    $className = $tableName -replace " ", "_"

    # Generate the main model
    Generate-Model -TableSchema $tableSchema -TableName $tableName -ClassName $className

    # If there's a corresponding history table, generate that model too
    if ($tables | Where-Object { $_.TABLE_SCHEMA -eq "History" -and $_.TABLE_NAME -eq $tableName }) {
        $historyClassName = "${tableName}History"
        Generate-History-Model -TableSchema "History" -TableName $tableName -BaseClassName $className -ClassName $historyClassName
    }
}

function Generate-Model ([string]$TableSchema, [string]$TableName, [string]$ClassName) {
    $classDef = @()
    $classDef += "using System;"
	$classDef += "using System.ComponentModel.DataAnnotations.Schema;"
	$classDef += ""
    $classDef += "namespace $namespace.Models"
    $classDef += "{"
	$classDef += "    [Table("$TableName", Schema = "$TableSchema")]"
    $classDef += "    public class $ClassName : IEntity"
    $classDef += "    {"

    # Fetch columns for the table
    $query = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '$TableSchema' AND TABLE_NAME = '$TableName'"
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
    $classDef -join [Environment]::NewLine | Out-File -Encoding UTF8 "$outputDir\$ClassName.cs"
}

function Generate-History-Model ([string]$TableSchema, [string]$TableName, [string]$BaseClassName [string]$ClassName) {
    $classDef = @()
    $classDef += "using System;"
	$classDef += "using System.ComponentModel.DataAnnotations.Schema;"
	$classDef += ""
    $classDef += "namespace $namespace.Models.History"
    $classDef += "{"
	$classDef += "    [Table("$TableName", Schema = "$TableSchema")]"
    $classDef += "    public class $ClassName : $BaseClassName, IHistoryEntity"
    $classDef += "    {"
    $classDef += "        public DateTime SysStartTime { get; set; }"
    $classDef += "        public DateTime SysEndTime { get; set; }"
    $classDef += "    }"
    $classDef += "}"

    # Save the class to a file
    $classDef -join [Environment]::NewLine | Out-File -Encoding UTF8 "$outputDir\History\$ClassName.cs"
}

$connection.Close()
