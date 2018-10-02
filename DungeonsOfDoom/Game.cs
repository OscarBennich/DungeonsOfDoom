using System;
using System.Collections.Generic;
using System.Linq;
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
        Player player;
        Room[,] world;
        Random random = new Random();
        private int _currentBackpackSelection;
        private List<string> _combatLog;

        public void Play()
        {
            CreatePlayer();
            CreateWorld();
            _currentBackpackSelection = 0;
            _combatLog = new List<string>();

            do
            {
                Console.Clear();
                DisplayWorld();
                DisplayStats();
                AskForMovement();
            } while (player.CurrentHealth > 0);

            GameOver();
        }

        private void CreatePlayer()
        {   
            RustyKnife rustyKnife = new RustyKnife();
            ClothArmor clothArmor = new ClothArmor();
            player = new Player(100, rustyKnife, clothArmor, "Oscar", 0, 0);
        }

        private void CreateWorld()
        {
            world = new Room[20, 5];
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();            

                    int itemOrMonsterPercentage = random.Next(0, 3);

                    // Monster spawn
                    if (itemOrMonsterPercentage <= 1)
                    {
                        int monsterTypePercentage = random.Next(0, 10);
                        switch (monsterTypePercentage)
                        {
                            case 0:
                                world[x, y].Monster = new Dragon();
                                break;
                            case 1:
                                world[x, y].Monster = new Bear();
                                break;
                            case 2:
                                world[x, y].Monster = new Cow();
                                break;
                        }
                    }
                    // Item spawn
                    else
                    {
                        int itemTypePercentage = random.Next(0, 15);
                        switch (itemTypePercentage)
                        {
                            case 0:
                                world[x, y].Item = new SmallPotion();
                                break;
                            case 1:
                                world[x, y].Item = new BigPotion();
                                break;
                            case 2:
                                world[x, y].Item = new Broadsword();
                                break;
                            case 3:
                                world[x, y].Item = new Rocketlauncher();
                                break;
                            case 4:
                                world[x, y].Item = new LeatherArmor();
                                break;
                            case 5:
                                world[x, y].Item = new PlateArmor();
                                break;
                        }
                    }
                }
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                    {
                        Console.Write("P");
                    }
                    else if (room.Monster != null)
                        Console.Write(room.Monster.GetShortName());
                    else if (room.Item != null)
                        Console.Write("I");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void DisplayStats()
        {   
            Console.WriteLine("-------------------");
            Console.WriteLine($"Player: {player.Name}");
            Console.WriteLine($"Health: {player.CurrentHealth} / {player.MaxHealth}");
            Console.WriteLine("-------------------");
            Console.WriteLine("Equipment:");
            Console.WriteLine($"Armor: [{player.Armor.Name}] ({player.Armor.ArmorClass} AC)");
            Console.WriteLine($"Weapon: [{player.Weapon.Name}] ({player.Weapon.WeaponDamage} ATK)");
            Console.WriteLine("-------------------");
            Console.WriteLine("Backpack: ");
            for (int i = 0; i < player.Backpack.Count; i++)
            {
                if (i == _currentBackpackSelection)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;                 
                }
                else if (player.Backpack.Count == 0)
                {
                    Console.WriteLine("Empty");
                }

                Console.WriteLine(player.Backpack[i].Name);
                Console.ResetColor();
            }
            Console.WriteLine("-------------------");
            foreach (string s in _combatLog)
            {
                Console.WriteLine(s);
                Console.WriteLine("----");
            }
        }

        private void AskForMovement()
        {
            int newX = player.X;
            int newY = player.Y;
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
                    if (_currentBackpackSelection != 0)
                    {
                        _currentBackpackSelection--;
                    }
                    break;
                case ConsoleKey.S:
                    if (_currentBackpackSelection != player.Backpack.Count-1)
                    {
                        _currentBackpackSelection++;
                    }
                    break;
                case ConsoleKey.Enter: UseCurrentlySelectedItem();
                    _currentBackpackSelection = 0;
                     DisplayStats();
                    break;
                default: isValidKey = false; break;
            }

            if (isValidKey &&
                newX >= 0 && newX < world.GetLength(0) &&
                newY >= 0 && newY < world.GetLength(1))
            {
                player.X = newX;
                player.Y = newY;
                RoomEncounter();
            }
        }

        private void UseCurrentlySelectedItem()
        {
            player.Backpack[_currentBackpackSelection].UseItem(player);
        }

        private void RoomEncounter()
        {
            Room room = world[player.X, player.Y];
            room.Item?.PickUpItem(player);
            room.Item = null; // Remove item when picked up
            DisplayStats();

            if (room.Monster != null)
            {
                _combatLog.Add(room.Monster.Attack(player));
                _combatLog.Add(player.Attack(room.Monster));

                if(room.Monster.CurrentHealth <= 0)
                {
                    _combatLog.Add($"{room.Monster.Name} was defeated!");
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
