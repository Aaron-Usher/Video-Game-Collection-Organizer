@echo off
 
rem Batch file to run a script to create the database.

rem Runs the script.
sqlcmd -S localhost -E -i "VideoGameDatabase.sql" 

@ECHO if no error messages appear DB was created

PAUSE
