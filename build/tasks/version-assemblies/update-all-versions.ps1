param(
[string]$VersionString="17.2.0"
)

$VSIXFile="$PSScriptRoot\..\..\..\TemplatesVsix\source.extension.vsixmanifest"

$VSIXFile=[System.IO.Path]::GetFullPath($VSIXFile);

$VSIXID="6D87597D-63A9-4A1C-BB39-A970AD731ED1"

write-output "Updating version:"
write-output "`$VSIXFile=$VSIXFile"
write-output "`$VersionString=$VersionString"
write-output "`$VSIXID=$VSIXID"



& "$PSScriptRoot\..\regex-replace-in-file\regex-replace-in-file.ps1" "$VSIXFile" "Id=`"$VSIXID`" Version=`".*`" L" "Id=`"$VSIXID`" Version=`"$VersionString`" L" 0 65001


