using System.ComponentModel;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ResourceEditorVM : BaseVM
    {
        private BindingList<CPResource> _resources;
        private BindingList<CPResourceType> _resourceTypes; 

        public BindingList<CPResource> Resources 
        {
            get
            {
                return _resources;
            }
            set
            {
                _resources = value;
                OnPropertyChanged("Resources");
            }
        }

        public BindingList<CPResourceType> ResourceTypes
        {
            get
            {
                return _resourceTypes;
            }
            set
            {
                _resourceTypes = value;
                OnPropertyChanged("ResourceTypes");
            }
        }

        public ResourceEditorVM()
        {
            Resources = new BindingList<CPResource>();
            ResourceTypes = new BindingList<CPResourceType>
            {
                new CPResourceType(string.Empty, ResourceType.Unknown, ResourceSubType.Unknown),
                new CPResourceType("Graphics/Tileset", ResourceType.Graphic, ResourceSubType.Tileset),
                new CPResourceType("Graphics/Creature", ResourceType.Graphic, ResourceSubType.Creature),
                new CPResourceType("Graphics/Inventory Icon", ResourceType.Graphic, ResourceSubType.InventoryIcon),
                new CPResourceType("Graphics/Equipment", ResourceType.Graphic, ResourceSubType.Equipment),
                new CPResourceType("Graphics/Head", ResourceType.Graphic, ResourceSubType.Head),
                new CPResourceType("Graphics/Placeable", ResourceType.Graphic, ResourceSubType.Placeable),
                new CPResourceType("Audio/Music", ResourceType.Audio, ResourceSubType.Music)
            };

        }
    }
}
