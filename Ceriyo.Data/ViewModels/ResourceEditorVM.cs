using System.ComponentModel;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.ResourceObjects;

namespace Ceriyo.Data.ViewModels
{
    public class ResourceEditorVM : BaseVM
    {
        private BindingList<ResourceEditorItem> _resources;
        private BindingList<ResourceEditorType> _resourceTypes; 

        public BindingList<ResourceEditorItem> Resources 
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

        public BindingList<ResourceEditorType> ResourceTypes
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
            Resources = new BindingList<ResourceEditorItem>();
            ResourceTypes = new BindingList<ResourceEditorType>
            {
                new ResourceEditorType(string.Empty, ResourceType.Unknown, ResourceSubType.Unknown),
                new ResourceEditorType("Graphics/Tileset", ResourceType.Graphic, ResourceSubType.Tileset),
                new ResourceEditorType("Graphics/Creature", ResourceType.Graphic, ResourceSubType.Creature),
                new ResourceEditorType("Graphics/Inventory Icon", ResourceType.Graphic, ResourceSubType.InventoryIcon),
                new ResourceEditorType("Graphics/Equipment", ResourceType.Graphic, ResourceSubType.Equipment),
                new ResourceEditorType("Graphics/Head", ResourceType.Graphic, ResourceSubType.Head),
                new ResourceEditorType("Graphics/Placeable", ResourceType.Graphic, ResourceSubType.Placeable),
                new ResourceEditorType("Audio/Music", ResourceType.Audio, ResourceSubType.Music)
            };

        }
    }
}
