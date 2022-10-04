# mono-financialbot
## Table of contents
* [Description](#description)
* [Features](#features)
* [Technologies Used](#technologies)
* [Installation guide](#installationguide)
* [Setup](#setup)
* [Usage](#usage)
---
## Description
A simple browser-based chat application using .NET. 
This application allows several users to talk in a chatroom and also to get stock quotes from an API using a specific command.
---
## Features
* Allow registered users to log in and talk with other users in a chatroom.
* Allow users to post messages as commands into the chatroom with the following format **/stock=stock_code**.
* Decoupled bot that calls an API using the stock_code as a parameter (https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here `aapl.us` is the stock_code).
* The bot parses the received CSV file and then send a message back into the chatroom using RabbitMQ. The message is a stock quote
with the following format: “APPL.US quote is $93.42 per share”. The post owner of the message is the bot.
*  Chat messages ordered by their timestamps. When a user gets connected to the chatroom the last 50 messages are displayed.
* Use of .NET Identity for authentication.
* Messages that are not understood or any exceptions raised within the bot are handled.
* Logs requests and trace possible exceptions
---
## Technologies
The project is created with or uses:

* Angular 13.3.11
* .NET Core 6
* RabbitMQ
* SignalR
* Bootstrap
---
## Installation Guide
* Visual Studio 2019 or greater than 
* Docker Desktop **for rabbit deployment**
* Instance of RabbitMQ
---
### Setup Angular
1. Go to this path inside the project **mono-financialbot-front-end-ts\ClientApp** and run on the terminal  **npm i**
### Run RabbitMQ Instance on Docker
1. Open Powershell or Bash and run the next command to start the RabbitMQ Docker image as a container. It's important that you keep this Powershell or Bash window open while running the application.
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```


2. Now you can run the application in the following URL: http://localhost:4200/.

---
## Usage
Run application, and register as an usar, then login into the app to acces a chat room.
