#Titan Client
cd C:\actions-runner-FrontEnd\_work\TitanFrontEnds\TitanFrontEnds\Titan.Client\bin\Release\net6.0
#I dont want any app settings overwritning my config - these should not be in the repo to begin with
remove-item .\appsettings*.*
Copy-Item C:\actions-runner-FrontEnd\_work\TitanFrontEnds\TitanFrontEnds\Titan.Client\bin\Release\net6.0\*  C:\inetpub\wwwroot\TitanFrontEnds\Titan.Client\ -Recurse -Force


#Titan Administration
cd C:\actions-runner-FrontEnd\_work\TitanFrontEnds\TitanFrontEnds\Titan.Administration\bin\Release\net6.0
#I dont want any app settings overwritning my config - these should not be in the repo to begin with
remove-item .\appsettings*.*
Copy-Item C:\actions-runner-FrontEnd\_work\TitanFrontEnds\TitanFrontEnds\Titan.Administration\bin\Release\net6.0\*  C:\inetpub\wwwroot\TitanFrontEnds\Titan.Administration\ -Recurse -Force

#Titan Stock
cd C:\actions-runner-FrontEnd\_work\TitanFrontEnds\TitanFrontEnds\Titan.Stock\bin\Release\net6.0
#I dont want any app settings overwritning my config - these should not be in the repo to begin with
remove-item .\appsettings*.*
Copy-Item C:\actions-runner-FrontEnd\_work\TitanFrontEnds\TitanFrontEnds\Titan.Stock\bin\Release\net6.0\*  C:\inetpub\wwwroot\TitanFrontEnds\Titan.Stock\ -Recurse -Force

