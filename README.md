# DossierManagement

## Prerequisiti
Accesso ad un server database MSSQL

## Setup
Rinominare il file `DossierManagement/DossierManagement.Api/appsettings.Example.json` in appsettings.Development.json  

Settare la chiave `MasterConnection` con la connection string per il database master con un utente con diritti di creazione di database come 
`server=localhost; database=master;User ID=xxx;Password=yyy`  

Allo stesso modo settare la chiave `Sql` con la connection string per il database DossierManagement come `server=localhost; database=DossierManagement;User ID=xxx;Password=yyy`
