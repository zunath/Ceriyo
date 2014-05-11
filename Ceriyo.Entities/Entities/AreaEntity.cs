using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;

namespace Ceriyo.Entities
{
    public class AreaEntity : PositionedObject
    {

        #region Methods

        // Constructor
        public AreaEntity(string contentManagerName)
        {
            // If you don't want to add to managers, make an overriding constructor
            Initialize(true);
        }

        protected virtual void InitializeEntity(bool addToManagers)
        {
            if (addToManagers)
            {
                AddToManagers(null);
            }
        }

        public virtual void AddToManagers(Layer layerToAddTo)
        {
            SpriteManager.AddPositionedObject(this);
        }
        


        public virtual void Activity()
        {
        }

        public virtual void Destroy()
        {
            // Remove self from the SpriteManager:
            SpriteManager.RemovePositionedObject(this);
        }

        #endregion
    }
}
