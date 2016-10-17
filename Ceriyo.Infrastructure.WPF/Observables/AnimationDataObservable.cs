﻿using System;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Observables;
using Ceriyo.Infrastructure.WPF.BindableBases;
using Ceriyo.Infrastructure.WPF.Validation.Validators;

namespace Ceriyo.Infrastructure.WPF.Observables
{
    public class AnimationDataObservable: ValidatableBindableBase<AnimationData>
    {
        public delegate AnimationDataObservable Factory();

        private string _globalID;

        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        private string _resref;

        public string Resref
        {
            get { return _resref; }
            set { SetProperty(ref _resref, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set { SetProperty(ref _comment, value); }
        }

        private ObservableCollectionEx<FrameDataObservable> _frames;

        public ObservableCollectionEx<FrameDataObservable> Frames
        {
            get { return _frames; }
            set { SetProperty(ref _frames, value); }
        }


        public AnimationDataObservable(AnimationDataObservableValidator validator, 
            IObjectMapper objectMapper)
            :base(objectMapper, validator)
        {
            GlobalID = Guid.NewGuid().ToString();
            _frames = new ObservableCollectionEx<FrameDataObservable>();
        }
    }
}
