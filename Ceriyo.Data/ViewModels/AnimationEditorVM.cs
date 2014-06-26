using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using FlatRedBall.Graphics.Animation;

namespace Ceriyo.Data.ViewModels
{
    public class AnimationEditorVM : BaseVM
    {
        private SpriteAnimation _selectedAnimation;
        private SpriteAnimationFrame _selectedFrame;
        private BindingList<SpriteAnimation> _animations;
        private BindingList<AnimationFrame> _animationFrames;
        private BindingList<GameResource> _graphics;
        private bool _isAnimationSelected;
        private bool _isFrameSelected;

        public SpriteAnimation SelectedAnimation
        {
            get
            {
                return _selectedAnimation;
            }
            set
            {
                _selectedAnimation = value;
                OnPropertyChanged("SelectedAnimation");
            }
        }

        public SpriteAnimationFrame SelectedFrame
        {
            get
            {
                return _selectedFrame;
            }
            set
            {
                _selectedFrame = value;
                OnPropertyChanged("SelectedFrame");
            }
        }

        public BindingList<SpriteAnimation> Animations
        {
            get
            {
                return _animations;
            }
            set
            {
                _animations = value;
                OnPropertyChanged("Animations");
            }
        }

        public BindingList<AnimationFrame> AnimationFrames
        {
            get
            {
                return _animationFrames;
            }
            set
            {
                _animationFrames = value;
                OnPropertyChanged("AnimationFrames");
            }
        }

        public BindingList<GameResource> Graphics
        {
            get
            {
                return _graphics;
            }
            set
            {
                _graphics = value;
                OnPropertyChanged("Graphics");
            }
        }

        public bool IsAnimationSelected
        {
            get
            {
                return _isAnimationSelected;
            }
            set
            {
                _isAnimationSelected = value;
                OnPropertyChanged("IsAnimationSelected");
            }
        }

        public bool IsFrameSelected
        {
            get
            {
                return _isFrameSelected;
            }
            set
            {
                _isFrameSelected = value;
                OnPropertyChanged("IsFrameSelected");
            }
        }

        public AnimationEditorVM()
        {
            this.AnimationFrames = new BindingList<AnimationFrame>();
            this.Animations = new BindingList<SpriteAnimation>();
            this.Graphics = new BindingList<GameResource>();
            this.IsAnimationSelected = false;
            this.IsFrameSelected = false;
            this.SelectedAnimation = new SpriteAnimation();
            this.SelectedFrame = new SpriteAnimationFrame();
        }

    }
}
