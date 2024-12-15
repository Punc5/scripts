@echo off
cd S:\Android Phone from a PC\platform-tools
adb tcpip 5555
scrcpy --tcpip=192.168.0.4