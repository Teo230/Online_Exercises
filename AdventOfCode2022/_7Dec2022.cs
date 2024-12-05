using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Online_Exercises.AdventOfCode2022
{
    public static class _7Dec2022
    {
        private static CDir _tree = new CDir()
        {
            Name = "root",
        };

        public static void Part1()
        {
            var stringText = File.ReadAllLines("AdventOfCode2022/7Dec2022.txt");

            CreateTree(stringText);
            TotalValidDirs(_tree);

            Console.WriteLine("1 - Total dir size {0}", _totalDirSize);
        }

        private static void CreateTree(string[] stringText)
        {
            CDir currentDir = _tree;
            for (int i = 0; i < stringText.Length; i++)
            {
                var currentLine = stringText[i];

                bool isCommand = currentLine.StartsWith('$');
                if (isCommand)
                {
                    var isCd = currentLine.Substring(2).StartsWith("cd");
                    if (isCd) currentDir = ChangeCDir(currentDir, currentLine.Substring(5));

                    var isLs = currentLine.Substring(2).StartsWith("ls");
                    if (isLs) ListCDir(currentDir, stringText[(i + 1)..]);
                }
            }
        }

        private static void ListCDir(CDir currentDir, string[] nextValues)
        {
            var index = 0;
            var currentValue = nextValues[index];
            while (!currentValue.StartsWith("$"))
            {
                var isDir = currentValue.StartsWith("dir");
                if (isDir)
                {
                    var dirName = currentValue.Substring(4);

                    currentDir.Dirs.Add(new()
                    {
                        ParentCDir = currentDir,
                        Name = dirName,
                    });
                }
                else
                {
                    int fileSize = int.Parse(currentValue.Split(' ')[0]);
                    string fileName = currentValue.Split(' ')[1];
                    currentDir.Files.Add(new()
                    {
                        Name = fileName,
                        Size = fileSize,
                    });
                }

                if (nextValues.Length == 1)
                {
                    currentValue = nextValues[0];
                    break;
                }
                else
                    index++;

                currentValue = nextValues[index];
            }
        }

        private static CDir ChangeCDir(CDir currentDir, string destCDirName)
        {
            if (destCDirName == "/")
            {
                return _tree;
            }
            else if (destCDirName == "..")
            {
                return currentDir.ParentCDir;
            }
            else
            {
                var existingCDir = currentDir.Dirs.FirstOrDefault(x => x.Name == destCDirName);
                if (existingCDir == null)
                {
                    var newDir = new CDir();
                    newDir.Name = destCDirName;
                    newDir.ParentCDir = currentDir;
                    return newDir;
                }
                else
                {
                    return existingCDir;
                }
            }
        }

        private static int _totalDirSize = 0;
        private static void TotalValidDirs(CDir dir)
        {
            if (dir.ValidDirSize) _totalDirSize += dir.Size;
            foreach (var subDir in dir.Dirs)
            {
                TotalValidDirs(subDir);
            }
        }

        public static void Part2()
        {
            var stringText = File.ReadAllLines("AdventOfCode2022/7Dec2022.txt");

            CreateTree(stringText);
            _maxSpaceRange = _tree.Size - 40_000_000;
            var result = RetrieveSmallestUnsedSpace(_tree);

            Console.WriteLine("2 - Total dir size {0}", result);
        }

        private static int _maxSpaceRange = 0;
        private static int RetrieveSmallestUnsedSpace(CDir dir) // TODO - It's not over till it's over
        {
            var _result = int.MaxValue;
            if(dir.Size >= _maxSpaceRange)
                _result = dir.Size;

            foreach(var subDir in dir.Dirs)
            {
                var subDirSize = RetrieveSmallestUnsedSpace(subDir);
                _result = Math.Min(_result, subDirSize);
            }

            return _result;
        }
    }

    public class CFile
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }

    public class CDir
    {
        private static int _maxDirSize = 100_000;

        public CDir ParentCDir { get; set; }
        public string Name { get; set; }
        public int Size => Files.Sum(x => x.Size) + Dirs.Sum(x => x.Size);
        public bool ValidDirSize => Size <= _maxDirSize;
        public List<CFile> Files { get; set; } = new();
        public List<CDir> Dirs { get; set; } = new();
    }
}
