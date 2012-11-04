@echo off
setlocal enableextensions enabledelayedexpansion

if exist "C:\Program Files\NSIS\makensis.exe" (
	set makensis="C:\Program Files\NSIS\makensis.exe"
) else if exist "C:\Program Files (x86)\NSIS\makensis.exe" (
	set makensis="C:\Program Files (x86)\NSIS\makensis.exe"
) else (
	echo Error: makensis.exe could not be found
	goto :eof
)

if not exist bin mkdir bin
if not exist output mkdir output

copy ..\WallSwitch\bin\Release\WallSwitch.exe bin\
if errorlevel 1 (
	echo Failed to copy WallSwitch.exe
	goto :eof
)

copy ..\WallSwitch\bin\Release\HtmlAgilityPack.dll bin\
if errorlevel 1 (
	echo Failed to copy HtmlAgilityPack.dll
	goto :eof
)

copy ..\WallSwitch\bin\Release\HtmlAgilityPack.xml bin\
if errorlevel 1 (
	echo Failed to copy HtmlAgilityPack.xml
	goto :eof
)

copy ..\WallSwitch\Resources\AppIcon.ico bin\WallSwitch.ico
if errorlevel 1 (
	echo Failed to copy AppIcon.ico
	goto :eof
)

copy ..\ReadMe.txt bin\
if errorlevel 1 (
	echo Failed to copy ReadMe.txt
	goto :eof
)

copy ..\Release\WallSwitchImgProc.dll bin\
if errorlevel 1 (
	echo Failed to copy WallSwitchImgProc.dll
	goto :eof
)

%makensis% Installer.nsi
if errorlevel 1 (
	echo MakeNSIS Failed.
	goto :eof
)
