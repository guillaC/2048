namespace _02048
{
    using System;
    using System.Collections.Generic;

    internal class IA
    {
        private int size = 4;
        private int[,] IAboard;
        private int bestScore = 0;
        private Direction bestDirection;
        private Dictionary<Direction, ConsoleKey> directionKey = new Dictionary<Direction, ConsoleKey>();
        private Random rnd = new Random();

        public IA(int[,] board)
        {
            this.directionKey.Add(Direction.Up, ConsoleKey.UpArrow);
            this.directionKey.Add(Direction.Down, ConsoleKey.DownArrow);
            this.directionKey.Add(Direction.Left, ConsoleKey.LeftArrow);
            this.directionKey.Add(Direction.Right, ConsoleKey.RightArrow);

            this.IAboard = board;
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                this.GetBestMove(direction, this.IAboard);
            }
        }

        private enum Direction
        {
            Up, Down, Left, Right, Rand
        };

        public ConsoleKey returnBest()
        {
            if (this.bestDirection == Direction.Rand)
            {
                this.bestDirection = (Direction)this.rnd.Next(0, 4);
            }
            System.Diagnostics.Debug.WriteLine(this.directionKey[this.bestDirection]);
            return this.directionKey[this.bestDirection];
        }

        private void GetBestMove(Direction direction, int[,] board)
        {
            int score = 0;

            if (direction == Direction.Rand)
            {
                score = 1;
            }
            else
            {
                for (int k = 0; k < size; k++)
                {
                    if (direction == Direction.Up)
                    {
                        for (int i = size - 1; i > 0; i--)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                if (this.IAboard[i - 1, j] == this.IAboard[i, j] && k == 0)
                                {
                                    this.IAboard[i - 1, j] = this.IAboard[i - 1, j] * 2;
                                    score += this.IAboard[i - 1, j];
                                    this.IAboard[i, j] = 0;
                                }

                                if (this.IAboard[i - 1, j] == 0)
                                {
                                    this.IAboard[i - 1, j] = this.IAboard[i, j];
                                    this.IAboard[i, j] = 0;
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
                                if (this.IAboard[i + 1, j] == this.IAboard[i, j] && k == 0)
                                {
                                    this.IAboard[i + 1, j] = this.IAboard[i + 1, j] * 2;
                                    score += this.IAboard[i + 1, j];
                                    this.IAboard[i, j] = 0;
                                }

                                if (this.IAboard[i + 1, j] == 0)
                                {
                                    this.IAboard[i + 1, j] = this.IAboard[i, j];
                                    this.IAboard[i, j] = 0;
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
                                if (this.IAboard[i, j - 1] == this.IAboard[i, j] && k == 0)
                                {
                                    this.IAboard[i, j - 1] = this.IAboard[i, j - 1] * 2;
                                    score += this.IAboard[i, j - 1];
                                    this.IAboard[i, j] = 0;
                                }

                                if (this.IAboard[i, j - 1] == 0)
                                {
                                    this.IAboard[i, j - 1] = this.IAboard[i, j];
                                    this.IAboard[i, j] = 0;
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
                                if (this.IAboard[i, j + 1] == this.IAboard[i, j] && k == 0)
                                {
                                    this.IAboard[i, j + 1] = this.IAboard[i, j + 1] * 2;
                                    score += this.IAboard[i, j + 1];
                                    this.IAboard[i, j] = 0;
                                }

                                if (this.IAboard[i, j + 1] == 0)
                                {
                                    this.IAboard[i, j + 1] = this.IAboard[i, j];
                                    this.IAboard[i, j] = 0;
                                }
                            }
                        }
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(direction + "->" + score);

            if (this.bestScore < score)
            {
                this.bestScore = score;
                this.bestDirection = direction;
            }
        }
    }
}