using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
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
        public ItemPropertyTypeEnum ItemPropertyType { get; set; }

        public AssignedItemProperty()
        {
            _name = string.Empty;
            this.ItemPropertyOptionResref = string.Empty;
            this.ItemPropertyOptionValueName = string.Empty;
            this.ItemPropertyOptionValueValue = string.Empty;
            this.ItemPropertyResref = string.Empty;
            this.ItemPropertyType = ItemPropertyTypeEnum.Unknown;
        }

        private void GetName()
        {
            WorkingDataManager wdm = new WorkingDataManager();
            ItemProperty ip = wdm.GetGameObject<ItemProperty>(ModulePaths.ItemPropertiesDirectory, ItemPropertyResref);
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
