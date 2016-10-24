using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    // Do something
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

    }
}
