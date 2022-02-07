#get certificate from keyvault as we are running under a server build.
$pfxSecretStringValue=$env:SDLPLCAuthenticode

$thumbprint = $env:CurrentThumbprint
$certUnprotectedBytes = [Convert]::FromBase64String($pfxSecretStringValue)


Add-Type -AssemblyName System.Security
$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2
$cert.Import($certUnprotectedBytes, $null, [System.Security.Cryptography.X509Certificates.X509KeyStorageFlags]"UserKeySet,PersistKeySet")


write-output "Adding certificate to user store"
$store = new-object System.security.cryptography.X509Certificates.X509Store -argumentlist "My", "CurrentUser"
$store.Open([System.Security.Cryptography.X509Certificates.OpenFlags]"ReadWrite")

$store.Add($cert)
$store.Close()


write-output "Check certificate"
$c=Get-ChildItem Cert:\CurrentUser\My\$thumbprint
write-output $c
Test-certificate $c -policy AUTHENTICODE