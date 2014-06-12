using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.ViewModels
{
    public class SaveScriptVM : BaseVM
    {
        private string _fileName;
        private string _contents;
        private string _oldFileName;

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        public string Contents
        {
            get
            {
                return _contents;
            }
            set
            {
                _contents = value;
                OnPropertyChanged("Contents");
            }
        }

        public string OldFileName
        {
            get
            {
                return _oldFileName;
            }
            set
            {
                _oldFileName = value;
                OnPropertyChanged("OldFileName");
            }
        }

        public SaveScriptVM()
        {
            this.FileName = "";
            this.Contents = "";
            this.OldFileName = "";
        }
    }
}
