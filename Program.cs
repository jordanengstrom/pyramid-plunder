using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            System.Console.Clear();
            Game newGame = new Game();
            while (newGame.playing)
            {
                newGame.Menu();
            }
        }
    }
}
