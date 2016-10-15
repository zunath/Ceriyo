using System;
using Ceriyo.Toolset.WPF.Events.Ability;
using Ceriyo.Toolset.WPF.Events.Animation;
using Ceriyo.Toolset.WPF.Events.Class;
using Ceriyo.Toolset.WPF.Events.Creature;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Item;
using Ceriyo.Toolset.WPF.Events.Placeable;
using Ceriyo.Toolset.WPF.Events.Skill;
using Ceriyo.Toolset.WPF.Events.Tileset;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.DataEditorView
{
    public class DataEditorViewModel : BindableBase, IInteractionRequestAware
    {
        private readonly IEventAggregator _eventAggregator;

        public DataEditorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            OkCommand = new DelegateCommand(Ok, IsValid);
            CancelCommand = new DelegateCommand(Cancel);

            IsAbilityEditorValid = true;
            IsAnimationEditorValid = true;
            IsClassEditorValid = true;
            IsCreatureEditorValid = true;
            IsItemEditorValid = true;
            IsPlaceableEditorValid = true;
            IsSkillEditorValid = true;
            IsTilesetEditorValid = true;

            _eventAggregator.GetEvent<AbilityEditorValidityChangedEvent>().Subscribe(OnAbilityEditorValidityChanged);
            _eventAggregator.GetEvent<AnimationEditorValidityChangedEvent>().Subscribe(OnAnimationEditorValidityChanged);
            _eventAggregator.GetEvent<ClassEditorValidityChangedEvent>().Subscribe(OnClassEditorValidityChanged);
            _eventAggregator.GetEvent<CreatureEditorValidityChangedEvent>().Subscribe(OnCreatureEditorValidityChanged);
            _eventAggregator.GetEvent<ItemEditorValidityChangedEvent>().Subscribe(OnItemEditorValidityChanged);
            _eventAggregator.GetEvent<PlaceableEditorValidityChangedEvent>().Subscribe(OnPlaceableEditorValidityChanged);
            _eventAggregator.GetEvent<SkillEditorValidityChangedEvent>().Subscribe(OnSkillEditorValidityChanged);
            _eventAggregator.GetEvent<TilesetEditorValidityChangedEvent>().Subscribe(OnTilesetEditorValidityChanged);
        }

        private bool IsValid()
        {
            return IsAbilityEditorValid &&
                   IsAnimationEditorValid &&
                   IsClassEditorValid &&
                   IsCreatureEditorValid &&
                   IsItemEditorValid &&
                   IsPlaceableEditorValid &&
                   IsSkillEditorValid &&
                   IsTilesetEditorValid;
        }

        private void OnAbilityEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }
        private void OnAnimationEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }
        private void OnClassEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }
        private void OnCreatureEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }
        private void OnItemEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }
        private void OnPlaceableEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }
        private void OnSkillEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }
        private void OnTilesetEditorValidityChanged(bool isValid)
        {
            IsAbilityEditorValid = isValid;
            OkCommand.RaiseCanExecuteChanged();
        }


        public DelegateCommand OkCommand { get; set; }

        private void Ok()
        {
            if (!IsValid()) return;

            _eventAggregator.GetEvent<DataEditorClosedEvent>().Publish(true);
            FinishInteraction();
        }

        public DelegateCommand CancelCommand { get; set; }

        private void Cancel()
        {
            _eventAggregator.GetEvent<DataEditorClosedEvent>().Publish(false);
            FinishInteraction();
        }



        private bool _isAbilityEditorValid;

        public bool IsAbilityEditorValid
        {
            get { return _isAbilityEditorValid; }
            set { SetProperty(ref _isAbilityEditorValid, value); }
        }

        private bool _isAnimationEditorValid;

        public bool IsAnimationEditorValid
        {
            get { return _isAnimationEditorValid; }
            set { SetProperty(ref _isAnimationEditorValid, value); }
        }
        private bool _isClassEditorValid;

        public bool IsClassEditorValid
        {
            get { return _isClassEditorValid; }
            set { SetProperty(ref _isClassEditorValid, value); }
        }
        private bool _isCreatureEditorValid;

        public bool IsCreatureEditorValid
        {
            get { return _isCreatureEditorValid; }
            set { SetProperty(ref _isCreatureEditorValid, value); }
        }
        private bool _isItemEditorValid;

        public bool IsItemEditorValid
        {
            get { return _isItemEditorValid; }
            set { SetProperty(ref _isItemEditorValid, value); }
        }
        private bool _isPlaceableEditorValid;

        public bool IsPlaceableEditorValid
        {
            get { return _isPlaceableEditorValid; }
            set { SetProperty(ref _isPlaceableEditorValid, value); }
        }
        private bool _isSkillEditorValid;

        public bool IsSkillEditorValid
        {
            get { return _isSkillEditorValid; }
            set { SetProperty(ref _isSkillEditorValid, value); }
        }
        private bool _isTilesetEditorValid;

        public bool IsTilesetEditorValid
        {
            get { return _isTilesetEditorValid; }
            set { SetProperty(ref _isTilesetEditorValid, value); }
        }

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }
    }
}
