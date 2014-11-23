using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class SaveModuleVM : BaseVM
    {
        private BindingList<string> _files; 
        private string _selectedFile;
        private string _fileName;

        public BindingList<string> Files
        {
            get { return _files; }
            set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }

        public string SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                OnPropertyChanged("SelectedFile");
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }


        public SaveModuleVM()
        {
            Files = new BindingList<string>();
            SelectedFile = string.Empty;
            FileName = string.Empty;
        }
    }
}
