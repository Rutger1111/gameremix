using System.IO;

namespace GameRemixForLinux
{
    internal static class PathHelper
    {
        internal static string ExeDir()
        {
            return new FileInfo(typeof(PathHelper).Assembly.Location).DirectoryName;
        }
    }
}




