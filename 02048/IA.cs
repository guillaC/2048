namespace _02048
{
    using System;
    using System.Collections.Generic;

    internal class IA
    {
        private readonly int size = 4;
        private readonly int[,] IAboard;
        private int bestScore;
        private Direction bestDirection;
        private readonly Dictionary<Direction, ConsoleKey> directionKey = new Dictionary<Direction, ConsoleKey>();
        private readonly Random rnd = new Random();

        public IA(int[,] board)
        {
            directionKey.Add(Direction.Up, ConsoleKey.UpArrow);
            directionKey.Add(Direction.Down, ConsoleKey.DownArrow);
            directionKey.Add(Direction.Left, ConsoleKey.LeftArrow);
            directionKey.Add(Direction.Right, ConsoleKey.RightArrow);

            IAboard = board;
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                GetBestMove(direction, IAboard);
            }
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            Rand
        };

        public ConsoleKey returnBest()
        {
            if (bestDirection == Direction.Rand)
            {
                bestDirection = (Direction)rnd.Next(0, 4);
            }
            System.Diagnostics.Debug.WriteLine(directionKey[bestDirection]);
            return directionKey[bestDirection];
        }

        private void GetBestMove(Direction direction, int[,] board)
        {
            var score = 0;

            if (direction == Direction.Rand)
            {
                score = 1;
            }
            else
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
                                    if (IAboard[i - 1, j] == IAboard[i, j] && k == 0)
                                    {
                                        IAboard[i - 1, j] = IAboard[i - 1, j] * 2;
                                        score += IAboard[i - 1, j];
                                        IAboard[i, j] = 0;
                                    }

                                    if (IAboard[i - 1, j] == 0)
                                    {
                                        IAboard[i - 1, j] = IAboard[i, j];
                                        IAboard[i, j] = 0;
                                    }
                                }
                            }

                            break;
                        case Direction.Down:
                            for (var i = 0; i < size - 1; i++)
                            {
                                for (var j = 0; j < size; j++)
                                {
                                    if (IAboard[i + 1, j] == IAboard[i, j] && k == 0)
                                    {
                                        IAboard[i + 1, j] = IAboard[i + 1, j] * 2;
                                        score += IAboard[i + 1, j];
                                        IAboard[i, j] = 0;
                                    }

                                    if (IAboard[i + 1, j] == 0)
                                    {
                                        IAboard[i + 1, j] = IAboard[i, j];
                                        IAboard[i, j] = 0;
                                    }
                                }
                            }

                            break;
                        case Direction.Left:
                            for (var j = size - 1; j > 0; j--)
                            {
                                for (var i = 0; i < size; i++)
                                {
                                    if (IAboard[i, j - 1] == IAboard[i, j] && k == 0)
                                    {
                                        IAboard[i, j - 1] = IAboard[i, j - 1] * 2;
                                        score += IAboard[i, j - 1];
                                        IAboard[i, j] = 0;
                                    }

                                    if (IAboard[i, j - 1] == 0)
                                    {
                                        IAboard[i, j - 1] = IAboard[i, j];
                                        IAboard[i, j] = 0;
                                    }
                                }
                            }

                            break;
                        case Direction.Right:
                            for (var j = 0; j < size - 1; j++)
                            {
                                for (var i = 0; i < size; i++)
                                {
                                    if (IAboard[i, j + 1] == IAboard[i, j] && k == 0)
                                    {
                                        IAboard[i, j + 1] = IAboard[i, j + 1] * 2;
                                        score += IAboard[i, j + 1];
                                        IAboard[i, j] = 0;
                                    }

                                    if (IAboard[i, j + 1] == 0)
                                    {
                                        IAboard[i, j + 1] = IAboard[i, j];
                                        IAboard[i, j] = 0;
                                    }
                                }
                            }

                            break;
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(direction + "->" + score);

            if (bestScore < score)
            {
                bestScore = score;
                bestDirection = direction;
            }
        }
    }
}
