
![Architecture](https://user-images.githubusercontent.com/20567991/204552288-b02dffde-fa2f-4ac6-ab05-5c197f880ac5.PNG)


Library-microservices

1 - Only Rabbit-MQ
  - docker-compose up on root
  - http://localhost:15672/  
  - username: guest / password: guest

2 - Build Server generates everything for prod environment

3 - Build Server Dev generates everything for development environment

4 - Clean Server erases images and containers from docker
