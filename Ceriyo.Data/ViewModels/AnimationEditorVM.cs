using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ceriyo.Data.GameObjects;
using FlatRedBall.Graphics.Animation;

namespace Ceriyo.Data.ViewModels
{
    public class AnimationEditorVM : BaseVM
    {
        private string _name;
        private string _tag;
        private string _resref;
        private BindingList<SpriteAnimation> _animations;
        private BindingList<AnimationFrame> _animationFrames;

    }
}
