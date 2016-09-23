using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Annotations;

namespace Ceriyo.Core.Data
{
    public class CreatureData: INotifyPropertyChanged
    {
        private string _comment;
        private string _description;
        private LocalVariableData _localVariables;
        private string _onSpawned;
        private string _onHeartbeat;
        private string _onDisturbed;
        private string _onDeath;
        private string _onDamaged;
        private string _onAttacked;
        private string _onConversation;
        private string _dialogResref;
        private int _level;
        private string _classResref;
        private string _resref;
        private string _tag;
        private string _name;
        private string _globalID;

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

        public string ClassResref
        {
            get { return _classResref; }
            set
            {
                if (value == _classResref) return;
                _classResref = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                if (value == _level) return;
                _level = value;
                OnPropertyChanged();
            }
        }

        public string DialogResref
        {
            get { return _dialogResref; }
            set
            {
                if (value == _dialogResref) return;
                _dialogResref = value;
                OnPropertyChanged();
            }
        }

        public string OnConversation
        {
            get { return _onConversation; }
            set
            {
                if (value == _onConversation) return;
                _onConversation = value;
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

        public string OnSpawned
        {
            get { return _onSpawned; }
            set
            {
                if (value == _onSpawned) return;
                _onSpawned = value;
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

        public CreatureData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
            Level = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
