using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MVVMBase;

namespace BigSort.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
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
                return new DelegateCommand(executedParam =>
                {
                    // Do something
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
            int localFilesite = FileSize*1024*1024;
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                while (GetFreeSize(path) && GeteFileSize(path) < FileSize)
                {
                    Progress = localFilesite * GeteFileSizeMB(path) / 100;

                    foreach (var item in Helper.WordsDictionary)
                    {
                        await outputFile.WriteLineAsync(item.Key + "." + item.Value);
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
    }
}
