using System;
using System.Collections.Generic;


public interface ICommand
{
    void Execute();
    void Undo();
}



public class Light
{
    public void On() => Console.WriteLine("Свет включен.");
    public void Off() => Console.WriteLine("Свет выключен.");
}

public class Door
{
    public void Open() => Console.WriteLine("Дверь открыта.");
    public void Close() => Console.WriteLine("Дверь закрыта.");
}

public class Thermostat
{
    private int temperature = 22;
    
    public void Increase() => Console.WriteLine($"Температура увеличена до {++temperature} градусов.");
    public void Decrease() => Console.WriteLine($"Температура уменьшена до {--temperature} градусов.");
}


public class LightOnCommand : ICommand
{
    private Light light;
    
    public LightOnCommand(Light light) => this.light = light;
    
    public void Execute() => light.On();
    public void Undo() => light.Off();
}

public class LightOffCommand : ICommand
{
    private Light light;
    
    public LightOffCommand(Light light) => this.light = light;
    
    public void Execute() => light.Off();
    public void Undo() => light.On();
}

public class DoorOpenCommand : ICommand
{
    private Door door;
    
    public DoorOpenCommand(Door door) => this.door = door;
    
    public void Execute() => door.Open();
    public void Undo() => door.Close();
}

public class DoorCloseCommand : ICommand
{
    private Door door;
    
    public DoorCloseCommand(Door door) => this.door = door;
    
    public void Execute() => door.Close();
    public void Undo() => door.Open();
}

public class IncreaseTemperatureCommand : ICommand
{
    private Thermostat thermostat;
    
    public IncreaseTemperatureCommand(Thermostat thermostat) => this.thermostat = thermostat;
    
    public void Execute() => thermostat.Increase();
    public void Undo() => thermostat.Decrease();
}

public class DecreaseTemperatureCommand : ICommand
{
    private Thermostat thermostat;
    
    public DecreaseTemperatureCommand(Thermostat thermostat) => this.thermostat = thermostat;
    
    public void Execute() => thermostat.Decrease();
    public void Undo() => thermostat.Increase();
}


public class SmartHomeInvoker
{
    private readonly Stack<ICommand> commandHistory = new Stack<ICommand>();
    
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Push(command);
    }
    
    public void UndoLastCommand()
    {
        if (commandHistory.Count > 0)
            commandHistory.Pop().Undo();
    }
}

public class Client
{
    public static void Main()
    {
        Light light = new Light();
        Door door = new Door();
        Thermostat thermostat = new Thermostat();

        SmartHomeInvoker invoker = new SmartHomeInvoker();

        // Управление светом
        invoker.ExecuteCommand(new LightOnCommand(light));
        invoker.UndoLastCommand();

        // Управление дверью
        invoker.ExecuteCommand(new DoorOpenCommand(door));
        invoker.UndoLastCommand();

        // Управление температурой
        invoker.ExecuteCommand(new IncreaseTemperatureCommand(thermostat));
        invoker.UndoLastCommand();
    }
}
