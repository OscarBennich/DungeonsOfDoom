using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Armors;
using DungeonsOfDoom.Weapons;
using DungeonsOfDoom.Potions;
using DungeonsOfDoom.Monsters;

namespace DungeonsOfDoom
{
    class Game
    {
        Player Player;
        Room[,] World;
        Random Random = new Random();
        private int CurrentBackpackSelection;
        private List<string> CombatLog;
        private List<Monster> MonsterSpawns;
        private List<Item> ItemsSpawns;
        private List<ISpawnable> SpawnList;

        public void Play()
        {   
            MonsterSpawns = new List<Monster>();
            ItemsSpawns  = new List<Item>();

            SpawnList = new List<ISpawnable>();

            var test =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(ISpawnable))
                select type;

            var monsterSubclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(Monster))
                select type;

            var armorSubclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(Armor))
                select type;

            var potionSubclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(Potion))
                select type;

            var weaponSubclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(Weapon))
                select type;

            for (int i = 0; i < (int)Rarity.Common; i++)
            {
                foreach (var type in monsterSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Monster)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Common)
                    {
                        MonsterSpawns.Add(instance);
                        SpawnList.Add(instance);
                        
                    }
                }

                foreach (var type in armorSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Common)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in potionSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Common)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in weaponSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Common)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }
            }
            for (int i = 0; i < (int)Rarity.Rare; i++)
            {
                foreach (var type in monsterSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Monster)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Rare)
                    {
                        MonsterSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in armorSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Rare)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in potionSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Rare)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in weaponSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Rare)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }
            }
            for (int i = 0; i < (int)Rarity.Legendary; i++)
            {
                foreach (var type in monsterSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Monster)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Legendary)
                    {
                        MonsterSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in armorSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Legendary)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in potionSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Legendary)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }

                foreach (var type in weaponSubclasses)
                {
                    var ctor = type.GetConstructors().Single();
                    var instance = (Item)ctor.Invoke(new object[] { });

                    if (instance.Rarity == Rarity.Legendary)
                    {
                        ItemsSpawns.Add(instance);
                        SpawnList.Add(instance);
                    }
                }
            }

            // Pick number of monster spawns
            int numberOfSpawns = 10;

            //MonsterSpawns = MonsterSpawns.OrderBy(x => Random.Next()).Take(numberOfSpawns / 2).ToList();
            //ItemsSpawns = ItemsSpawns.OrderBy(x => Random.Next()).Take(numberOfSpawns / 2).ToList();       

            SpawnList = SpawnList.OrderBy(x => Random.Next()).Take(numberOfSpawns).ToList();

            CreatePlayer();
            CreateWorld(SpawnList);
            CurrentBackpackSelection = 0;
            CombatLog = new List<string>();

            do
            {
                Console.Clear();
                DisplayWorld();
                DisplayStats();
                AskForMovement();
            } while (Player.CurrentHealth > 0);

            GameOver();
        }

        private void CreatePlayer()
        {   
            RustyKnife rustyKnife = new RustyKnife();
            ClothArmor clothArmor = new ClothArmor();
            string playerName = "Oscar";
            Player = new Player(100, rustyKnife, clothArmor, playerName, 0, 0);
        }

        private void CreateWorld(List<ISpawnable> spawnList)
        {
            int xLength = 20;
            int yLength = 5;
            World = new Room[xLength, yLength];

            // Create grid
            for (int y = 0; y < World.GetLength(1); y++)
            {
                for (int x = 0; x < World.GetLength(0); x++)
                {   
                    World[x, y] = new Room();            
                }
            }

            // Spawn things
            for (int i = 0; i < spawnList.Count - 1; i++)
            {
                int x = Random.Next(0, xLength);
                int y = Random.Next(0, yLength);

                World[x, y].Spawn = spawnList[i];
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < World.GetLength(1); y++)
            {
                for (int x = 0; x < World.GetLength(0); x++)
                {
                    Room room = World[x, y];
                    if (Player.X == x && Player.Y == y)
                    {
                        Console.Write("P");
                    }
                    else
                    {
                        var monster = room.Spawn as Monster;
                        if (monster != null)
                            Console.Write(monster.GetShortName());

                        var item = room.Spawn as Item;
                        if (item != null)
                            Console.Write("I");
                        else
                            Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        private void DisplayStats()
        {
            string line = "-------------------";
            Console.WriteLine(line);
            Console.WriteLine($"Player: {Player.Name}");
            Console.WriteLine($"Health: {Player.CurrentHealth} / {Player.MaxHealth}");
            Console.WriteLine(line);
            Console.WriteLine("Equipment:");
            Console.WriteLine($"Armor: [{Player.Armor.Name}] ({Player.Armor.ArmorClass} AC)");
            Console.WriteLine($"Weapon: [{Player.Weapon.Name}] ({Player.Weapon.WeaponDamage} ATK)");
            Console.WriteLine(line);
            Console.WriteLine("Backpack: ");
            for (int i = 0; i < Player.Backpack.Count; i++)
            {
                if (i == CurrentBackpackSelection)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;                 
                }
                else if (Player.Backpack.Count == 0)
                {
                    Console.WriteLine("Empty");
                }

                Console.WriteLine(Player.Backpack[i].Name);
                Console.ResetColor();
            }
            Console.WriteLine(line);
            foreach (string s in CombatLog.Reverse<string>().Take(6))
            {
                Console.WriteLine(s);
                Console.WriteLine("----");
            }
        }

        private void AskForMovement()
        {
            int newX = Player.X;
            int newY = Player.Y;
            bool isValidKey = true;

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++;
                    break;
                case ConsoleKey.LeftArrow: newX--;
                    break;
                case ConsoleKey.UpArrow: newY--;
                    break;
                case ConsoleKey.DownArrow: newY++;
                    break;
                case ConsoleKey.W: 
                    if (CurrentBackpackSelection != 0)
                    {
                        CurrentBackpackSelection--;
                    }
                    break;
                case ConsoleKey.S:
                    if (CurrentBackpackSelection != Player.Backpack.Count-1)
                    {
                        CurrentBackpackSelection++;
                    }
                    break;
                case ConsoleKey.Enter:
                    UseCurrentlySelectedItem();
                    CurrentBackpackSelection = 0;
                     DisplayStats();
                    break;
                default: isValidKey = false; break;
            }

            if (isValidKey &&
                newX >= 0 && newX < World.GetLength(0) &&
                newY >= 0 && newY < World.GetLength(1))
            {
                Player.X = newX;
                Player.Y = newY;
                RoomEncounter();
            }
        }

        private void UseCurrentlySelectedItem()
        {
            Player.Backpack[CurrentBackpackSelection].UseItem(Player);
        }

        private void RoomEncounter()
        {
            Room room = World[Player.X, Player.Y];

            if (room.Item != null)
            {
                room.Item.PickUp(Player);
                CombatLog.Add($"You picked up {room.Item.Name}!");
                room.Item = null; // Remove item when picked up
                DisplayStats();
            }       

            if (room.Monster != null)
            {
                CombatLog.Add(room.Monster.Attack(Player));
                CombatLog.Add(Player.Attack(room.Monster));

                if(room.Monster.CurrentHealth <= 0)
                {
                    CombatLog.Add($"{room.Monster.Name} was defeated!");
                    room.Monster.PickUp(Player);
                    room.Monster = null; // Remove monster when defeated                    
                    DisplayStats(); 
                }
            }
        }


        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            Play();
        }
    }
}
