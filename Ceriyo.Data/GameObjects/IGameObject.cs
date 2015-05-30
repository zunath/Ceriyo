using System.ComponentModel;
using Ceriyo.Data.DataObjects;
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
        BindingList<LocalVariable> LocalVariables { get; set; }
        SerializableDictionary<ScriptEventType, string> Scripts { get; set; }
        string WorkingDirectory { get; }
        string CategoryName { get; }
    }
}
