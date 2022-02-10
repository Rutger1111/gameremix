using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameRemixConsoleMac
{
    class Program
    {
        private readonly int left;
        private readonly int top;
        private Game game;
        static void Main()
        {
            Program p = new();
            p.MainLoop();
        }

        public Program()
        {
            left = Console.CursorLeft;
            top = Console.CursorTop;
            Console.CursorVisible = false;
            game = new Game();

            ReadInput(left, top);
        }

        private void MainLoop()
        {
            while (true)
            {
                Console.Clear();
                game.RenderWithConsole();

                //Console.SetCursorPosition(left, top);
                Thread.Sleep(100);
            }
        }

       
        private void ReadInput(int left, int top)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(false);
                    //Console.Clear();
                    //Console.SetCursorPosition(left, top);

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        game.MovePlayer(0, -1);
                    }
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        game.MovePlayer(0, 1);
                    }

                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        game.MovePlayer(-1, 0);
                    }
                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        game.MovePlayer(1, 0);
                    }

                }
            });
        }


    }
}

