@echo off

REM Define the Umango service name
SET UMANGO_SERVICE="UmangoService"

REM Get the directory of the current script
set SCRIPT_DIR=%~dp0

REM Define source and destination paths for the file
SET SOURCE_FILE=%SCRIPT_DIR%..\bin\Debug\Umango.Addin.Export.OpenWeatherApi.dll
SET DESTINATION_FOLDER="C:\Program Files\Umango\23\Addins\Export\OpenWeather"

REM Check if the Umango service is running
sc query %UMANGO_SERVICE% | find "STATE" | find /i "RUNNING"
if errorlevel 1 (
    echo Umango service is not running. Proceeding to copy the file.
) else (
    echo Stopping Umango service...
    net stop %UMANGO_SERVICE%
    :CHECK_SERVICE_STOP
    sc query %UMANGO_SERVICE% | find "STATE" | find /i "STOPPED"
    if errorlevel 1 (
        echo Waiting for the Umango service to stop...
        timeout /t 1 /nobreak
        goto CHECK_SERVICE_STOP
    ) else (
        echo Service stopped. Waiting 1 second before proceeding...
        timeout /t 1 /nobreak
    )
)

REM Attempt to copy the file with retries
SET RETRY_COUNT=0
:TRY_COPY
echo Attempting to copy file from %SOURCE_FILE% to %DESTINATION_FOLDER%...
copy %SOURCE_FILE% %DESTINATION_FOLDER%
if %ERRORLEVEL% NEQ 0 (
    SET /A RETRY_COUNT+=1
    if %RETRY_COUNT% LEQ 5 (
        echo Failed to copy file, it might be in use. Retrying in 2 seconds...
        timeout /t 2 /nobreak
        goto TRY_COPY
    ) else (
        echo Failed to copy file after several attempts. Please check if the file is in use.
        goto END
    )
)

echo File copied successfully.

:RESTART_SERVICE
REM Start the Umango service if it was stopped earlier
echo Starting Umango service...
net start %UMANGO_SERVICE%

:END
echo Script execution completed.
pause
