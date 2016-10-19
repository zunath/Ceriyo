namespace Ceriyo.Core.Data
{
    public class LocalDoubleData
    {
        public string Key { get; set; }

        public double Value { get; set; }

        public LocalDoubleData()
        {
            Key = string.Empty;
            Value = 0.0f;
        }

        public LocalDoubleData(string key, double value)
        {
            Key = key;
            Value = value;
        }
    }
}
