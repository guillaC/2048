namespace _02048
{
    using System;
    using System.Collections.Generic;

    internal class Game
    {
        private int[,] map = new int[4, 4];
        private Dictionary<int, ConsoleColor> colorInt = new Dictionary<int, ConsoleColor>();
        private Random rnd = new Random();

        public Game()
        {
            this.colorInt.Add(0, ConsoleColor.White);
            this.colorInt.Add(2, ConsoleColor.Blue);
            this.colorInt.Add(4, ConsoleColor.DarkBlue);
            this.colorInt.Add(8, ConsoleColor.DarkCyan);
            this.colorInt.Add(16, ConsoleColor.Gray);
            this.colorInt.Add(32, ConsoleColor.DarkGray);
            this.colorInt.Add(64, ConsoleColor.DarkGreen);
            this.colorInt.Add(128, ConsoleColor.Green);
            this.colorInt.Add(256, ConsoleColor.Yellow);
            this.colorInt.Add(512, ConsoleColor.Magenta);
            this.colorInt.Add(1024, ConsoleColor.White);
            this.colorInt.Add(2048, ConsoleColor.Red);

            this.AddRandom();
            this.AddRandom();
        }

        public void Up()
        {
            for (int k = 0; k <= 4; k++)
            {
                for (int i = 3; i > 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (this.map[i - 1, j] == this.map[i, j])
                        {
                            this.map[i - 1, j] = this.map[i - 1, j] * 2;
                            this.map[i, j] = 0;
                        }

                        if (this.map[i - 1, j] == 0)
                        {
                            this.map[i - 1, j] = this.map[i, j];
                            this.map[i, j] = 0;
                        }
                    }
                }
            }
        }

        public void Down()
        {
            for (int k = 0; k <= 4; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (this.map[i + 1, j] == this.map[i, j])
                        {
                            this.map[i + 1, j] = this.map[i + 1, j] * 2;
                            this.map[i, j] = 0;
                        }

                        if (this.map[i + 1, j] == 0)
                        {
                            this.map[i + 1, j] = this.map[i, j];
                            this.map[i, j] = 0;
                        }
                    }
                }
            }
        }

        public void Left()
        {
            for (int k = 0; k <= 4; k++)
            {
                for (int j = 3; j > 0; j--)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (this.map[i, j - 1] == this.map[i, j])
                        {
                            this.map[i, j - 1] = this.map[i, j - 1] * 2;
                            this.map[i, j] = 0;
                        }

                        if (this.map[i, j - 1] == 0)
                        {
                            this.map[i, j - 1] = this.map[i, j];
                            this.map[i, j] = 0;
                        }
                    }
                }
            }
        }

        public void Right()
        {
            for (int k = 0; k <= 4; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (this.map[i, j + 1] == this.map[i, j])
                        {
                            this.map[i, j + 1] = this.map[i, j + 1] * 2;
                            this.map[i, j] = 0;
                        }

                        if (this.map[i, j + 1] == 0)
                        {
                            this.map[i, j + 1] = this.map[i, j];
                            this.map[i, j] = 0;
                        }
                    }
                }
            }
        }

        public void UserInput(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
            {
                if (key == ConsoleKey.UpArrow)
                {
                    this.Up();
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    this.Down();
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    this.Left();
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    this.Right();
                }

                this.AddRandom();
                this.ConsoleUpdate();
            }
        }

        public void AddRandom()
        {
            int randCol, randRow;
            if (!this.CheckWin())
            {
                if (this.CheckLoose())
                {
                    randCol = this.rnd.Next(0, 4);
                    randRow = this.rnd.Next(0, 4);
                    while (this.map[randCol, randRow] != 0)
                    {
                        randCol = this.rnd.Next(0, 4);
                        randRow = this.rnd.Next(0, 4);
                    }

                    this.map[randCol, randRow] = 2;
                }
                else
                {
                    Console.WriteLine("Loose.");
                    this.map = new int[4, 4];
                }
            }
            else
            {
                Console.WriteLine("Win.");
                this.map = new int[4, 4];
            }
        }

        public bool CheckWin()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (this.map[i, j] == 2048)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckLoose()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (this.map[i, j] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ConsoleUpdate()
        {
            for (int i = 0; i != 4; i++)
            {
                for (int j = 0; j != 4; j++)
                {
                    Console.ForegroundColor = this.colorInt[this.map[i, j]];
                    Console.Write(" " + this.map[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}