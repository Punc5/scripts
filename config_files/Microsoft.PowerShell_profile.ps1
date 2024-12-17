## PowerShell 7.4.6 Config

# Check for Internet Connection (GitHub Connectivity Check with 1 second Timeout)
$global:canConnectToGitHub = Test-Connection github.com -Count 1 -Quiet -TimeoutSeconds 1

if (-not $global:canConnectToGitHub) {
    Write-Host "There is NO an Internet Connection! Unable to connect to GitHub" -ForegroundColor Red
}

# Information on Startup
# Write-Host "Hello there $($env:USERNAME)! Wanna play a game?" -ForegroundColor Red
# Write-Host ""

# Function - Check pwsh Updates
function Check-PwshUpdate {
    Write-Host "Checking for PowerShell updates..." -ForegroundColor Cyan

    # Update pwsh with Winget
    $updateCheck = winget upgrade --id Microsoft.Powershell --source winget | Out-String

    if ($updateCheck -notmatch "^Microsoft\.PowerShell\s") {
        Write-Host "No PowerShell updates available." -ForegroundColor Green
    } else {
        Write-Host "Downloading PowerShell update..." -ForegroundColor Yellow
        try {
            winget upgrade --id Microsoft.Powershell --source winget --accept-source-agreements --accept-package-agreements
            Write-Host "PowerShell has been updated." -ForegroundColor Green
        } catch {
            Write-Host "An error occurred during PowerShell update." -ForegroundColor Red
        }
    }
}

# Exec Function
Check-PwshUpdate

# Import Modules and External Profiles
oh-my-posh init pwsh --config 'C:\Users\kacpe\AppData\Local\Programs\oh-my-posh\themes\uew.omp.json' | Invoke-Expression

Import-Module -Name Terminal-Icons
Import-Module -Name Microsoft.WinGet.CommandNotFound

$ChocolateyProfile = "$env:ChocolateyInstall\helpers\chocolateyProfile.psm1"
if (Test-Path($ChocolateyProfile)) {
  Import-Module "$ChocolateyProfile"
}

# winfetch
# f45873b3-b655-43a6-b217-97c00aa0db58 PowerToys CommandNotFound module
# f45873b3-b655-43a6-b217-97c00aa0db58

# Custom Aliases & Functions

Set-Alias np "C:\Users\kacpe\Pulpit\Notepad++\notepad++.exe"
# Set-Alias startupapps "S:\Scripts\startup_apps.bat"
Set-Alias -Name winfr -Value winfr.exe
# Set-Alias -Name nv -Value "C:\Program Files\Neovim\bin\nvim.exe"

Set-Alias pwshupdate "S:\Scripts\pwsh_update.bat"
# Set-Alias sftpmain "sftpmain_function"
Set-Alias sftpmain "S:\Scripts\sftpmain.bat"
Set-Alias wingetall "S:\Scripts\winget_upgrade_all.bat"
Set-Alias ssmcs2update "S:\Scripts\ss_moments_counter-strike_2_change.bat"
Set-Alias sscs2update "S:\Scripts\screenshots_counter-strike_2_change.bat"
Set-Alias sspioupdate "S:\Scripts\screenshots_paper.io_2_and_3d_change.bat"
Set-Alias sssioupdate "S:\Scripts\screenshots_slither.io_change.bat"

Set-Alias grep "Select-String"

# SFTP Server Connection

# function sftpmain_function {
#     & "S:\Scripts\sftpmain.bat"
# }

# Find File
function ff {
    param (
        [string]$name,
        [string]$path
    )

    Get-ChildItem -Path $path -Recurse -Filter "*$name*" -ErrorAction SilentlyContinue | ForEach-Object {
        Write-Output "$($_.FullName)"
    }
}

# Find Directory
function fd {
    param (
        [string]$name,
        [string]$path
    )

    Get-ChildItem -Path $path -Recurse -Filter "*$name*" -Directory -ErrorAction SilentlyContinue | ForEach-Object {
        Write-Output "$($_.FullName)"
    }
}

# Enhanced Listing
function la { 
    Get-ChildItem -Path . -Force | Format-Table -AutoSize 
}
function ll { 
    Get-ChildItem -Path . -Force -Hidden | Format-Table -AutoSize 
}

# Network Utilities
function Get-PubIP { 
    (Invoke-WebRequest http://ifconfig.me/ip).Content 
}

# Open WinUtil full-release
function winutil {
	irm https://christitus.com/win | iex
}

# Open WinUtil pre-release
function winutildev {
	irm https://christitus.com/windev | iex
}

# Edit pwsh Profile & Other
function ep {
    explorer $PROFILE
}

# Refresh Profile
function reload {
    . $PROFILE
}

# Git Shortcuts
function gs { 
    git status 
}

function ga { 
    git add . 
}

function gp { 
    git push 
}

function clipboard {
    Get-Clipboard
}

function setclipboard {
    Set-Clipboard
}

# Enhanced pwsh Experience
Set-PSReadLineOption -Colors @{
    Command = "Yellow"
    Parameter = "Green"
    Operator = "#FFB6C1" # LightPink (pastel)
    Variable = "#DDA0DD" # Plum (pastel)
    String = "#FFDAB9"  # PeachPuff (pastel)
    Number = "#B0E0E6"  # PowderBlue (pastel)
    Type = "#F0E68C"    # Khaki (pastel)
    Comment = "#D3D3D3" # LightGray (pastel)
    Keyword = "#8367C7" # Violet (pastel)
    Error = "Red"
}
