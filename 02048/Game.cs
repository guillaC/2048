namespace _02048
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class Game
    {
        private int winValue = 2048;
        private int size = 4;
        private int[,] board = new int[4, 4];
        private bool auto = false;
        private Dictionary<int, ConsoleColor> colorInt = new Dictionary<int, ConsoleColor>();
        private Random rnd = new Random();

        public Game()
        {
            this.colorInt.Add(0, ConsoleColor.White);
            this.colorInt.Add(2, ConsoleColor.Blue);
            this.colorInt.Add(4, ConsoleColor.Cyan);
            this.colorInt.Add(8, ConsoleColor.Green);
            this.colorInt.Add(16, ConsoleColor.Magenta);
            this.colorInt.Add(32, ConsoleColor.Red);
            this.colorInt.Add(64, ConsoleColor.Yellow);
            this.colorInt.Add(128, ConsoleColor.White);
            this.colorInt.Add(256, ConsoleColor.Blue);
            this.colorInt.Add(512, ConsoleColor.Cyan);
            this.colorInt.Add(1024, ConsoleColor.Green);
            this.colorInt.Add(2048, ConsoleColor.Magenta);

            this.AddRandom();
            this.AddRandom();
            this.ConsoleUpdate();

            Thread threadAuto = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    if (this.auto == true)
                    {
                        this.AutoPlay();
                    }
                }
            });

            threadAuto.Start();
        }

        private enum Direction
        {
            Up, Down, Left, Right
        };

        private void Move(Direction direction)
        {
            for (int k = 0; k < size; k++)
            {
                if (direction == Direction.Up)
                {
                    for (int i = size - 1; i > 0; i--)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (this.board[i - 1, j] == this.board[i, j] && k == 0)
                            {
                                this.board[i - 1, j] = this.board[i - 1, j] * 2;
                                this.board[i, j] = 0;
                            }

                            if (this.board[i - 1, j] == 0)
                            {
                                this.board[i - 1, j] = this.board[i, j];
                                this.board[i, j] = 0;
                            }
                        }
                    }
                }
                else if (direction == Direction.Down)
                {
                    for (int i = 0; i < size - 1; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (this.board[i + 1, j] == this.board[i, j] && k == 0)
                            {
                                this.board[i + 1, j] = this.board[i + 1, j] * 2;
                                this.board[i, j] = 0;
                            }

                            if (this.board[i + 1, j] == 0)
                            {
                                this.board[i + 1, j] = this.board[i, j];
                                this.board[i, j] = 0;
                            }
                        }
                    }
                }
                else if (direction == Direction.Left)
                {
                    for (int j = size - 1; j > 0; j--)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (this.board[i, j - 1] == this.board[i, j] && k == 0)
                            {
                                this.board[i, j - 1] = this.board[i, j - 1] * 2;
                                this.board[i, j] = 0;
                            }

                            if (this.board[i, j - 1] == 0)
                            {
                                this.board[i, j - 1] = this.board[i, j];
                                this.board[i, j] = 0;
                            }
                        }
                    }
                }
                else if (direction == Direction.Right)
                {
                    for (int j = 0; j < size - 1; j++)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (this.board[i, j + 1] == this.board[i, j] && k == 0)
                            {
                                this.board[i, j + 1] = this.board[i, j + 1] * 2;
                                this.board[i, j] = 0;
                            }

                            if (this.board[i, j + 1] == 0)
                            {
                                this.board[i, j + 1] = this.board[i, j];
                                this.board[i, j] = 0;
                            }
                        }
                    }
                }
            }
        }

        public void UserInput(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        this.Move(Direction.Up);
                        break;

                    case ConsoleKey.DownArrow:
                        this.Move(Direction.Down); ;
                        break;

                    case ConsoleKey.LeftArrow:
                        this.Move(Direction.Left);
                        break;

                    case ConsoleKey.RightArrow:
                        this.Move(Direction.Right); ;
                        break;
                }

                this.AddRandom();
                this.ConsoleUpdate();
            }
            else if (key == ConsoleKey.Spacebar)
            {
                this.auto = !this.auto;
            }
        }

        private void AutoPlay()
        {
            ;
            IA IA = new IA(board);
            ConsoleKey key = IA.returnBest();
            UserInput(key);
        }

        private void AddRandom()
        {
            int randCol, randRow;
            if (!this.CheckWin())
            {
                if (this.CheckLose())
                {
                    randCol = this.rnd.Next(0, size);
                    randRow = this.rnd.Next(0, size);
                    while (this.board[randCol, randRow] != 0)
                    {
                        randCol = this.rnd.Next(0, size);
                        randRow = this.rnd.Next(0, size);
                    }

                    this.board[randCol, randRow] = 2;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lose.");
                    Console.ReadLine();
                    this.board = new int[size, size];
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Win.");
                Console.ReadLine();
                this.board = new int[size, size];
            }
        }

        private bool CheckWin()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (this.board[i, j] == winValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckLose()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (this.board[i, j] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ConsoleUpdate()
        {
            Console.Clear();
            for (int i = 0; i != size; i++)
            {
                for (int j = 0; j != size; j++)
                {
                    Console.SetCursorPosition(j * 4, i * 2);
                    Console.ForegroundColor = this.colorInt[this.board[i, j]];
                    Console.Write(this.board[i, j]);
                    Console.WriteLine();
                }
            }
        }
    }
}