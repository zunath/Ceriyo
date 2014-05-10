using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.GameObjects
{
    public interface IGameObject
    {
        string Name { get; set; }
        string Tag { get; set; }
        string Resref { get; set; }
        string WorkingDirectory { get; }
    }
}
