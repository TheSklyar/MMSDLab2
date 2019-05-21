@ECHO OFF
set server=%1
set database=%2

if "%server%" EQU "" goto error
if "%database%" EQU "" goto error

echo start install %server%, %database% 
echo === 

echo ===/Server/DB/createdb.sql===
sqlcmd -S %server% -C -i ../Server/DB/createdb.sql

echo ===/Server/Tables/dbo.tRevisionLog.sql===
sqlcmd -S %server% -d %database% -v dbname=%database%  -C -i ../Server/Tables/dbo.tRevisionLog.sql

echo ===/Server/Procedures/dbo.Init.sql===
sqlcmd -S %server% -d %database% -v dbname=%database%  -C -i ../Server/Procedures/dbo.Init.sql

echo ===/Server/Scripts/InitialScript.sql===
sqlcmd -S %server% -d %database% -v dbname=%database%  -C -i ../Server/Scripts/InitialScript.sql

echo ===
echo stop install %server%, %database%
exit /b
:error
ECHO Please, use run.cmd
:exit
