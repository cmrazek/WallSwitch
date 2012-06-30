; -------------------------------------------------------------------------------------------------
; WallSwitch Installer Script
;
; This script uses the LockedList plugin: http://nsis.sourceforge.net/LockedList_plug-in
; -------------------------------------------------------------------------------------------------

Name "WallSwitch"
OutFile "Output\WallSwitch_V1.0.4_Setup.exe"
InstallDir "$PROGRAMFILES\WallSwitch"
RequestExecutionLevel admin

Var AppName
Var AppNameIdent
Var AppVersion

Section "-Initialize"
	StrCpy $AppName      "WallSwitch"
	StrCpy $AppNameIdent "WallSwitch"
	StrCpy $AppVersion   "1.0.4"
SectionEnd

; -------------------------------------------------------------------------------------------------
; Installer
; -------------------------------------------------------------------------------------------------

;Page license
Page components
Page directory
Page Custom LockedCheck
Page instfiles

;LicenseData "license.txt"
;LicenseForceSelection checkbox

; Check for .NET Framework 4.0
Section "-CheckDotNet"
	StrCpy $0 0
LoopEnum:
	EnumRegKey $1 HKLM "SOFTWARE\Microsoft\.NETFramework" $0
	StrCmp $1 "" DotNetNotFound
	StrCmp $1 "v4.0.30319" ExitCheckDotNet
	IntOp $0 $0 + 1
	Goto LoopEnum
DotNetNotFound:
	MessageBox MB_YESNO "This application requires Microsoft.NET 4.0 framework in order to function. Do you want to be taken to the Microsoft.NET website to download the framework?" /SD IDNO IDYES GoMicrosoft
	Quit
GoMicrosoft:
	ExecShell "open" "http://www.microsoft.com/download/en/details.aspx?id=17718"
ExitCheckDotNet:
SectionEnd

; Check for locked files
Function LockedCheck
	LockedList::AddModule "$INSTDIR\WallSwitch.exe"
	LockedList::AddFile "$INSTDIR\WallSwitch.ico"
	LockedList::AddModule "$INSTDIR\HtmlAgilityPack.dll"
	LockedList::AddFile "$INSTDIR\HtmlAgilityPack.xml"
	LockedList::AddFile "$INSTDIR\ReadMe.txt"
	LockedList::AddModule "$INSTDIR\HtmlAgilityPack.dll"
	LockedList::AddModule "$INSTDIR\Uninstall.exe"
	LockedList::Dialog /autonext
	Pop $R0
FunctionEnd

; Install main application
Section "Application (Required)"
	SectionIn RO
	SetOutPath "$INSTDIR"
	File "bin\WallSwitch.exe"
	File "bin\WallSwitch.ico"
	File "bin\HtmlAgilityPack.dll"
	File "bin\HtmlAgilityPack.xml"
	File "bin\ReadMe.txt"
	WriteUninstaller "$INSTDIR\Uninstall.exe"
SectionEnd

; Install start menu shortcuts
Section "Start Menu Shortcuts"
	CreateDirectory "$SMPROGRAMS\WallSwitch"
	CreateShortCut "$SMPROGRAMS\WallSwitch\WallSwitch.lnk" "$INSTDIR\WallSwitch.exe" "" "$INSTDIR\WallSwitch.exe" 0
	CreateShortCut "$SMPROGRAMS\WallSwitch\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
SectionEnd

; Ask the user if they want to start the app now
Section "-StartNow"
	MessageBox MB_YESNO "Do you want to run WallSwitch now?" /SD IDYES IDNO NoStartNow
	Exec "$INSTDIR\WallSwitch.exe"
NoStartNow:
SectionEnd

; Add to the add/remove programs control panel
Section "-AddRemovePrograms"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WallSwitch" "DisplayName"     "WallSwitch"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WallSwitch" "DisplayVersion"  "$AppVersion"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WallSwitch" "DisplayIcon"     "$INSTDIR\WallSwitch.ico"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WallSwitch" "InstallLocation" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WallSwitch" "UninstallPath"   "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WallSwitch" "UninstallString" "$INSTDIR\Uninstall.exe"
SectionEnd

; -------------------------------------------------------------------------------------------------
; Uninstaller
; -------------------------------------------------------------------------------------------------

UninstPage uninstConfirm
UninstPage components
UninstPage Custom un.LockedCheck
UninstPage instfiles

Function un.LockedCheck
	LockedList::AddModule "$INSTDIR\WallSwitch.exe"
	LockedList::AddFile "$INSTDIR\WallSwitch.ico"
	LockedList::AddModule "$INSTDIR\HtmlAgilityPack.dll"
	LockedList::AddFile "$INSTDIR\HtmlAgilityPack.xml"
	LockedList::AddFile "$INSTDIR\ReadMe.txt"
	LockedList::AddModule "$INSTDIR\HtmlAgilityPack.dll"
	LockedList::AddModule "$INSTDIR\Uninstall.exe"
	LockedList::Dialog /autonext
	Pop $R0
FunctionEnd

Section "Uninstall"
	; Remove installed files
	Delete "$INSTDIR\WallSwitch.exe"
	Delete "$INSTDIR\HtmlAgilityPack.dll"
	Delete "$INSTDIR\HtmlAgilityPack.xml"
	Delete "$INSTDIR\Uninstall.exe"
	RMDir "$INSTDIR"
	
	; Remove start menu shortcuts
	Delete "$SMPROGRAMS\*.*"
	RMDir "$SMPROGRAMS\WallSwitch"
	
	; Remove from add/remove programs control panel
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\WallSwitch"
SectionEnd

Section /o "un.Remove user settings"
	RMDir /REBOOTOK "$APPDATA\WallSwitch"
SectionEnd
