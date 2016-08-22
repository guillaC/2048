using System;

namespace _2048
{
    internal class Board
    {
        public int size = 4;
        private int[,] board = new int[4, 4];
        private readonly Random rnd = new Random();
        private readonly Game game;
        private bool useful = false;

        public Board(Game GameInstance)
        {
            game = GameInstance;
            board[1, 2] = board[0, 3] = 2;
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public void Move(Direction direction)
        {
            useful = false;
            switch (direction)
            {
                case Direction.Up:
                    for (var k = 0; k < size; k++)
                    {
                        for (var i = size - 1; i > 0; i--)
                        {
                            for (var j = 0; j < size; j++)
                            {
                                if (board[i - 1, j] == 0)
                                {
                                    board[i - 1, j] = board[i, j];
                                    board[i, j] = 0;
                                    useful = true;
                                }
                            }
                        }
                    }

                    for (var i = size - 1; i > 0; i--)
                    {
                        for (var j = 0; j < size; j++)
                        {
                            if (board[i - 1, j] == board[i, j])
                            {
                                board[i - 1, j] = board[i - 1, j] * 2;
                                board[i, j] = 0;
                                game.IncScore(board[i - 1, j]);
                                useful = true;
                            }
                        }
                    }

                    for (var i = size - 1; i > 0; i--)
                    {
                        for (var j = 0; j < size; j++)
                        {
                            if (board[i - 1, j] == 0)
                            {
                                board[i - 1, j] = board[i, j];
                                board[i, j] = 0;
                                useful = true;
                            }
                        }
                    }

                    break;

                case Direction.Down:
                    for (var k = 0; k < size; k++)
                    {
                        for (var i = size - 2; i > -1; i--)
                        {
                            for (var j = 0; j < size; j++)
                            {
                                if (board[i + 1, j] == 0)
                                {
                                    board[i + 1, j] = board[i, j];
                                    board[i, j] = 0;
                                    useful = true;
                                }
                            }
                        }
                    }

                    for (var i = size - 2; i > -1; i--)
                    {
                        for (var j = 0; j < size; j++)
                        {
                            if (board[i + 1, j] == board[i, j])
                            {
                                board[i + 1, j] = board[i + 1, j] * 2;
                                board[i, j] = 0;
                                game.IncScore(board[i + 1, j]);
                                useful = true;
                            }
                        }
                    }

                    for (var i = size - 2; i > -1; i--)
                    {
                        for (var j = 0; j < size; j++)
                        {
                            if (board[i + 1, j] == 0)
                            {
                                board[i + 1, j] = board[i, j];
                                board[i, j] = 0;
                                useful = true;
                            }
                        }
                    }

                    break;

                case Direction.Left:
                    for (var k = 0; k < size; k++)
                    {
                        for (var j = size - 1; j > 0; j--)
                        {
                            for (var i = 0; i < size; i++)
                            {
                                if (board[i, j - 1] == 0)
                                {
                                    board[i, j - 1] = board[i, j];
                                    board[i, j] = 0;
                                    useful = true;
                                }
                            }
                        }
                    }

                    for (var j = size - 1; j > 0; j--)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            if (board[i, j - 1] == board[i, j])
                            {
                                board[i, j - 1] = board[i, j - 1] * 2;
                                board[i, j] = 0;
                                game.IncScore(board[i, j - 1]);
                                useful = true;
                            }
                        }
                    }

                    for (var j = size - 1; j > 0; j--)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            if (board[i, j - 1] == 0)
                            {
                                board[i, j - 1] = board[i, j];
                                board[i, j] = 0;
                                useful = true;
                            }
                        }
                    }

                    break;

                case Direction.Right:
                    for (var k = 0; k < size; k++)
                    {
                        for (var j = size - 2; j > -1; j--)
                        {
                            for (var i = 0; i < size; i++)
                            {
                                if (board[i, j + 1] == 0)
                                {
                                    board[i, j + 1] = board[i, j];
                                    board[i, j] = 0;
                                    useful = true;
                                }
                            }
                        }
                    }

                    for (var j = size - 2; j > -1; j--)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            if (board[i, j + 1] == board[i, j])
                            {
                                board[i, j + 1] = board[i, j + 1] * 2;
                                board[i, j] = 0;
                                game.IncScore(board[i, j + 1]);
                                useful = true;
                            }
                        }
                    }

                    for (var j = size - 2; j > -1; j--)
                    {
                        for (var i = 0; i < size; i++)
                        {
                            if (board[i, j + 1] == 0)
                            {
                                board[i, j + 1] = board[i, j];
                                board[i, j] = 0;
                                useful = true;
                            }
                        }
                    }

                    break;
            }
            if (useful)
            {
                game.IncMove();
            }
        }

        public void AddRandom()
        {
            int randCol, randRow;
            if (!game.CheckWin())
            {
                if (game.CheckLose())
                {
                    if (useful)
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
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lose.");
                    Console.ReadLine();
                    game.newGame();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Win.");
                Console.ReadLine();
                game.newGame();
            }
        }
    }
}