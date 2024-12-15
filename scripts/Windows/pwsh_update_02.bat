# Sprawdź aktualizacje programu PowerShell za pomocą winget
$updateInfo = winget upgrade --id Microsoft.Powershell

# Sprawdź, czy są dostępne aktualizacje
if ($updateInfo.Status -eq "Available") {
    # Dostępne aktualizacje, ale nie są automatycznie instalowane
    Write-Host "Dostępne są aktualizacje programu PowerShell, ale nie są one automatycznie instalowane."
    Write-Host "Aby zaktualizować ręcznie, uruchom 'winget upgrade --id Microsoft.Powershell'."
} else {
    # Brak dostępnych aktualizacji
    Write-Host "Brak dostępnych aktualizacji dla programu PowerShell."
}