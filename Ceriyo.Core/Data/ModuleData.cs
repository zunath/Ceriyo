using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Annotations;

namespace Ceriyo.Core.Data
{
    public class ModuleData: INotifyPropertyChanged
    {
        private BindingList<string> _tilesetIDs;
        private BindingList<string> _skillIDs;
        private BindingList<string> _scriptIDs;
        private BindingList<string> _placeableIDs;
        private BindingList<string> _itemPropertyIDs;
        private BindingList<string> _itemIDs;
        private BindingList<string> _creatureIDs;
        private BindingList<string> _classIDs;
        private BindingList<string> _abilityIDs;

        public BindingList<string> AbilityIDs
        {
            get { return _abilityIDs; }
            private set
            {
                if (Equals(value, _abilityIDs)) return;
                _abilityIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ClassIDs
        {
            get { return _classIDs; }
            private set
            {
                if (Equals(value, _classIDs)) return;
                _classIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> CreatureIDs
        {
            get { return _creatureIDs; }
            private set
            {
                if (Equals(value, _creatureIDs)) return;
                _creatureIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ItemIDs
        {
            get { return _itemIDs; }
            private set
            {
                if (Equals(value, _itemIDs)) return;
                _itemIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ItemPropertyIDs
        {
            get { return _itemPropertyIDs; }
            private set
            {
                if (Equals(value, _itemPropertyIDs)) return;
                _itemPropertyIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> PlaceableIDs
        {
            get { return _placeableIDs; }
            private set
            {
                if (Equals(value, _placeableIDs)) return;
                _placeableIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> ScriptIDs
        {
            get { return _scriptIDs; }
            private set
            {
                if (Equals(value, _scriptIDs)) return;
                _scriptIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> SkillIDs
        {
            get { return _skillIDs; }
            private set
            {
                if (Equals(value, _skillIDs)) return;
                _skillIDs = value;
                OnPropertyChanged();
            }
        }

        public BindingList<string> TilesetIDs
        {
            get { return _tilesetIDs; }
            private set
            {
                if (Equals(value, _tilesetIDs)) return;
                _tilesetIDs = value;
                OnPropertyChanged();
            }
        }

        public ModuleData()
        {
            AbilityIDs = new BindingList<string>();
            ClassIDs = new BindingList<string>();
            CreatureIDs = new BindingList<string>();
            ItemIDs = new BindingList<string>();
            ItemPropertyIDs = new BindingList<string>();
            PlaceableIDs = new BindingList<string>();
            ScriptIDs = new BindingList<string>();
            SkillIDs = new BindingList<string>();
            TilesetIDs = new BindingList<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
