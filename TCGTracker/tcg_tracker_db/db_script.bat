echo off

rem batch file to run sql script with sqlexpress

sqlcmd -S localhost -E -i create_db.sql
sqlcmd -S localhost -E -i create_stored_procedures.sql
sqlcmd -S localhost -E -i add_values.sql
rem sqlcmd -S 127.0.0.1 -E -i %1

echo.
echo if no error message appear, the db was created
echo but you should check for yourself
pause