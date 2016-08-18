namespace _02048
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class Game
    {
        private readonly Dictionary<int, ConsoleColor> colorInt = new Dictionary<int, ConsoleColor>();
        private readonly Random rnd = new Random();
        private readonly int winValue = 2048;
        private readonly int size = 4;
        private int[,] board = new int[4, 4];
        private bool auto;

        public Game()
        {
            colorInt.Add(0, ConsoleColor.White);
            colorInt.Add(2, ConsoleColor.Blue);
            colorInt.Add(4, ConsoleColor.Cyan);
            colorInt.Add(8, ConsoleColor.Green);
            colorInt.Add(16, ConsoleColor.Magenta);
            colorInt.Add(32, ConsoleColor.Red);
            colorInt.Add(64, ConsoleColor.Yellow);
            colorInt.Add(128, ConsoleColor.White);
            colorInt.Add(256, ConsoleColor.Blue);
            colorInt.Add(512, ConsoleColor.Cyan);
            colorInt.Add(1024, ConsoleColor.Green);
            colorInt.Add(2048, ConsoleColor.Magenta);

            AddRandom();
            AddRandom();
            ConsoleUpdate();

            var threadAuto = new Thread(() =>
            {
                while (true)
                {
                    if (auto)
                    {
                        Thread.Sleep(2000);
                        AutoPlay();
                    }
                }
            });

            threadAuto.Start();
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public void UserInput(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        Move(Direction.Up);
                        break;

                    case ConsoleKey.DownArrow:
                        Move(Direction.Down);
                        break;

                    case ConsoleKey.LeftArrow:
                        Move(Direction.Left);
                        break;

                    case ConsoleKey.RightArrow:
                        Move(Direction.Right);
                        break;
                }

                AddRandom();
                ConsoleUpdate();
            }
            else
            {
                if (key == ConsoleKey.Spacebar)
                {
                    auto = !auto;
                }
            }
        }

        private void Move(Direction direction)
        {
            for (var k = 0; k < size; k++)
            {
                switch (direction)
                {
                    case Direction.Up:
                        for (var i = size - 1; i > 0; i--)
                        {
                            for (var j = 0; j < size; j++)
                            {
                                if (board[i - 1, j] == board[i, j] && k == 0)
                                {
                                    board[i - 1, j] = board[i - 1, j] * 2;
                                    board[i, j] = 0;
                                }

                                if (board[i - 1, j] == 0)
                                {
                                    board[i - 1, j] = board[i, j];
                                    board[i, j] = 0;
                                }
                            }
                        }

                        break;

                    case Direction.Down:
                        for (var i = 0; i < size - 1; i++)
                        {
                            for (var j = 0; j < size; j++)
                            {
                                if (board[i + 1, j] == board[i, j] && k == 0)
                                {
                                    board[i + 1, j] = board[i + 1, j] * 2;
                                    board[i, j] = 0;
                                }

                                if (board[i + 1, j] == 0)
                                {
                                    board[i + 1, j] = board[i, j];
                                    board[i, j] = 0;
                                }
                            }
                        }

                        break;

                    case Direction.Left:
                        for (var j = size - 1; j > 0; j--)
                        {
                            for (var i = 0; i < size; i++)
                            {
                                if (board[i, j - 1] == board[i, j] && k == 0)
                                {
                                    board[i, j - 1] = board[i, j - 1] * 2;
                                    board[i, j] = 0;
                                }

                                if (board[i, j - 1] == 0)
                                {
                                    board[i, j - 1] = board[i, j];
                                    board[i, j] = 0;
                                }
                            }
                        }

                        break;

                    case Direction.Right:
                        for (var j = 0; j < size - 1; j++)
                        {
                            for (var i = 0; i < size; i++)
                            {
                                if (board[i, j + 1] == board[i, j] && k == 0)
                                {
                                    board[i, j + 1] = board[i, j + 1] * 2;
                                    board[i, j] = 0;
                                }

                                if (board[i, j + 1] == 0)
                                {
                                    board[i, j + 1] = board[i, j];
                                    board[i, j] = 0;
                                }
                            }
                        }

                        break;
                }
            }
        }

        private void AutoPlay()
        {
            var ia = new IA(board);
            var key = ia.returnBest();
            UserInput(key);
        }

        private void AddRandom()
        {
            int randCol, randRow;
            if (!CheckWin())
            {
                if (CheckLose())
                {
                    randCol = rnd.Next(0, size);
                    randRow = rnd.Next(0, size);
                    while (board[randCol, randRow] != 0)
                    {
                        randCol = rnd.Next(0, size);
                        randRow = rnd.Next(0, size);
                    }

                    board[randCol, randRow] = 2;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lose.");
                    Console.ReadLine();
                    board = new int[size, size];
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Win.");
                Console.ReadLine();
                board = new int[size, size];
            }
        }

        private bool CheckWin()
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (board[i, j] == winValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckLose()
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (board[i, j] == 0)
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
            for (var i = 0; i != size; i++)
            {
                for (var j = 0; j != size; j++)
                {
                    Console.SetCursorPosition(j * 5, i * 2);
                    Console.ForegroundColor = colorInt[board[i, j]];
                    Console.Write(board[i, j]);
                    Console.WriteLine();
                }
            }
        }
    }
}
