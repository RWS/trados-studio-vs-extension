$DebugPreference = 'Continue'

. "$psscriptroot\get-fileencoding.ps1"

$enc = Get-FileEncoding ("C:\code\TradosStudio3\TradosStudio\src\Sdl\AutoCorrect\VersionInfo\Sdl.TranslationStudio.AutoCorrect.Info.cs")

$enc
