param(
    [string]$OutputDirectory = ".\backups"
)

$hostName = if ($env:QLKH_DB_HOST) { $env:QLKH_DB_HOST } else { "localhost" }
$port = if ($env:QLKH_DB_PORT) { $env:QLKH_DB_PORT } else { "5432" }
$database = if ($env:QLKH_DB_NAME) { $env:QLKH_DB_NAME } else { "quanlyhanghoa" }
$username = if ($env:QLKH_DB_USER) { $env:QLKH_DB_USER } else { "postgres" }
$password = if ($env:QLKH_DB_PASSWORD) { $env:QLKH_DB_PASSWORD } else { "1234" }

if (-not (Get-Command pg_dump -ErrorAction SilentlyContinue)) {
    throw "Khong tim thay pg_dump. Hay cai PostgreSQL client tools va them vao PATH."
}

New-Item -ItemType Directory -Force -Path $OutputDirectory | Out-Null

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$backupFile = Join-Path $OutputDirectory "$database`_$timestamp.backup"

$env:PGPASSWORD = $password
try {
    pg_dump `
        --host $hostName `
        --port $port `
        --username $username `
        --format custom `
        --blobs `
        --verbose `
        --file $backupFile `
        $database
}
finally {
    Remove-Item Env:\PGPASSWORD -ErrorAction SilentlyContinue
}

Write-Host "Da backup database vao: $backupFile"
