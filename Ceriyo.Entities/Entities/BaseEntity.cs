using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;

namespace Ceriyo.Entities
{
    public abstract class BaseEntity : PositionedObject
    {
        string mContentManagerName;

        public BaseEntity(string contentManagerName)
        {
            mContentManagerName = contentManagerName;
            Initialize(true);
        }

        protected abstract void CustomInitialize();
        protected virtual void InitializeEntity(bool addToManagers)
        {
            if (addToManagers)
            {
                AddToManagers(null);
            }

            CustomInitialize();
        }

        public virtual void AddToManagers(Layer layerToAddTo)
        {
            SpriteManager.AddPositionedObject(this);
        }


        protected abstract void CustomActivity();
        public virtual void Activity()
        {
            CustomActivity();
        }

        protected abstract void CustomDestroy();
        public virtual void Destroy()
        {
            SpriteManager.RemovePositionedObject(this);
            CustomDestroy();
        }
    }
}
