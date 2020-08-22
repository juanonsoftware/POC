$scriptToWork = "$PsScriptRoot\Script1.ps1"
$params = @(
)

$logFile = "C:\Logs\log 12.txt";

&$scriptToWork -id "ID_ONE" -name "Name IS LIKE THIS" > $logFile