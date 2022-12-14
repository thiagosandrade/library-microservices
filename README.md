
![Architecture]![Architecture](https://user-images.githubusercontent.com/20567991/207581356-24bc25b2-eccb-4ad8-b709-f4df3e5fa3a4.PNG)



Library-microservices

1 - Only Basic Services
  - docker-compose up on root
  - http://localhost:5601/   (Kibana) 
  - http://localhost:15672/  (RabbitMq)
    - username: guest / password: guest

2 - Build Server generates everything for prod environment

3 - Build Server Dev generates everything for development environment

4 - Clean Server erases images and containers from docker
