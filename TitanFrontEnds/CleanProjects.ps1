Get-ChildItem $PSScriptRoot -Include bin -Recurse -Force | Remove-Item -Force -Recurse
Get-ChildItem $PSScriptRoot -Include obj -Recurse -Force | Remove-Item -Force -Recurse