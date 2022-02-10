using System.IO;

namespace GameRemixForLinux
{
    internal class FileLevelDataSource : ILevelDataSource
    {
        public string[] GetLines(int roomX, int roomY)
        {
            string dir = new FileInfo(GetType().Assembly.Location).DirectoryName;
            string path = Path.Combine(dir, "leveldata", $"room.{roomX}.{roomY}.txt");
            return File.ReadAllLines(path);
        }
    }
}




