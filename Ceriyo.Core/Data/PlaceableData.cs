using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Properties;

namespace Ceriyo.Core.Data
{
    public class PlaceableData: INotifyPropertyChanged
    {
        private LocalVariableData _localVariables;
        private string _onUsed;
        private string _onAttacked;
        private string _onUnlocked;
        private string _onLocked;
        private string _onDisturbed;
        private string _onHeartbeat;
        private string _onDeath;
        private string _onDamaged;
        private string _onOpened;
        private string _onClosed;
        private string _keyTag;
        private bool _autoRemoveKey;
        private bool _isKeyRequired;
        private bool _isLocked;
        private bool _isUseable;
        private bool _isPlot;
        private bool _isStatic;
        private string _comment;
        private string _description;
        private string _resref;
        private string _tag;
        private string _globalID;
        private string _name;

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

        public bool IsStatic
        {
            get { return _isStatic; }
            set
            {
                if (value == _isStatic) return;
                _isStatic = value;
                OnPropertyChanged();
            }
        }

        public bool IsPlot
        {
            get { return _isPlot; }
            set
            {
                if (value == _isPlot) return;
                _isPlot = value;
                OnPropertyChanged();
            }
        }

        public bool IsUseable
        {
            get { return _isUseable; }
            set
            {
                if (value == _isUseable) return;
                _isUseable = value;
                OnPropertyChanged();
            }
        }

        public bool IsLocked
        {
            get { return _isLocked; }
            set
            {
                if (value == _isLocked) return;
                _isLocked = value;
                OnPropertyChanged();
            }
        }

        public bool IsKeyRequired
        {
            get { return _isKeyRequired; }
            set
            {
                if (value == _isKeyRequired) return;
                _isKeyRequired = value;
                OnPropertyChanged();
            }
        }

        public bool AutoRemoveKey
        {
            get { return _autoRemoveKey; }
            set
            {
                if (value == _autoRemoveKey) return;
                _autoRemoveKey = value;
                OnPropertyChanged();
            }
        }

        public string KeyTag
        {
            get { return _keyTag; }
            set
            {
                if (value == _keyTag) return;
                _keyTag = value;
                OnPropertyChanged();
            }
        }

        public string OnClosed
        {
            get { return _onClosed; }
            set
            {
                if (value == _onClosed) return;
                _onClosed = value;
                OnPropertyChanged();
            }
        }

        public string OnOpened
        {
            get { return _onOpened; }
            set
            {
                if (value == _onOpened) return;
                _onOpened = value;
                OnPropertyChanged();
            }
        }

        public string OnDamaged
        {
            get { return _onDamaged; }
            set
            {
                if (value == _onDamaged) return;
                _onDamaged = value;
                OnPropertyChanged();
            }
        }

        public string OnDeath
        {
            get { return _onDeath; }
            set
            {
                if (value == _onDeath) return;
                _onDeath = value;
                OnPropertyChanged();
            }
        }

        public string OnHeartbeat
        {
            get { return _onHeartbeat; }
            set
            {
                if (value == _onHeartbeat) return;
                _onHeartbeat = value;
                OnPropertyChanged();
            }
        }

        public string OnDisturbed
        {
            get { return _onDisturbed; }
            set
            {
                if (value == _onDisturbed) return;
                _onDisturbed = value;
                OnPropertyChanged();
            }
        }

        public string OnLocked
        {
            get { return _onLocked; }
            set
            {
                if (value == _onLocked) return;
                _onLocked = value;
                OnPropertyChanged();
            }
        }

        public string OnUnlocked
        {
            get { return _onUnlocked; }
            set
            {
                if (value == _onUnlocked) return;
                _onUnlocked = value;
                OnPropertyChanged();
            }
        }

        public string OnAttacked
        {
            get { return _onAttacked; }
            set
            {
                if (value == _onAttacked) return;
                _onAttacked = value;
                OnPropertyChanged();
            }
        }

        public string OnUsed
        {
            get { return _onUsed; }
            set
            {
                if (value == _onUsed) return;
                _onUsed = value;
                OnPropertyChanged();
            }
        }

        public LocalVariableData LocalVariables
        {
            get { return _localVariables; }
            set
            {
                if (Equals(value, _localVariables)) return;
                _localVariables = value;
                OnPropertyChanged();
            }
        }


        public PlaceableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
