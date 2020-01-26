#Build server Applications
docker-compose down
docker-compose up -d --build --force-recreate

#wait for server completes 
Start-Sleep 20

#Changing to Library Application
cd Library

#Build Library Docker
docker-compose down
docker-compose up -d --build --force-recreate  

#Back to the original location
cd..