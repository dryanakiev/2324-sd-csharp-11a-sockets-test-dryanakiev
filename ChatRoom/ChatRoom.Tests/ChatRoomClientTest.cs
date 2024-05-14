using NUnit.Framework;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ChatRoom.Server;
using ChatRoom.Client;

namespace ChatRoom.Tests
{
    public class ChatRoomClientTest
    {
        private ServerSocket server;
        private Thread serverThread;

        [SetUp]
        public void Setup()
        {
            server = new ServerSocket();
            serverThread = new Thread(new ThreadStart(server.StartServer));
            serverThread.Start();
            Thread.Sleep(1000); // Allow server to start before running tests
        }

        [TearDown]
        public void Teardown()
        {
            server.StopServer();
        }

        // TODO: Change the greeting message from the server on established connection
        // Changed greeting message
        [Test]
        public void TestGreetingMessage()
        {
            // Arrange
            ClientSocket client = new ClientSocket();
            client.StartConnection();

            // Act
            string receivedMessage = client.ReceiveMessage();
            client.CloseConnection();

            // Assert
            Assert.That(receivedMessage, Is.EqualTo("Hello! Welcome to our chat server!"));
        }

        // TODO: Implement a server reply to the client
        // Changed server reply message
        [Test]
        public void TestReplyToClient()
        {
            // Arrange
            ClientSocket client = new ClientSocket();
            client.StartConnection();

            // Act
            string message = "Ping";
            client.SendMessage(message);
            string receivedMessage = client.ReceiveMessage();
            client.CloseConnection();

            // Assert
            Assert.That(receivedMessage, Is.EqualTo("Pong"));
        }

        // TODO: Implement a method in the server that checks if an incoming message from a client is a palindrome
        // The server should respond with "Palindrome" if the message is a palindrome, otherwise "Not a palindrome"
        [Test]
        public void TestPalindromeCheck()
        {
            // Arrange
            ClientSocket client = new ClientSocket();
            client.StartConnection();

            // Act
            string message = "racecar";
            client.SendMessage(message);
            string receivedMessage = client.ReceiveMessage();
            client.CloseConnection();

            // Assert
            Assert.That(receivedMessage, Is.EqualTo("Palindrome"));
        }

        // TODO: Implement a server method to handle multiple client communication
        // Modified broadcast test to include additional message exchanges
        [Test]
        public void TestBroadcastMessage()
        {
            // Arrange
            ClientSocket clientOne = new ClientSocket();
            clientOne.StartConnection();
            ClientSocket clientTwo = new ClientSocket();
            clientTwo.StartConnection();

            // Act
            string messageFromClientOne = "Message from client 1";
            clientOne.SendMessage(messageFromClientOne);
            string receivedMessageByClientTwo = clientTwo.ReceiveMessage();

            string messageFromClientTwo = "Reply from client 2";
            clientTwo.SendMessage(messageFromClientTwo);
            string receivedMessageByClientOne = clientOne.ReceiveMessage();

            clientOne.CloseConnection();
            clientTwo.CloseConnection();

            // Assert
            Assert.That(receivedMessageByClientTwo, Is.EqualTo(messageFromClientOne));
            Assert.That(receivedMessageByClientOne, Is.EqualTo(messageFromClientTwo));
        }

        // TODO: Implement a method in the server that checks if an incoming message is a number
        // Changed to a different arithmetic operation: product of digits
        [Test]
        public void TestProductOfDigits()
        {
            // Arrange
            ClientSocket client = new ClientSocket();
            client.StartConnection();

            // Act
            string message = "1234";
            client.SendMessage(message);
            string receivedMessage = client.ReceiveMessage();
            client.CloseConnection();

            // Assert
            Assert.That(receivedMessage, Is.EqualTo("24")); // 1*2*3*4 = 24
        }

        // TODO: Implement a method in the server that checks if the incoming message contains any vowels
        // The server should respond with the count of vowels in the message
        [Test]
        public void TestVowelCount()
        {
            // Arrange
            ClientSocket client = new ClientSocket();
            client.StartConnection();

            // Act
            string message = "hello world";
            client.SendMessage(message);
            string receivedMessage = client.ReceiveMessage();
            client.CloseConnection();

            // Assert
            Assert.That(receivedMessage, Is.EqualTo("3")); // e, o, o
        }
    }
}
