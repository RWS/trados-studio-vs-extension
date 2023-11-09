param(
        [string]$filename,
        [string]$regex,
        [string]$value,
        [int]$matchIndex=0,
        [int]$codepage=0
        )

$DebugPreference = 'Continue'

#dot source function for getting the file encoding.
. "$psscriptroot\..\get-fileencoding\Get-FileEncoding.ps1"
        
if (Test-Path $filename)
{
    if (0 -eq $codepage)
    {
        $FileEncoding = Get-FileEncoding -Path $filename
    }
    else
    {
        [System.Text.Encoding]$FileEncoding = [System.Text.Encoding]::GetEncoding($codepage)
    }

    Write-Debug "regex-replace-in-file::filename='$filename'  regex='$regex' value='$value' matchIndex='$matchIndex' Encoding='$($FileEncoding.BodyName)'"

    $FileContents=get-content -path $filename -Raw

    $FileContents -match $regex

    if ($matches.count -gt 0)
    {
        $Replacer=$matches[$matchIndex]
   
        $Replacer = $Replacer -replace "\[", "\["
        $Replacer = $Replacer -replace "\]", "\]"
        $Replacer = $Replacer -replace "\$", "\$"
    
        $FileEncoding
        $Replacer
        $value

        $newContents = $FileContents -replace "$Replacer", "$value"
        set-content -path "$filename" -value $newContents -Encoding $FileEncoding -NoNewline

        return $true
    }
    else
    {
        return $false
    }
}
else
{
    $m= "regex-replace-in-file::cannot find filename=$filename  regex=$regex value=$value matchIndex=$matchIndex"
    throw $m
    return $false
}
