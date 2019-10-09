using MainApp.Engine;
using System;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Run(60);
        }
    }
}
