using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Extensions;
using Ceriyo.Core.Observables;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class LocalVariableDataObservable: ValidatableBindableBase<LocalVariableData>
    {
        public delegate LocalVariableDataObservable Factory(LocalVariableData data = null);

        private ObservableCollectionEx<LocalDoubleDataObservable> _localDoubles;
        private ObservableCollectionEx<LocalStringDataObservable> _localStrings;
        private string _globalID;

        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }

        public ObservableCollectionEx<LocalStringDataObservable> LocalStrings
        {
            get { return _localStrings; }
            set { SetProperty(ref _localStrings, value); }
        }

        public ObservableCollectionEx<LocalDoubleDataObservable> LocalDoubles
        {
            get { return _localDoubles; }
            set { SetProperty(ref _localDoubles, value); }
        }

        public event EventHandler<NotifyCollectionChangedEventArgs> VariablesCollectionChanged;
        public event EventHandler<PropertyChangedEventArgs> VariablesPropertyChanged;
        public event EventHandler<PropertyChangedEventArgs> VariablesItemPropertyChanged;

        public LocalVariableDataObservable()
        {
            
        }

        public LocalVariableDataObservable(LocalVariableDataObservableValidator validator,
            IObjectMapper objectMapper,
            LocalVariableData data = null)
            :base(objectMapper, validator, data)
        {
            if (data != null) return;
            GlobalID = Guid.NewGuid().ToString();
            _localStrings = new ObservableCollectionEx<LocalStringDataObservable>();
            _localDoubles = new ObservableCollectionEx<LocalDoubleDataObservable>();

            _localDoubles.CollectionChanged += VariableCollectionChanged;
            _localStrings.CollectionChanged += VariableCollectionChanged;

            _localDoubles.PropertyChanged += VariableCollectionPropertyChanged;
            _localStrings.PropertyChanged += VariableCollectionPropertyChanged;

            _localDoubles.ItemPropertyChanged += VariableCollectionItemPropertyChanged;
            _localStrings.ItemPropertyChanged += VariableCollectionItemPropertyChanged;
        }

        private void VariableCollectionItemPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            VariablesItemPropertyChanged.RaiseEvent(sender, propertyChangedEventArgs);
        }

        private void VariableCollectionPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            VariablesPropertyChanged.RaiseEvent(sender, propertyChangedEventArgs);
        }

        private void VariableCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            VariablesCollectionChanged.RaiseEvent(sender, notifyCollectionChangedEventArgs);
        }
    }
}
