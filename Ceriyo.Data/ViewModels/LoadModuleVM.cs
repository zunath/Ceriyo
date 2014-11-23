﻿using System.ComponentModel;

namespace Ceriyo.Data.ViewModels
{
    public class LoadModuleVM : BaseVM
    {
        private BindingList<string> _fileNames;
        private string _selectedFile;

        public BindingList<string> Files
        {
            get { return _fileNames; }
            set
            {
                _fileNames = value;
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

        public LoadModuleVM()
        {
            Files = new BindingList<string>();
            SelectedFile = string.Empty;
        }
    }
}
