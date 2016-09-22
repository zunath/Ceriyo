using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Annotations;

namespace Ceriyo.Core.Data
{
    public class ClassRequirementData: INotifyPropertyChanged
    {
        private int _levelRequired;
        private string _classResref;

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

        public int LevelRequired
        {
            get { return _levelRequired; }
            set
            {
                if (value == _levelRequired) return;
                _levelRequired = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
