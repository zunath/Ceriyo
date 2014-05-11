using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class EditDialogVM : BaseVM
    {
        private string _name;
        private string _tag;
        private string _resref;
        private BindingList<string> _scripts;
        private BindingList<LocalVariable> _localVariables;
        private string _description;
        private string _comments;



        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
                OnPropertyChanged("Tag");
            }
        }

        public string Resref
        {
            get
            {
                return _resref;
            }
            set
            {
                _resref = value;
                OnPropertyChanged("Resref");
            }
        }

        public BindingList<string> Scripts
        {
            get
            {
                return _scripts;
            }
            set
            {
                _scripts = value;
                OnPropertyChanged("Scripts");
            }
        }

        public BindingList<LocalVariable> LocalVariables
        {
            get
            {
                return _localVariables;
            }
            set
            {
                _localVariables = value;
                OnPropertyChanged("LocalVariables");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged("Comments");
            }
        }

        public EditDialogVM()
        {
            this.Comments = "";
            this.Description = "";
            this.LocalVariables = new BindingList<LocalVariable>();
            this.Name = "";
            this.Resref = "";
            this.Scripts = new BindingList<string>();
            this.Tag = "";
        }

    }
}
