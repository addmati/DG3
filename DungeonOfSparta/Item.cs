﻿public enum ItemType
{
    WEAPON,
    HELMET,
    BODYARMOUR,
    GLOVE,
    BOOT
}

internal class Item
{
    public string Name { get; }
    public string Desc { get; }

    public ItemType Type { get; }

    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }

    public int Price { get; }

    public bool IsEquipped { get; private set; }
    public bool IsPurchased { get; private set; }

    public Item(string name, string desc, ItemType type, int atk, int def, int hp, int price, bool isEquipped = false, bool isPurchased = false)
    {
        Name = name;
        Desc = desc;
        Type = type;
        Atk = atk;
        Def = def;
        Hp = hp;
        Price = price;
        IsEquipped = isEquipped;
        IsPurchased = isPurchased;
    }

    // 아이템 정보를 보여줄때 타입이 비슷한게 2가지있음.
    // 1. 인벤토리에서 그냥 내가 어느 아이템 가지고 있는지 보여줄 때
    // 2. 장착관리에서 내가 어떤 아이템을 낄지 말지 결정할 때
    internal void PrintItemStatDescription(bool withNumber = false, int idx = 0)
    {
        Console.Write("- ");
        if (withNumber)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(ConsoleUtility.PadRightForMixedText(idx.ToString(), 3));
            Console.ResetColor();
        }
        if (IsEquipped)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("E");
            Console.ResetColor();
            Console.Write("]");
            Console.Write(ConsoleUtility.PadRightForMixedText(Name, 11));
        }
        else Console.Write(ConsoleUtility.PadRightForMixedText(Name, 14));

        Console.Write(" | ");

        int cnt = 0;

        if (Atk != 0)
        {
            Console.Write($"공격력 +{ConsoleUtility.PadRightForMixedText(Atk.ToString(), 2)}");
            cnt = 0;
        }
        else if (Def != 0)
        {
            Console.Write($"방어력 +{ConsoleUtility.PadRightForMixedText(Def.ToString(), 2)}");
            cnt = 1;
        }
        else if (Hp != 0)
        {
            Console.Write($"체  력 +{ConsoleUtility.PadRightForMixedText(Hp.ToString(), 2)}");
            cnt = 2;
        }

        Console.Write(" | ");

        Console.WriteLine(Desc);

        // 방어력도 있는 경우
        if (cnt < 1 && Def != 0)
        {
            if (withNumber)
            {
                //Console.Write(ConsoleUtility.PadRightForMixedText("", 3));
                Console.Write(new string(' ', 3));
            }
            Console.Write(new string(' ', 16));
            Console.Write(" | ");
            Console.Write($"방어력 +{ConsoleUtility.PadRightForMixedText(Def.ToString(), 2)}");
            cnt = 1;
            Console.WriteLine(" | ");
        }

        // 체력도 있는 경우
        if (cnt < 2 && Hp != 0)
        {
            if (withNumber)
            {
                Console.Write(new string(' ', 3));
            }
            Console.Write(new string(' ', 16));
            Console.Write(" | ");
            Console.Write($"체  력 +{ConsoleUtility.PadRightForMixedText(Hp.ToString(), 2)}");
            cnt = 2;
            Console.WriteLine(" | ");
        }
    }

    public void PrintStoreItemDescription(bool withNumber = false, int idx = 0, bool onlyGold = false)
    {
        Console.Write("- ");
        // 장착관리 전용
        if (withNumber)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(ConsoleUtility.PadRightForMixedText(idx.ToString(), 3));
            Console.ResetColor();
        }
        Console.Write(ConsoleUtility.PadRightForMixedText(Name, 14));

        Console.Write(" | ");

        int cnt = 0;

        if (Atk != 0)
        {
            Console.Write($"공격력 +{ConsoleUtility.PadRightForMixedText(Atk.ToString(), 2)}");
            cnt = 0;
        }
        else if (Def != 0)
        {
            Console.Write($"방어력 +{ConsoleUtility.PadRightForMixedText(Def.ToString(), 2)}");
            cnt = 1;
        }
        else if (Hp != 0)
        {
            Console.Write($"체  력 +{ConsoleUtility.PadRightForMixedText(Hp.ToString(), 2)}");
            cnt = 2;
        }

        Console.Write(" | ");

        Console.Write(ConsoleUtility.PadRightForMixedText(Desc, 20));

        Console.Write(" | ");

        if (IsPurchased && !onlyGold)
        {
            Console.WriteLine("구매완료");
        }
        else
        {
            ConsoleUtility.PrintTextHighlights("", Price.ToString(), " G");
        }

        // 방어력도 있는 경우
        if (cnt < 1 && Def != 0)
        {
            if (withNumber)
            {
                //Console.Write(ConsoleUtility.PadRightForMixedText("", 3));
                Console.Write(new string(' ', 3));
            }
            Console.Write(new string(' ', 16));
            Console.Write(" | ");
            Console.Write($"방어력 +{ConsoleUtility.PadRightForMixedText(Def.ToString(), 2)}");
            cnt = 1;
            Console.WriteLine(" | ");
        }

        // 체력도 있는 경우
        if (cnt < 2 && Hp != 0)
        {
            if (withNumber)
            {
                Console.Write(new string(' ', 3));
            }
            Console.Write(new string(' ', 16));
            Console.Write(" | ");
            Console.Write($"체  력 +{ConsoleUtility.PadRightForMixedText(Hp.ToString(), 2)}");
            cnt = 2;
            Console.WriteLine(" | ");
        }
    }

    internal void ToggleEquipStatus()
    {
        IsEquipped = !IsEquipped;
    }

    internal void Purchase()
    {
        IsPurchased = true;
    }

    internal void Sell()
    {
        IsEquipped = false;
        IsPurchased = false;
    }
}