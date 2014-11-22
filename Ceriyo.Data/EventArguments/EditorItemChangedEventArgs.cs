using System;
using Ceriyo.Data.GameObjects;

namespace Ceriyo.Data.EventArguments
{
    public class EditorItemChangedEventArgs : EventArgs
    {
        public bool IsAdded { get; set; }
        public IGameObject GameObject { get; set; }
        public string Resref { get; set; }
        public bool IsChanged { get; set; }

        public EditorItemChangedEventArgs(IGameObject gameObject, string resref, bool isAdded, bool isChanged)
        {
            GameObject = gameObject;
            IsAdded = isAdded;
            Resref = resref;
            IsChanged = isChanged;
        }
    }
}
