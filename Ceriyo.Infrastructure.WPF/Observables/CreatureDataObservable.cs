using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class CreatureDataObservable: ValidatableBindableBase<CreatureData>
    {
        public delegate CreatureDataObservable Factory(CreatureData data = null);

        private string _comment;
        private string _description;
        private LocalVariableDataObservable _localVariables;
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
            set { SetProperty(ref _globalID, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        public string ClassResref
        {
            get { return _classResref; }
            set { SetProperty(ref _classResref, value); }
        }

        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        public string DialogResref
        {
            get { return _dialogResref; }
            set { SetProperty(ref _dialogResref, value); }
        }

        public string OnConversation
        {
            get { return _onConversation; }
            set { SetProperty(ref _onConversation, value); }
        }

        public string OnAttacked
        {
            get { return _onAttacked; }
            set { SetProperty(ref _onAttacked, value); }
        }

        public string OnDamaged
        {
            get { return _onDamaged; }
            set { SetProperty(ref _onDamaged, value); }
        }

        public string OnDeath
        {
            get { return _onDeath; }
            set { SetProperty(ref _onDeath, value); }
        }

        public string OnDisturbed
        {
            get { return _onDisturbed; }
            set { SetProperty(ref _onDisturbed, value); }
        }

        public string OnHeartbeat
        {
            get { return _onHeartbeat; }
            set { SetProperty(ref _onHeartbeat, value); }
        }

        public string OnSpawned
        {
            get { return _onSpawned; }
            set { SetProperty(ref _onSpawned, value); }
        }

        public LocalVariableDataObservable LocalVariables
        {
            get { return _localVariables; }
            set { SetProperty(ref _localVariables, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }
        
        public CreatureDataObservable(CreatureDataObservableValidator validator,
            IObjectMapper objectMapper,
            LocalVariableDataObservable.Factory localVariableFactory,
            CreatureData creatureData = null)
            :base(objectMapper, validator, creatureData)
        {
            GlobalID = Guid.NewGuid().ToString();
            _localVariables = localVariableFactory.Invoke();
        }
    }
}
