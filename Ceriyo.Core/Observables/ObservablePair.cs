using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Properties;

namespace Ceriyo.Core.Observables
{
    public class ObservablePair<T1, T2>: INotifyPropertyChanged
    {
        private T1 _key;

        public T1 Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        private T2 _value;

        public T2 Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public ObservablePair()
        {
            
        }

        public ObservablePair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
