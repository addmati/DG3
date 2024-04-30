using System.Xml;

enum MonsterType
{
    Minion,
    VoidSwarm,
    SiegeMinion
}

public partial class GameManager
{
    private List<Monster> monsters;

    Random random = new Random();
    int randomNumber;
    int originalHp;
    private void BattleScene()
    {
        originalHp = player.Hp;
        monsters = new List<Monster>();
        
        randomNumber = random.Next(1, 5);
        for (int i = 0; i < randomNumber; i++)
        {
            // Enum.GetNames(typeof(MonsterType)).Length = 3
            // random.Next(3) = 0 ~ 2
            MonsterType monsterType = (MonsterType)random.Next(Enum.GetNames(typeof(MonsterType)).Length);

            switch (monsterType)
            {
                case MonsterType.Minion:
                    monsters.Add(new Monster("미니언", 2, 15, 5));
                    break;
                case MonsterType.VoidSwarm:
                    monsters.Add(new Monster("공허충", 3, 10, 9));
                    break;
                case MonsterType.SiegeMinion:
                    monsters.Add(new Monster("대포미니언", 5, 25, 8));
                    break;
            }
        }

        BattleStartScene();
    }

    private void BattleStartScene()
    {
        Console.Clear();

        ConsoleUtility.ShowTitle("■ Battle!! ■");

        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].PrintMonsterDescription();
        }

        Console.WriteLine("");
        Console.WriteLine("[내정보]");
        ConsoleUtility.PrintTextHighlights("Lv.", player.Level.ToString(), $" {player.Name} ( {player.Job} )");
        ConsoleUtility.PrintTextHighlights("HP ", player.Hp.ToString(), "", false);
        ConsoleUtility.PrintTextHighlights("/", "100");

        Console.WriteLine("");
        Console.WriteLine("1. 공격");
        Console.WriteLine("");

        switch (ConsoleUtility.PromptSceneChoice(0, 2))
        {
            case 0:
                MainScene(); 
                break;
            case 1:
                MyBattleScene();
                break;
            case 2:
                //skill
            default:
                break;
        }
    }

    private void MyBattleScene()
    {
        Console.Clear();

        ConsoleUtility.ShowTitle("■ Battle!! ■");

        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].PrintMonsterDescription(true,i+1);
        }
        Console.WriteLine("");
        Console.WriteLine("0. 취소");
        Console.WriteLine("");
        int keyInput = ConsoleUtility.PromptSceneChoice(0, monsters.Count);
        
        switch (keyInput)
        {
            case 0:
                MainScene();
                break;
            default:
                while (monsters[keyInput - 1].IsDead == true)
                {
                    Console.WriteLine("이미 죽어있습니다.");
                    keyInput = ConsoleUtility.PromptSceneChoice(0, monsters.Count);
                }
                MyBattleAttackScene(keyInput-1);                
                break;
        }
    }

    private void MyBattleAttackScene(int _enemyNumber)
    {

        int enemyNumber = _enemyNumber;
        int playerRandomDamage=0;
        randomNumber = random.Next(-1, 2);
        float _playerRandomDamage = player.Atk * randomNumber * 0.1f;
        if( (int)_playerRandomDamage < _playerRandomDamage)
        {
            playerRandomDamage = (int)_playerRandomDamage + 1;
        }
        else
        {
            playerRandomDamage = (int)_playerRandomDamage;
        }
        Console.Clear();
        
        ConsoleUtility.ShowTitle("■ Battle!! ■");

        Console.WriteLine($"{player.Name}의 공격!");
        Console.WriteLine($"Lv. {monsters[enemyNumber].Level} {monsters[enemyNumber].Name}을(를) 맞췄습니다. [데미지 : {player.Atk+ playerRandomDamage}]");
        Console.Write($"Lv. {monsters[enemyNumber].Level} {monsters[enemyNumber].Name}\n" +
            $"HP {monsters[enemyNumber].Hp} - > ");
        var current = monsters[enemyNumber];
        current.Hp -= (player.Atk + playerRandomDamage);
        monsters[enemyNumber].Hp = current.Hp;
        if (monsters[enemyNumber].Hp <= 0)
        {
            monsters[enemyNumber].IsDead = true;
            Console.WriteLine("Dead");
        }
        else
        {
            Console.WriteLine($"{monsters[enemyNumber].Hp}");
        }
        Console.WriteLine("");
        Console.WriteLine("0. 다음");
        Console.WriteLine("");
        ConsoleUtility.PromptSceneChoice(0, 0);
        EnemyTrunBattle();
    }

    private void EnemyTrunBattle()
    {
        int enemyDeadCount = 0;
        int enemyRandomDamage = 0;
        
        
        for (int i = 0; i < monsters.Count; i++)
        {
            Console.Clear();
            ConsoleUtility.ShowTitle("■ Battle!! ■");
            if (monsters[i].IsDead==false)
            {
                randomNumber = random.Next(-1, 2);
                float _enemyRandomDamage = monsters[i].Atk * randomNumber * 0.1f;
                if ((int)_enemyRandomDamage < _enemyRandomDamage)
                {
                    enemyRandomDamage = (int)_enemyRandomDamage + 1;
                }
                else
                {
                    enemyRandomDamage = (int)_enemyRandomDamage;
                }
                

                Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name}의 공격!");
                Console.WriteLine($"{player.Name}을(를) 맞췄습니다. [데미지 : {monsters[i].Atk + enemyRandomDamage-player.Def}]");
                Console.Write($"Lv.{player.Level} {player.Name}\n" +
                              $"HP {player.Hp} - > ");
                var current = player;
                current.Hp -= (monsters[i].Atk+ enemyRandomDamage- player.Def);
                player.Hp = current.Hp;
                
                if (player.Hp <= 0)
                {
                    Console.WriteLine($"{player.Name} Dead");
                    Console.WriteLine("");
                    Console.WriteLine("0. 다음");
                    Console.WriteLine("");
                    ConsoleUtility.PromptSceneChoice(0, 0);
                    BattleResult();
                }
                else
                {
                    Console.WriteLine($"{player.Hp}");
                }
                Console.WriteLine("");
                Console.WriteLine("0. 다음");
                Console.WriteLine("");
                ConsoleUtility.PromptSceneChoice(0, 0);
            }
            else
            {
                enemyDeadCount++;
                if(enemyDeadCount == monsters.Count)
                {
                    Console.WriteLine("모든 몬스터를 처치하였습니다.");
                    Console.WriteLine("");
                    Console.WriteLine("0. 다음");
                    Console.WriteLine("");
                    ConsoleUtility.PromptSceneChoice(0, 0);
                    BattleResult();
                }
            }
            
        }
        BattleStartScene();
    }

    public void BattleResult()
    {
        
        Console.Clear();
        ConsoleUtility.ShowTitle("■ Battle!! - Result ■");
        if (player.Hp <= 0)
        {
            Console.WriteLine("You Lose");
            Console.WriteLine($"Lv.{player.Level} {player.Name}\n" +
                                 $"HP {player.Hp} - > {player.Hp}");
        }
        else
        {
            Console.WriteLine("Victory");
            Console.WriteLine($"던전에서{monsters.Count}마리를 잡았습니다.");
            Console.WriteLine($"Lv.{player.Level} {player.Name}\n" +
                                 $"HP {originalHp} - > {player.Hp}");
        }
        Console.WriteLine("");
        Console.WriteLine("0. 다음");
        Console.WriteLine("");
        ConsoleUtility.PromptSceneChoice(0, 0);
        MainScene();
    }
}
