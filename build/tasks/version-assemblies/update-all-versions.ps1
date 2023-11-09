param(
[string]$VersionString="17.2.0"
)

$VSIXFile="$PSScriptRoot\..\..\..\TemplatesVsix\source.extension.vsixmanifest"

$VSIXFile=[System.IO.Path]::GetFullPath($VSIXFile);

write-output "Updating version:"
write-output "`$VSIXFile=$VSIXFile"
write-output "`$VersionString=$VersionString"



& "$PSScriptRoot\..\regex-replace-in-file\regex-replace-in-file.ps1" "$VSIXFile" "Id=`"3D390539-4A5C-4A0E-AAF8-2BCD8EA83FF1`" Version=`".*`" L" "Id=`"3D390539-4A5C-4A0E-AAF8-2BCD8EA83FF1`" Version=`"$VersionString`" L" 0 65001


