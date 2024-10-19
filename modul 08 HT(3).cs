using System;
using System.Collections.Generic;


public interface IMediator
{
    void SendMessage(string message, User user);
    void SendPrivateMessage(string message, User sender, string recipient);
    void AddUser(User user);
    void RemoveUser(User user);
}


public class ChatRoom : IMediator
{
    private Dictionary<string, User> users = new Dictionary<string, User>();

    public void AddUser(User user)
    {
        if (!users.ContainsKey(user.Name))
        {
            users.Add(user.Name, user);
            NotifyAll($"{user.Name} присоединился к чату.");
        }
    }

    public void RemoveUser(User user)
    {
        if (users.ContainsKey(user.Name))
        {
            users.Remove(user.Name);
            NotifyAll($"{user.Name} покинул чат.");
        }
    }

    public void SendMessage(string message, User sender)
    {
        foreach (var user in users.Values)
        {
            if (user != sender)
                user.ReceiveMessage($"{sender.Name}: {message}");
        }
    }

    public void SendPrivateMessage(string message, User sender, string recipient)
    {
        if (users.ContainsKey(recipient))
        {
            users[recipient].ReceiveMessage($"(Личное) {sender.Name}: {message}");
        }
        else
        {
            sender.ReceiveMessage($"Пользователь {recipient} не найден.");
        }
    }

    private void NotifyAll(string message)
    {
        foreach (var user in users.Values)
        {
            user.ReceiveMessage(message);
        }
    }
}


public abstract class User
{
    protected IMediator mediator;
    public string Name { get; }

    protected User(IMediator mediator, string name)
    {
        this.mediator = mediator;
        this.Name = name;
    }

    public abstract void SendMessage(string message);
    public abstract void SendPrivateMessage(string message, string recipient);
    public abstract void ReceiveMessage(string message);
}


public class ChatUser : User
{
    public ChatUser(IMediator mediator, string name) : base(mediator, name) { }

    public override void SendMessage(string message)
    {
        Console.WriteLine($"{Name} отправляет сообщение: {message}");
        mediator.SendMessage(message, this);
    }

    public override void SendPrivateMessage(string message, string recipient)
    {
        Console.WriteLine($"{Name} отправляет личное сообщение {recipient}: {message}");
        mediator.SendPrivateMessage(message, this, recipient);
    }

    public override void ReceiveMessage(string message)
    {
        Console.WriteLine($"{Name} получил сообщение: {message}");
    }
}


public class Client
{
    public static void Main()
    {
        IMediator chatRoom = new ChatRoom();

        User user1 = new ChatUser(chatRoom, "Алиса");
        User user2 = new ChatUser(chatRoom, "Боб");
        User user3 = new ChatUser(chatRoom, "Чарли");

        chatRoom.AddUser(user1);
        chatRoom.AddUser(user2);
        chatRoom.AddUser(user3);

        user1.SendMessage("Привет всем!");
        user2.SendPrivateMessage("Привет, Алиса!", "Алиса");

        chatRoom.RemoveUser(user3);
    }
}
