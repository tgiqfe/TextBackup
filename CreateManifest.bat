@echo off
pushd %~dp0

rem # Ver. 0.01.001

rem # Debug��Manifest.exe�̃^�C���X�^���v���擾
set execDebug=Manifest\bin\Debug\Manifest.exe
for %%a in (%execDebug%) do set timestampD=%%~ta
echo Debug Manifest.exe : %timestampD%

rem # Release��Manifest.exe�̃^�C���X�^���v���擾
set execRelease=Manifest\bin\Release\Manifest.exe
for %%a in (%execRelease%) do set timestampR=%%~ta
echo Release Manifest.exe : %timestampR%

rem # Debug/Release�ŐV�����ق���exe�����s
if "%timestampR%" GEQ "%timestampD%" (
	%execRelease%
) else (
	%execDebug%
)

pause
