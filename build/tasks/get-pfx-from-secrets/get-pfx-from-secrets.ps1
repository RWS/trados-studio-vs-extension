#copy the password and pfx from azure keyvault variables.
param(
[string]$fileToSign = "$PSScriptRoot\signme.exe"
)

$pfxPath = "$psscriptroot\..\..\..\tools\SDLPLCAuthenticode.pfx"

if ($null -eq $env:TF_BUILD)
{
    #if we are running locally then we can generate a pfx.
    if (!(test-path $pfxPath))
    {
        $cert = New-SelfSignedCertificate -Subject "SDL Internal Codesigning" -Type CodeSigningCert -CertStoreLocation cert:\LocalMachine\My
        #Move-Item -Path $cert.PSPath -Destination "Cert:\CurrentUser\Root"

        $CertPassword = ConvertTo-SecureString -String "unset" -Force -AsPlainText

        Export-PfxCertificate -Cert $cert -FilePath "$pfxPath" -Password $CertPassword

        write-host "Created $pfxPath"

        $env:SDLPLCAuthenticodeKey = "unset"
    }
}
else
{
    #get certificate from keyvault as we are running under a server build.
    $pfxSecretStringValue=$env:SDLPLCAuthenticode
    Write-Host "pfxSecretStringValue=$pfxSecretStringValue"

    $kvSecretBytes = [System.Convert]::FromBase64String($pfxSecretStringValue)
    $certCollection = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2Collection
    $certCollection.Import($kvSecretBytes, $null, [System.Security.Cryptography.X509Certificates.X509KeyStorageFlags]::Exportable)


    $password = $env:SDLPLCAuthenticodeKey
    $protectedCertificateBytes = $certCollection.Export([System.Security.Cryptography.X509Certificates.X509ContentType]::Pkcs12, $password)
    
    [System.IO.File]::WriteAllBytes($pfxPath, $protectedCertificateBytes)
    write-host "Extracted $pfxPath from Azure keyvault"


}
$ToolPath=[System.IO.Path]::GetFullPath("C:\Users\VssAdministrator\.nuget\packages\Microsoft.VSSDK.Vsixsigntool\16.2.29116.78\tools\vssdk\vsixsigntool.exe");

& $ToolPath sign /v /f $pfxPath  /p $password /fd sha1 $fileToSign  /sha1 "77 5d 05 e4"
