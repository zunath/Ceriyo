using System;

namespace Ceriyo.Core.Data
{
    public class FrameData: BaseDataRecord
    {
        private int _textureCellY;
        private int _textureCellX;
        private float _frameLength;
        private bool _flipVertical;
        private bool _flipHorizontal;
        private string _name;
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

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool FlipHorizontal
        {
            get { return _flipHorizontal; }
            set
            {
                if (value == _flipHorizontal) return;
                _flipHorizontal = value;
                OnPropertyChanged();
            }
        }

        public bool FlipVertical
        {
            get { return _flipVertical; }
            set
            {
                if (value == _flipVertical) return;
                _flipVertical = value;
                OnPropertyChanged();
            }
        }

        public float FrameLength
        {
            get { return _frameLength; }
            set
            {
                if (value.Equals(_frameLength)) return;
                _frameLength = value;
                OnPropertyChanged();
            }
        }

        public int TextureCellX
        {
            get { return _textureCellX; }
            set
            {
                if (value == _textureCellX) return;
                _textureCellX = value;
                OnPropertyChanged();
            }
        }

        public int TextureCellY
        {
            get { return _textureCellY; }
            set
            {
                if (value == _textureCellY) return;
                _textureCellY = value;
                OnPropertyChanged();
            }
        }

        public FrameData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
        
    }
}
