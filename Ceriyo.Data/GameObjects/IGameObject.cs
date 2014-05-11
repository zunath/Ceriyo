using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Extensions;

namespace Ceriyo.Data.GameObjects
{
    public interface IGameObject
    {
        string Name { get; set; }
        string Tag { get; set; }
        string Resref { get; set; }
        string Description { get; set; }
        string Comments { get; set; }
        List<LocalVariable> LocalVariables { get; set; }
        SerializableDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }
        string WorkingDirectory { get; }
    }
}
