namespace Ceriyo.Core.Data
{
    public class ClassRequirementData: BaseDataRecord
    {
        private int _levelRequired;
        private string _classResref;

        public string ClassResref
        {
            get { return _classResref; }
            set
            {
                if (value == _classResref) return;
                _classResref = value;
                OnPropertyChanged();
            }
        }

        public int LevelRequired
        {
            get { return _levelRequired; }
            set
            {
                if (value == _levelRequired) return;
                _levelRequired = value;
                OnPropertyChanged();
            }
        }
        
    }
}
