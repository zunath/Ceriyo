using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class SkillEditorVM : BaseVM
    {
        private BindingList<Skill> _skills;
        private Skill _selectedSkill;
        private bool _isSkillSelected;

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

        public SkillEditorVM()
        {
            this.Skills = new BindingList<Skill>();
            this.SelectedSkill = new Skill();
            this.IsSkillSelected = false;
        }
    }
}
