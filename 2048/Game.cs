using System;

namespace _2048
{
    internal class Game
    {
        private Board board;
        private Human human;
        private int nbmove = 0;
        private int score = 0;

        public void newGame()
        {
            board = new Board(this);
            human = new Human(this, board);
            ConsoleUpdate();
        }

        public bool CheckWin()
        {
            var currentBoard = board.GetBoard();
            for (var i = 0; i < board.size; i++)
            {
                for (var j = 0; j < board.size; j++)
                {
                    if (currentBoard[i, j] == 2048)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void IncScore(int nb)
        {
            score += nb;
        }

        public void IncMove()
        {
            nbmove++;
        }

        public bool CheckLose()
        {
            var currentBoard = board.GetBoard();
            for (var i = 0; i < board.size; i++)
            {
                for (var j = 0; j < board.size; j++)
                {
                    if (currentBoard[i, j] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ConsoleUpdate()
        {
            var currentBoard = board.GetBoard();
            Console.Clear();
            for (var i = 0; i != board.size; i++)
            {
                for (var j = 0; j != board.size; j++)
                {
                    Console.SetCursorPosition(j * 5, i * 2);

                    Console.Write(currentBoard[i, j]);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("nbmove: " + nbmove);
            Console.WriteLine("score: " + score);
        }
    }
}