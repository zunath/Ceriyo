using System.ComponentModel;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.ViewModels
{
    public class RaceEditorVM : BaseVM
    {
        private BindingList<Race> _races;
        private Race _selectedRace;
        private bool _isRaceSelected;

        public BindingList<Race> Races
        {
            get
            {
                return _races;
            }
            set
            {
                _races = value;
                OnPropertyChanged("Races");
            }
        }

        public Race SelectedRace
        {
            get
            {
                return _selectedRace;
            }
            set
            {
                _selectedRace = value;
                OnPropertyChanged("SelectedRace");
            }
        }

        public bool IsRaceSelected
        {
            get
            {
                return _isRaceSelected;
            }
            set
            {
                _isRaceSelected = value;
                OnPropertyChanged("IsRaceSelected");
            }
        }

        public RaceEditorVM()
        {
            this.IsRaceSelected = false;
            this.Races = new BindingList<Race>();
            this.SelectedRace = new Race();
        }
    }
}
