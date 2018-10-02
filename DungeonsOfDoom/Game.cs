using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Game
    {
        Player player;
        Room[,] world;
        Random random = new Random();

        public void Play()
        {
            CreatePlayer();
            CreateWorld();

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
            player = new Player(100, rustyKnife, "Oscar", 0, 0);
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
                        int itemTypePercentage = random.Next(0, 10);
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
                        Console.Write(room.Monster.Name);
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
            Console.WriteLine("Armor: [...]");
            Console.WriteLine($"Weapon: [{player.Weapon.Name}] ({player.Weapon.WeaponDamage} ATK)");
            Console.WriteLine("-------------------");
            Console.WriteLine("Backpack: ");
            foreach (Item i in player.Backpack)
            {
                Console.WriteLine(i.Name);
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
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
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

        private void RoomEncounter()
        {
            Room room = world[player.X, player.Y];
            room.Item?.PickUpItem(player);
            room.Item = null; // Remove item when picked up

            if (room.Monster != null)
            {
                room.Monster?.Attack(player);
                player.Attack(room.Monster);

                if(room.Monster.CurrentHealth <= 0)
                {
                    room.Monster = null; // Remove monster when defeated
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
