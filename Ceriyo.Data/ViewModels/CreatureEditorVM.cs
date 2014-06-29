using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class CreatureEditorVM : BaseVM
    {
        private Creature _selectedCreature;
        private BindingList<Creature> _creatures;
        private bool _isCreatureSelected;
        private BindingList<Dialog> _dialogs;
        private BindingList<string> _scripts;
        private BindingList<CharacterClass> _characterClasses;
        private int _maxLevel;

        public Creature SelectedCreature
        {
            get
            {
                return _selectedCreature;
            }
            set
            {
                _selectedCreature = value;
                OnPropertyChanged("SelectedCreature");
            }
        }

        public BindingList<Creature> Creatures
        {
            get
            {
                return _creatures;
            }
            set
            {
                _creatures = value;
                OnPropertyChanged("Creatures");
            }
        }

        public bool IsCreatureSelected
        {
            get
            {
                return _isCreatureSelected;
            }
            set
            {
                _isCreatureSelected = value;
                OnPropertyChanged("IsCreatureSelected");
            }
        }

        public BindingList<Dialog> Dialogs
        {
            get
            {
                return _dialogs;
            }
            set
            {
                _dialogs = value;
                OnPropertyChanged("Dialogs");
            }
        }

        public BindingList<string> Scripts
        {
            get
            {
                return _scripts;
            }
            set
            {
                _scripts = value;
                OnPropertyChanged("Scripts");
            }
        }

        public BindingList<CharacterClass> CharacterClasses
        {
            get
            {
                return _characterClasses;
            }
            set
            {
                _characterClasses = value;
                OnPropertyChanged("CharacterClasses");
            }

        }

        public int MaxLevel
        {
            get
            {
                return _maxLevel;
            }
            set
            {
                _maxLevel = value;
                OnPropertyChanged("MaxLevel");
            }
        }

        public CreatureEditorVM()
        {
            this.IsCreatureSelected = false;
            this.Creatures = new BindingList<Creature>();
            this.SelectedCreature = new Creature();
            this.Dialogs = new BindingList<Dialog>();
            this.Scripts = new BindingList<string>();
            this.CharacterClasses = new BindingList<CharacterClass>();
            this.MaxLevel = EngineConstants.MaxLevel;
        }
    }
}
