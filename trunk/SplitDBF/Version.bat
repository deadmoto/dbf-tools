echo namespace SplitDBF > Version.cs && ^
echo { >> Version.cs && ^
echo     static class Version >> Version.cs && ^
echo     { >> Version.cs && ^
for /f %%i in ('svnversion') do echo         public static string Value = "%%i"; >> Version.cs && ^
echo     } >> Version.cs && ^
echo } >> Version.cs