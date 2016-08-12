namespace _02048
{
    using System;
    using System.Collections.Generic;

    internal class Game
    {
        private int[,] board = new int[4, 4];
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
            this.ConsoleUpdate();
        }
        private enum Direction { Up, Down, Left, Right };

        private void Move(Direction direction)
        {
            for (int k = 0; k < 4; k++)
            {
                if (direction == Direction.Up)
                {
                    for (int i = 3; i > 0; i--)
                    {
                        for (int j = 0; j < 4; j++)
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
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 4; j++)
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
                    for (int j = 3; j > 0; j--)
                    {
                        for (int i = 0; i < 4; i++)
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
                    for (int j = 0; j < 3; j++)
                    {
                        for (int i = 0; i < 4; i++)
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
                if (key == ConsoleKey.UpArrow)
                {
                    this.Move(Direction.Up);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    this.Move(Direction.Down);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    this.Move(Direction.Left);
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    this.Move(Direction.Right);
                }

                this.AddRandom();
                this.ConsoleUpdate();
            }
        }

        private void AddRandom()
        {
            int randCol, randRow;
            if (!this.CheckWin())
            {
                if (this.CheckLose())
                {
                    randCol = this.rnd.Next(0, 4);
                    randRow = this.rnd.Next(0, 4);
                    while (this.board[randCol, randRow] != 0)
                    {
                        randCol = this.rnd.Next(0, 4);
                        randRow = this.rnd.Next(0, 4);
                    }

                    this.board[randCol, randRow] = 2;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lose.");
                    this.board = new int[4, 4];
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Win.");
                this.board = new int[4, 4];
            }
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (this.board[i, j] == 2048)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckLose()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
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
            for (int i = 0; i != 4; i++)
            {
                for (int j = 0; j != 4; j++)
                {
                    Console.ForegroundColor = this.colorInt[this.board[i, j]];
                    Console.Write(" " + this.board[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}