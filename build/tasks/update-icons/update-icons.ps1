
#copy a new .ico file over all the found files.

#get full path of "$PSScriptRoot\..\..\TemplatesVSIX\"



$targetFolder = "$PSScriptRoot\..\..\..\TemplatesVSIX\"
$targetFolder = [System.IO.Path]::GetFullPath($targetFolder)
# find all .ico files in $targetFolder and sub folders

$iconList = Get-ChildItem -Path $targetFolder -Filter "SDL*.ico" -Recurse

$srcIcon="$PSScriptRoot\..\..\..\..\newicon.ico"

if (Test-Path $srcIcon)
{
    #copy a new .ico file over all the found files.
    foreach ($icon in $iconList)
    {
        Copy-Item -Path $srcIcon -Destination $icon.FullName -Force
    }
} else
{
    Write-Host "Source icon file not found: $srcIcon"
    exit 1
}

