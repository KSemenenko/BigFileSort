using System;
using System.Collections.Generic;
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


        private int fileSize;

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

        public ICommand CreateFileCommand
        {
            get
            {
                return new DelegateCommand(executedParam =>
                {
                    string path = "d:\\WriteLines.txt";

                    using (StreamWriter outputFile = new StreamWriter(path))
                    {
                        while(GetFreeSize(path) && GeteFileSize(path) < FileSize)
                        { 
                            foreach (var item in Helper.WordsDictionary)
                            {
                                outputFile.WriteLine(item.Key+"."+item.Value);
                            }
              
                            Debug.WriteLine("size:"+ GeteFileSize(path));
                        }

                    }

                    MessageBox.Show("ok");

                },
                canExecutedParam => { return !string.IsNullOrEmpty(FilePath) && FileSize > 0; });
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

        public bool GetFreeSize(string path)
        {                                                                    
            DriveInfo driveInfo = new DriveInfo(path.Substring(0,2));
            return (driveInfo.AvailableFreeSpace/1024/1024) > 500;
        }

    }
}
