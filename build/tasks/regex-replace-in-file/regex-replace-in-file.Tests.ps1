
$DebugPreference = 'Continue'

Describe "regex-replace-in-file" {

    $testPath = "TestDrive:\test.txt"

    It "no inputfile" {
        {& ".\regex-replace-in-file.ps1"} | Should -Throw 
    }

    It 'no replace' {
        Set-Content $testPath -value "this is a token to be replaced."
        & ".\regex-replace-in-file.ps1" "$testPath" "dave" "smurf"
        $result = Get-Content $testPath
        $result | Should -be "this is a token to be replaced."
    }

    It 'simple replace' {
        Set-Content $testPath -value "this is a token to be replaced."
        & ".\regex-replace-in-file.ps1" "$testPath" "token" "smurf"
        $result = Get-Content $testPath
        $result | Should -be "this is a smurf to be replaced."
    }

    It 'simple regex replace' {
        Set-Content $testPath -value "this is a token to be replaced."
        & ".\regex-replace-in-file.ps1" "$testPath" "t[ao]ken" "smurf"
        $result = Get-Content $testPath
        $result | Should -be "this is a smurf to be replaced."
    }

    It 'variable regex replace' {
        Copy-Item "branch-variables.yml" -Destination $testPath -force
        & ".\regex-replace-in-file.ps1" "$testPath" "(?sm)name: VersionGroup.*value: '(.*Versions[^']*)'" "smurf" 1
        $result = Get-Content $testPath
        $CorrectResult = Get-Content "branch-variables-good.yml"
        $result | Should -be $CorrectResult
    }
}
