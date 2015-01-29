
namespace Ceriyo.Data.GameObjects
{
    public class LocalVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public LocalVariable()
        {
        }

        public LocalVariable(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
