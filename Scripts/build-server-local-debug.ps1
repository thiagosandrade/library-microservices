#Root folder from application
cd..

#Build server Applications
docker-compose down
docker-compose up -d --build --force-recreate  

#wait for server completes 
Start-Sleep 10

#Build Dapr Docker to run locally
cd Library

# 'dapr init'

start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
      --app-id library-shop `
	  --config ./configuration/config.yaml `
	  --components-path ./components `
      --app-port 19581 `
	  --metrics-port 9091 `
	  --dapr-grpc-port 50001 `
	  --dapr-http-port 3501 `
      --log-level debug `
	  '
	  
Start-Sleep -Seconds 2

start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
      --app-id library-book `
	  --config ./configuration/config.yaml `
	  --components-path ./components `
      --app-port 19582 `
	  --metrics-port 9092 `
	  --dapr-grpc-port 50002 `
	  --dapr-http-port 3502 `
	  --log-level debug `
  '
	  
Start-Sleep -Seconds 2


start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
	  --app-id library-hub `
	  --config ./configuration/config.yaml `
	  --components-path ./components `
      --app-port 19583 `
	  --metrics-port 9093 `
	  --dapr-grpc-port 50003 `
	  --dapr-http-port 3503 `
	  --log-level debug `
	  '
	  
Start-Sleep -Seconds 2

start-process powershell.exe -argument `
  '-nologo
    -noprofile
    -executionpolicy bypass
    -command dapr run `
      --app-id library-auth `
	  --config ./configuration/config.yaml `
	  --components-path ./components `
      --app-port 19584 `
	  --metrics-port 9094 `
	  --dapr-grpc-port 50004 `
	  --dapr-http-port 3504 `
      --log-level debug `
  '
	  
Start-Sleep -Seconds 2

cd ../Scripts

