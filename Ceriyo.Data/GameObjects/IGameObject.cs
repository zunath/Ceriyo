using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.GameObjects
{
    public interface IGameObject
    {
        string Name { get; set; }
        string Tag { get; set; }
        string Resref { get; set; }
        string Description { get; set; }
        string Comments { get; set; }
        IList<LocalVariable> LocalVariables { get; set; }
        IDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        string WorkingDirectory { get; }
    }
}
