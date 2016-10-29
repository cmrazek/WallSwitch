# GetShellRatings.ps1
#
# This script will extract the Windows metadata ratings from all files in a folder
# and create a CSV file that can be used to import your ratings into WallSwitch via
# The Import Ratings button in Tools -> Settings, Images tab.

param(
[Parameter(Mandatory=$true)][string]$folder,
[Parameter(Mandatory=$true)][string]$csvFileName
)

function Scan-Folder()
{
    $shell = New-Object -ComObject Shell.Application

    $files = Get-ChildItem -Path $folder -Recurse
    foreach ($file in $files)
    {
        if (![System.IO.File]::Exists($file.FullName)) { continue; }

        $folderPath = [System.IO.Path]::GetDirectoryName($file.FullName)
        $shellFolder = $shell.Namespace($folderPath)
        $shellFile = $shellFolder.Items().Item($file.Name)

        for ($i = 0; $i -le 312; $i++)
        {
            $name = $shellFolder.GetDetailsOf($file.Name, $i)
            if ($name -eq "Rating")
            {
                $detail = $shellFolder.GetDetailsOf($shellFile, $i)
                if ($detail -match "^\s*(\d)\s+Stars\s*$")
                {
                    [int]$rating = $Matches[1]
                    if ($rating -ge 1 -and $rating -le 5)
                    {
                        $ret = New-Object PSObject
                        $ret | Add-Member "Path" $file.FullName
                        $ret | Add-Member "Rating" $rating
                        Write-Output $ret
                        break;
                    }
                }
            }
        }
    }
}

Scan-Folder | Export-Csv $csvFileName -NoTypeInformation
