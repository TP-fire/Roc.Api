@ECHO OFF

taskkill /im Single.exe /f

ping -n 2 127.1 >nul
