using System;
using System.Threading;

namespace _2048
{
    internal class Human : Player
    {
        public Human(Game GameInstance, Board BoardInstance)
            : base(GameInstance, BoardInstance)
        {
            ConsoleKeyInfo keyInfo;
            var threadInput = new Thread(() =>
            {
                while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
                {
                    UserInput(keyInfo.Key);
                }
            });
            threadInput.Start();
        }
    }
}