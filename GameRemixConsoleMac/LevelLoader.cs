
using System;
using System.Collections.Generic;
using System.IO;

namespace GameRemixConsoleMac
{
    public class LevelLoader
    {

        public LevelLoader()
        {
        }
        public Room Load(int roomX, int roomY)
        {
            Room room = new()
            {
                roomx = roomX,
                roomy = roomY
            };


            //string[] lines = new string[] {
            //    "################",
            //    "#..............#",
            //    "#..............#",
            //    "#..............#",
            //    "#..............#",
            //    "#..............#",
            //    "#..............#",
            //    "#..............#",
            // "################"};

            //room.tiles = new RenderObject[lines.Length][];
           string[] lines = File.ReadAllLines(Path.Combine("leveldata", $"room-{roomX}-{roomY}.txt"));
            room.tiles = new RenderObject[lines.Length][];
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                room.tiles[y] = new RenderObject[line.Length];
                for (int x = 0; x < room.tiles[y].Length; x++)
                {
                    room.tiles[y][x] = new RenderObject()
                    {
                        graphic = line[x],
                        position = new int[] { x, y },
                        //sprite = tileMap[line[x]]
                    };
                    if (line[x] == '#')
                    {
                        room.tiles[y][x].color = ConsoleColor.DarkMagenta;
                    }
                    else
                    {
                        room.tiles[y][x].color = ConsoleColor.Gray;
                    }
                }
            }
            //}
            return room;
        }

    }
}

