using System.IO;

namespace GameRemix
{
    internal static class PathHelper
    {
        internal static string ExeDir()
        {
            return new FileInfo(typeof(PathHelper).Assembly.Location).DirectoryName;
        }
    }
}




