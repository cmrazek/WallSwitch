copy ..\WallSwitch\bin\Release\WallSwitch.exe .
copy ..\WallSwitch\Resources\AppIcon.ico WallSwitch.ico
copy ..\WallSwitch\bin\Release\ChangeLog.txt .
makensis installer.nsi
