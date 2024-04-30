public partial class GameManager
{
    private void StatusScene()
    {
        Console.Clear();

        ConsoleUtility.ShowTitle("■ 상태보기 ■");
        Console.WriteLine("캐릭터의 정보가 표기됩니다.");

        ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
        Console.WriteLine("");
        Console.WriteLine($"{player.Name} ( {player.Job} )");

        // TODO : 능력치 강화분을 표현하도록 변경

        int bonusAtk = inventory.Select(item => item.IsEquipped ? item.Atk : 0).Sum();
        int bonusDef = inventory.Select(item => item.IsEquipped ? item.Def : 0).Sum();
        int bonusHp = inventory.Select(item => item.IsEquipped ? item.Hp : 0).Sum();

        var current = player;
        current.Atk += bonusAtk;
        current.Def += bonusDef;
        current.Hp += bonusHp;
        player.Atk = current.Atk;
        player.Def = current.Def;
        player.Hp = current.Hp;

        ConsoleUtility.PrintTextHighlights("공격력 : ", (player.Atk).ToString(), bonusAtk > 0 ? $" (+{bonusAtk})" : "");
        ConsoleUtility.PrintTextHighlights("방어력 : ", (player.Def).ToString(), bonusDef > 0 ? $" (+{bonusDef})" : "");
        ConsoleUtility.PrintTextHighlights("체 력 : ", (player.Hp).ToString(), bonusHp > 0 ? $" (+{bonusHp})" : "");

        ConsoleUtility.PrintTextHighlights("Gold : ", player.Gold.ToString());
        Console.WriteLine("");

        Console.WriteLine("0. 뒤로가기");
        Console.WriteLine("");

        switch (ConsoleUtility.PromptSceneChoice(0, 0))
        {
            case 0:
                MainScene();
                break;
        }
    }
}