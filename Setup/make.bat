copy ..\WallSwitch\bin\Release\WallSwitch.exe bin\
copy ..\WallSwitch\bin\Release\HtmlAgilityPack.dll bin\
copy ..\WallSwitch\bin\Release\HtmlAgilityPack.xml bin\
copy ..\WallSwitch\Resources\AppIcon.ico bin\WallSwitch.ico
copy ..\ReadMe.txt bin\

makensis installer.nsi
