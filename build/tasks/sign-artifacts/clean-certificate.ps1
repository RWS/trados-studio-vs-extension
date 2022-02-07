
write-output "Cleaning up"
Get-ChildItem Cert:\CurrentUser\My\$env:CurrentThumbprint | Remove-Item

