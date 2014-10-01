using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ClassEditorVM : BaseVM
    {
        private BindingList<CharacterClass> _classes;
        private CharacterClass _selectedClass;
        private bool _isClassSelected;

        public BindingList<CharacterClass> Classes
        {
            get
            {
                return _classes;
            }
            set
            {
                _classes = value;
                OnPropertyChanged("Classes");
            }
        }

        public CharacterClass SelectedClass
        {
            get
            {
                return _selectedClass;
            }
            set
            {
                _selectedClass = value;
                OnPropertyChanged("SelectedClass");
            }
        }

        public bool IsClassSelected
        {
            get
            {
                return _isClassSelected;
            }
            set
            {
                _isClassSelected = value;
                OnPropertyChanged("IsClassSelected");
            }
        }

        public ClassEditorVM()
        {
            this.IsClassSelected = false;
            this.Classes = new BindingList<CharacterClass>();
            this.SelectedClass = new CharacterClass();
        }
    }
}
