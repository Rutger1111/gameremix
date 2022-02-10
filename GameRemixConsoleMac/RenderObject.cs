using System;

namespace GameRemixConsoleMac
{
    public class RenderObject
    {
        public int[] position;
        public char graphic;
        public ConsoleColor color = ConsoleColor.Green;

        public override string ToString()
        {
            return $"{position[0]} {position[1]} '{graphic}'";
        }
    }
}

