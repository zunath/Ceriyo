namespace Ceriyo.Toolset.WPF.EventArgs
{
    public class ModuleEventArgs
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }

        public ModuleEventArgs(string name, string tag, string resref)
        {
            Name = name;
            Tag = tag;
            Resref = resref;
        }
    }
}
