using System;

namespace _2048
{
    internal class Player
    {
        private readonly Board BoardInstance;
        private readonly Game GameInstance;

        public Player(Game GameInstance, Board BoardInstance)
        {
            this.BoardInstance = BoardInstance;
            this.GameInstance = GameInstance;
        }

        public void UserInput(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        BoardInstance.Move(Board.Direction.Up);
                        break;

                    case ConsoleKey.DownArrow:
                        BoardInstance.Move(Board.Direction.Down);
                        break;

                    case ConsoleKey.LeftArrow:
                        BoardInstance.Move(Board.Direction.Left);
                        break;

                    case ConsoleKey.RightArrow:
                        BoardInstance.Move(Board.Direction.Right);
                        break;
                }

                BoardInstance.AddRandom();
                GameInstance.ConsoleUpdate();
            }
        }
    }
}