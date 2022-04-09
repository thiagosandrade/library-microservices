#Stop server Applications
docker-compose down

#wait for server completes 
Start-Sleep 20

#Changing to Library Application
cd Library

#Stop Library Docker
docker-compose down


#Back to the original location
cd..