using System.Linq;
using System.Xml.Serialization;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.GameObjects
{
    public class AssignedItemProperty
    {
        private string _name;

        [XmlIgnore]
        public string Name 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_name))
                {
                    GetName();
                }

                return _name;
            }
        }

        public string ItemPropertyResref { get; set; }
        public string ItemPropertyOptionResref { get; set; }
        public string ItemPropertyOptionValueName { get; set; }
        public string ItemPropertyOptionValueValue { get; set; }
        public ItemPropertyType ItemPropertyType { get; set; }

        public AssignedItemProperty()
        {
            _name = string.Empty;
            ItemPropertyOptionResref = string.Empty;
            ItemPropertyOptionValueName = string.Empty;
            ItemPropertyOptionValueValue = string.Empty;
            ItemPropertyResref = string.Empty;
            ItemPropertyType = ItemPropertyType.Unknown;
        }

        private void GetName()
        {
            ItemProperty ip = WorkingDataManager.GetGameObject<ItemProperty>(ModulePaths.ItemPropertiesDirectory, ItemPropertyResref);
            ItemPropertyOption ipo = ip.Options.SingleOrDefault(x => x.Resref == ItemPropertyOptionResref);

            _name = ip.Name;

            if (ipo != null)
            {
                _name += " : " + ipo.Name;

                if (!string.IsNullOrWhiteSpace(ItemPropertyOptionValueName))
                {
                    _name += " [ " + ItemPropertyOptionValueName + " ]";
                }
            }
        }

    }
}
