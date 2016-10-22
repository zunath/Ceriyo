using System;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Observables.Contracts;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class FrameDataObservable: ValidatableBindableBase<FrameDataObservableValidator>, IDataObservable
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
            set { SetProperty(ref _globalID, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public bool FlipHorizontal
        {
            get { return _flipHorizontal; }
            set { SetProperty(ref _flipHorizontal, value); }
        }

        public bool FlipVertical
        {
            get { return _flipVertical; }
            set { SetProperty(ref _flipVertical, value); }
        }

        public float FrameLength
        {
            get { return _frameLength; }
            set { SetProperty(ref _frameLength, value); }
        }

        public int TextureCellX
        {
            get { return _textureCellX; }
            set { SetProperty(ref _textureCellX, value); }
        }

        public int TextureCellY
        {
            get { return _textureCellY; }
            set { SetProperty(ref _textureCellY, value); }
        }
        
        public FrameDataObservable()
        {
            GlobalID = Guid.NewGuid().ToString();
            Name = string.Empty;
            FlipHorizontal = false;
            FlipVertical = false;
            FrameLength = 0.0f;
            TextureCellX = 0;
            TextureCellY = 0;
        }
    }
}
