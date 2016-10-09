using System;
using System.Collections.Generic;

namespace Ceriyo.Core.Data
{
    public class LocalVariableData: BaseDataRecord
    {
        private Dictionary<string, float> _localFloats;
        private Dictionary<string, string> _localStrings;
        private string _globalID;

        public string GlobalID
        {
            get { return _globalID; }
            set
            {
                if (value == _globalID) return;
                _globalID = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<string, string> LocalStrings
        {
            get { return _localStrings; }
            set
            {
                if (Equals(value, _localStrings)) return;
                _localStrings = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<string, float> LocalFloats
        {
            get { return _localFloats; }
            set
            {
                if (Equals(value, _localFloats)) return;
                _localFloats = value;
                OnPropertyChanged();
            }
        }

        public LocalVariableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalStrings = new Dictionary<string, string>();
            LocalFloats = new Dictionary<string, float>();
        }
        
    }
}
