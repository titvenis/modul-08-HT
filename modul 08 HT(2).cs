using System;


public abstract class Beverage
{
    
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        if (CustomerWantsCondiments())
        {
            AddCondiments();
        }
    }

    protected abstract void Brew();
    protected abstract void AddCondiments();

    private void BoilWater() => Console.WriteLine("Кипячение воды...");
    private void PourInCup() => Console.WriteLine("Наливание в чашку...");

    // Перехватываемый метод (hook)
    protected virtual bool CustomerWantsCondiments()
    {
        return true;
    }
}


public class Tea : Beverage
{
    protected override void Brew() => Console.WriteLine("Заваривание чая...");
    protected override void AddCondiments() => Console.WriteLine("Добавление лимона...");
}


public class Coffee : Beverage
{
    protected override void Brew() => Console.WriteLine("Заваривание кофе...");
    protected override void AddCondiments() => Console.WriteLine("Добавление сахара и молока...");
    protected override bool CustomerWantsCondiments()
    {
        Console.Write("Хотите добавить сахар и молоко (y/n)? ");
        string input = Console.ReadLine().ToLower();
        return input == "y";
    }
}


public class HotChocolate : Beverage
{
    protected override void Brew() => Console.WriteLine("Подогрев шоколада...");
    protected override void AddCondiments() => Console.WriteLine("Добавление взбитых сливок...");

    protected override bool CustomerWantsCondiments()
    {
        Console.Write("Хотите добавить взбитые сливки (y/n)? ");
        string input = Console.ReadLine().ToLower();
        return input == "y";
    }
}

public class Client
{
    public static void Main()
    {
        Beverage tea = new Tea();
        Beverage coffee = new Coffee();
        Beverage hotChocolate = new HotChocolate();

        Console.WriteLine("\nПриготовление чая:");
        tea.PrepareRecipe();

        Console.WriteLine("\nПриготовление кофе:");
        coffee.PrepareRecipe();

        Console.WriteLine("\nПриготовление горячего шоколада:");
        hotChocolate.PrepareRecipe();
    }
}
