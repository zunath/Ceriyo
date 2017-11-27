using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ceriyo.Core.Properties;

namespace Ceriyo.Core.Observables
{
    /// <summary>
    /// An observable pair which signals INotifyPropertyChanged events.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    public class ObservablePair<T1, T2>: INotifyPropertyChanged
    {
        private T1 _key;

        /// <summary>
        /// The key of the pair.
        /// </summary>
        public T1 Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        private T2 _value;

        /// <summary>
        /// The value stored.
        /// </summary>
        public T2 Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructs a new ObservablePair.
        /// </summary>
        public ObservablePair()
        {
            
        }

        /// <summary>
        /// Constructs a new ObservablePair.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <param name="value">The value to store.</param>
        public ObservablePair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Handles invoking property changed events.
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
