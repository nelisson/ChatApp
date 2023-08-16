# README: StockChat - Chat Application with Stock Information


## Overview
Welcome to StockChat! This unique chat application allows users to communicate in real-time and retrieve stock quotes directly within the chatroom. Designed with a focus on back-end technologies using .NET, this application provides an intuitive user experience and efficient real-time data access. Join the community, chat with others, and stay updated on your favorite stock quotes.

## Features

### Real-time Chatroom
- Chat with multiple users in a real-time chatroom. Share ideas, ask questions, and engage in conversations.
- Messages are ordered by timestamps, and the chatroom displays only the last 50 messages, ensuring a clean and focused conversation.

### Stock Quote Commands
- Use the command `/stock=stock_code` within the chatroom to request stock quotes.
- A decoupled bot retrieves stock quotes from the Stooq API and sends a message back to the chatroom with the quote information.
- Receive stock quote messages in the following format: "APPL.US quote is $93.42 per share." The bot will be credited as the post owner.

### User Authentication
- StockChat employs .NET Identity for user authentication, offering a secure login experience for all users.

### Error Handling
- The chatbot can handle messages that are not understood or any exceptions raised during its operation, ensuring a smooth user experience.

## Preparing

Getting Started
Prerequisites
.NET 7 SDK

Setting Up

1. Clone the repository to your local machine
```console
git clone https://github.com/nelisson/ChatApp.git
cd ChatApp
```
2. Restore packages
```console
dotnet restore
```
3. Apply database migrations
```console
cd .\ChatApp\Server\
dotnet ef database update
```
4.Run the project
```console
cd .\ChatApp\Server\
dotnet run
```
Open a web browser and navigate to http://localhost:7234

Running Tests

```console
cd .\ChatApp.Test
dotnet test
```

## How to Use

1. Open two or more browser windows and navigate to the StockChat application.
2. Register or log in with different user credentials in each window.
3. Start chatting in the chatroom and use the `/stock=stock_code` command to request stock quotes.
4. The stock quote will be delivered as a chat message from the bot.

## Security and Performance
- StockChat prioritizes security and performance, ensuring confidential information is kept secure and that the chat doesn't consume excessive resources.

Join StockChat today and stay informed, connected, and engaged!

## Author
Nélisson Cavalheiro
