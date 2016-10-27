using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DirtyMergeSort;
using MVVMBase;

namespace BigSort.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            var lst1 = new List<object>();
            var lst2 = new List<object>();

            lst1.Add(1);
            lst2.Add(1);

            var x = lst1[0] == lst2[0];
            var y = lst1[0] == lst1[0];
        }

        

        private bool isFileGenerateInProgress = false;
        private object sync = new object();

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CreateFileCommand));
                OnPropertyChanged(nameof(SortFileCommand));
            }
        }
        


        private int fileSize = 2;

        public int FileSize
        {
            get { return fileSize; }
            set
            {
                fileSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CreateFileCommand));
                OnPropertyChanged(nameof(SortFileCommand));
            }
        }


        private int progress;

        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateFileCommand
        {
            get
            {
                return new DelegateCommand(async executedParam =>
                {
                    if (isFileGenerateInProgress)
                        return;

                    isFileGenerateInProgress = true;

                    OnPropertyChanged(nameof(CreateFileCommand));

                    string path = "d:\\WriteLines.txt";
                    await GenerateFile(path);
                    isFileGenerateInProgress = false;

                },
                canExecutedParam => { return !string.IsNullOrEmpty(FilePath) && FileSize > 0 && !isFileGenerateInProgress; });
            }
        }

        public ICommand SortFileCommand
        {
            get
            {
                return new DelegateCommand(async executedParam =>
                {
                    string path = "d:\\WriteLines.txt";
                    await SortFile(path);
                },
                canExecutedParam => { return !string.IsNullOrEmpty(FilePath); });
            }
        }

        public int GeteFileSize(string path)
        {                                                                //KB   //MB   //GB      
            return Convert.ToInt32(new System.IO.FileInfo(path).Length / 1024 / 1024 / 1024);
        }

        public int GeteFileSizeMB(string path)
        {                                                                //KB   //MB        
            return Convert.ToInt32(new System.IO.FileInfo(path).Length / 1024 / 1024);
        }

        public bool GetFreeSize(string path)
        {                                                                    
            DriveInfo driveInfo = new DriveInfo(path.Substring(0,2));
            return (driveInfo.AvailableFreeSpace/1024/1024) > 500;
        }

        public async Task GenerateFile(string path)
        {
            Debug.WriteLine("GenerateFile - Start");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int oldSize = -1;
            Progress = 0;
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                while (GetFreeSize(path) && GeteFileSize(path) < FileSize)
                {
                    Progress = (FileSize * 1024)  * GeteFileSizeMB(path) / 100;

                    StringBuilder sb = new StringBuilder();
                    int lines = 0;
                    foreach (var item in Helper.GetRandomValue(5000))
                    {
                        lines++;
                        sb.AppendLine(item);

                        if(lines > 1000)
                        {
                            await outputFile.WriteLineAsync(sb.ToString());
                            sb.Clear();
                            lines = 0;
                        }
                        
                    }

                    if(sb.Length > 0)
                    {
                        await outputFile.WriteLineAsync(sb.ToString());
                        sb.Clear();
                    }

                    var newSize = GeteFileSize(path);
                    if(newSize > oldSize)
                    {
                        Debug.WriteLine("size:" + newSize);
                        oldSize = newSize;
                    }
                    
                }
            }

            sw.Stop();

            if (!GetFreeSize(path))
            {
                MessageBox.Show("Disk if full :(");
            }

            MessageBox.Show("File ready - size is: " + GeteFileSize(path));
            Debug.WriteLine("GenerateFile - Stop; Total:"+sw.Elapsed);
        }

        public async Task SortFile(string path)
        {
            Sort(@"d:\sorted.txt", @"d:\3gb.txt");
        }

        public void Sort(string sortedPath, string dataPath)
        {
            File.WriteAllLines(sortedPath,
                File.ReadLines(dataPath)
                    .Select(ProgressUpdate("read"))
                    .Sorted(1000000)
                    .Select(ProgressUpdate("write")));
        }

        private Func<string, int, string> ProgressUpdate(string type)
        {
            return (x, i) =>
            {
                if (i % 100000 == 0) Debug.WriteLine("{0}: {1}", type, i);
                return x;
            };
        }
    }
}
