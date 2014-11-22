using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class SkillEditorVM : BaseVM
    {
        private BindingList<Skill> _skills;
        private Skill _selectedSkill;
        private bool _isSkillSelected;
        private BindingList<string> _scripts; 

        public BindingList<Skill> Skills
        {
            get
            {
                return _skills;
            }
            set
            {
                _skills = value;
                OnPropertyChanged("Skills");
            }
        }

        public Skill SelectedSkill
        {
            get
            {
                return _selectedSkill;
            }
            set
            {
                _selectedSkill = value;
                OnPropertyChanged("SelectedSkill");
            }
        }

        public bool IsSkillSelected
        {
            get
            {
                return _isSkillSelected;
            }
            set
            {
                _isSkillSelected = value;
                OnPropertyChanged("IsSkillSelected");
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

        public SkillEditorVM()
        {
            Skills = new BindingList<Skill>();
            SelectedSkill = new Skill();
            IsSkillSelected = false;
            Scripts = new BindingList<string>();
        }
    }
}
