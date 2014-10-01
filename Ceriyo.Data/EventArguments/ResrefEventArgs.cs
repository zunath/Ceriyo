using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.EventArguments
{
    public class ResrefEventArgs : EventArgs
    {
        public string Resref { get; set; }

        public ResrefEventArgs(string resref)
        {
            this.Resref = resref;
        }

    }
}
