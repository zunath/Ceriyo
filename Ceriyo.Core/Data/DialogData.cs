using System;

namespace Ceriyo.Core.Data
{
    public class DialogData
    {
        public string GlobalID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }

        public DialogData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
