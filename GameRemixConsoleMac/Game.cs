using System;
using System.Collections.Generic;
using System.Linq;

namespace GameRemixConsoleMac
{
    public class Game
    {

        private readonly LevelLoader levelLoader = new();
        private readonly RenderObject player = new();
        private Room room;
        public Game()

        {
            room = levelLoader.Load(0, 0);

            player = new()
            {
                position = new int[] { 2, 2 }
            };
        }

        internal void RenderWithConsole()
        {
            for (int i = 0; i < room.tiles.Length; i++)
            {
                Console.Write(' ');
            }
            Console.WriteLine();
            for (int i = 0; i < room.tiles.Length; i++)
            {
                for (int i2 = 0; i2 < room.tiles[i].Length; i2++)
                {
                    if (i2 == player.position[0] && i == player.position[1])
                    {
                        Console.ForegroundColor = player.color;
                        Console.Write('c');
                    }
                    else
                    {
                        char graphic = room.tiles[i][i2].graphic;
                        Console.ForegroundColor = room.tiles[i][i2].color;
                        Console.Write(graphic);
                    }
                }
                Console.WriteLine();
            }
        }

        RenderObject GetNextTile(int newx, int newy)
        {
            for (int i = 0; i < room.tiles.Length; i++)
            {
                for (int i2 = 0; i2 < room.tiles[i].Length; i2++)
                {
                    RenderObject t = room.tiles[i][i2];
                    if (t.position[0] == newx && t.position[1] == newy)
                    {
                        return t;
                    }
                }
            }
            return null;
        }
        internal void MovePlayer(int x, int y)
        {
            int newx = player.position[0] + x;
            int newy = player.position[1] + y;

            RenderObject next = GetNextTile(newx, newy);

            if (next != null)
            {
                if (next.graphic == 'D')
                {
                    LoadNextRoom(x, y);
                }
                else if (next.graphic != '#')
                {

                    player.position[0] = newx;
                    player.position[1] = newy;
                }

            }
        }

        private void LoadNextRoom(int x, int y)
        {
            room = levelLoader.Load(room.roomx + x, room.roomy + y);
            if (x > 0)
            {
                player.position[0] = 1;

            }
            else if (x < 0)
            {
                player.position[0] = room.tiles[0].Length - 2;

            }
            if (y > 0)
            {
                player.position[1] = 1;

            }
            else if (y < 0)
            {
                player.position[1] = room.tiles.Length - 2;
            }
        }
    }
}

