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
        Player _player;
        Room[,] _world;
        Random _random = new Random();
        private int _currentBackpackSelection;
        private List<string> _combatLog;
        private Monster[] _monsterSpawns;
        private Item[] _itemsSpawns;

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
            } while (_player.CurrentHealth > 0);

            GameOver();
        }

        private void CreatePlayer()
        {   
            RustyKnife rustyKnife = new RustyKnife();
            ClothArmor clothArmor = new ClothArmor();
            _player = new Player(100, rustyKnife, clothArmor, "Oscar", 0, 0);
        }

        private void CreateWorld()
        {
            _world = new Room[20, 5];
            for (int y = 0; y < _world.GetLength(1); y++)
            {
                for (int x = 0; x < _world.GetLength(0); x++)
                {
                    _world[x, y] = new Room();            

                    int itemOrMonsterPercentage = _random.Next(0, 3);

                    // Monster spawn
                    if (itemOrMonsterPercentage <= 1)
                    {
                        int monsterTypePercentage = _random.Next(0, 10);
                        switch (monsterTypePercentage)
                        {
                            case 0:
                                _world[x, y].Monster = new Dragon();
                                break;
                            case 1:
                                _world[x, y].Monster = new Bear();
                                break;
                            case 2:
                                _world[x, y].Monster = new Cow();
                                break;
                        }
                    }
                    // Item spawn
                    else
                    {
                        int itemTypePercentage = _random.Next(0, 15);
                        switch (itemTypePercentage)
                        {
                            case 0:
                                _world[x, y].Item = new SmallPotion();
                                break;
                            case 1:
                                _world[x, y].Item = new BigPotion();
                                break;
                            case 2:
                                _world[x, y].Item = new Broadsword();
                                break;
                            case 3:
                                _world[x, y].Item = new Rocketlauncher();
                                break;
                            case 4:
                                _world[x, y].Item = new LeatherArmor();
                                break;
                            case 5:
                                _world[x, y].Item = new PlateArmor();
                                break;
                        }
                    }
                }
            }
        }

        private void DisplayWorld()
        {
            for (int y = 0; y < _world.GetLength(1); y++)
            {
                for (int x = 0; x < _world.GetLength(0); x++)
                {
                    Room room = _world[x, y];
                    if (_player.X == x && _player.Y == y)
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
            string line = "-------------------";
            Console.WriteLine(line);
            Console.WriteLine($"Player: {_player.Name}");
            Console.WriteLine($"Health: {_player.CurrentHealth} / {_player.MaxHealth}");
            Console.WriteLine(line);
            Console.WriteLine("Equipment:");
            Console.WriteLine($"Armor: [{_player.Armor.Name}] ({_player.Armor.ArmorClass} AC)");
            Console.WriteLine($"Weapon: [{_player.Weapon.Name}] ({_player.Weapon.WeaponDamage} ATK)");
            Console.WriteLine(line);
            Console.WriteLine("Backpack: ");
            for (int i = 0; i < _player.Backpack.Count; i++)
            {
                if (i == _currentBackpackSelection)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;                 
                }
                else if (_player.Backpack.Count == 0)
                {
                    Console.WriteLine("Empty");
                }

                Console.WriteLine(_player.Backpack[i].Name);
                Console.ResetColor();
            }
            Console.WriteLine(line);
            foreach (string s in _combatLog.Reverse<string>().Take(6))
            {
                Console.WriteLine(s);
                Console.WriteLine("----");
            }
        }

        private void AskForMovement()
        {
            int newX = _player.X;
            int newY = _player.Y;
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
                    if (_currentBackpackSelection != _player.Backpack.Count-1)
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
                newX >= 0 && newX < _world.GetLength(0) &&
                newY >= 0 && newY < _world.GetLength(1))
            {
                _player.X = newX;
                _player.Y = newY;
                RoomEncounter();
            }
        }

        private void UseCurrentlySelectedItem()
        {
            _player.Backpack[_currentBackpackSelection].UseItem(_player);
        }

        private void RoomEncounter()
        {
            Room room = _world[_player.X, _player.Y];

            if (room.Item != null)
            {
                room.Item.PickUpItem(_player);
                _combatLog.Add($"You picked up {room.Item.Name}!");
                room.Item = null; // Remove item when picked up
                DisplayStats();
            }       

            if (room.Monster != null)
            {
                _combatLog.Add(room.Monster.Attack(_player));
                _combatLog.Add(_player.Attack(room.Monster));

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
