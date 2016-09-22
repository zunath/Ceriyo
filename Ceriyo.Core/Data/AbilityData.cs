using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Annotations;

namespace Ceriyo.Core.Data
{
    public class AbilityData: INotifyPropertyChanged
    {
        private string _globalID;
        private string _name;
        private string _tag;
        private string _resref;
        private bool _isPassive;
        private string _comment;
        private string _description;
        private string _onActivated;

        public string GlobalID
        {
            get { return _globalID; }
            set
            {
                if (value == _globalID) return;
                _globalID = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Tag
        {
            get { return _tag; }
            set
            {
                if (value == _tag) return;
                _tag = value;
                OnPropertyChanged();
            }
        }

        public string Resref
        {
            get { return _resref; }
            set
            {
                if (value == _resref) return;
                _resref = value;
                OnPropertyChanged();
            }
        }

        public bool IsPassive
        {
            get { return _isPassive; }
            set
            {
                if (value == _isPassive) return;
                _isPassive = value;
                OnPropertyChanged();
            }
        }

        public string OnActivated
        {
            get { return _onActivated; }
            set
            {
                if (value == _onActivated) return;
                _onActivated = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value == _comment) return;
                _comment = value;
                OnPropertyChanged();
            }
        }

        public AbilityData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
