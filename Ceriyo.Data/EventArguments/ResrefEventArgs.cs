using System;

namespace Ceriyo.Data.EventArguments
{
    public class ResrefEventArgs : EventArgs
    {
        public string Resref { get; set; }

        public ResrefEventArgs(string resref)
        {
            Resref = resref;
        }

    }
}
