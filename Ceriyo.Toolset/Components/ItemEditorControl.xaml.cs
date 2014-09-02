using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.Engine;
using Ceriyo.Data.ResourceObjects;
using System.ComponentModel;

namespace Ceriyo.Toolset.Components
{
    public partial class ItemEditorControl : UserControl
    {
        private ItemEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }
        private ResourcePackDataManager ResourcePackManager { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public ItemEditorControl()
        {
            InitializeComponent();
            Model = new ItemEditorVM();
            Processor = new GameResourceProcessor();
            ResourcePackManager = new ResourcePackDataManager();
            WorkingManager = new WorkingDataManager();
            this.DataContext = Model;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            string resref = Processor.GenerateUniqueResref(Model.Items.Cast<IGameObject>().ToList(), item.CategoryName);

            item.Name = resref;
            item.Tag = resref;
            item.Resref = resref;

            item.InventoryGraphic = lbInventoryGraphic.Items[0] as GameResource;
            item.WorldGraphic = lbWorldGraphic.Items[0] as GameResource;
            item.ItemTypeResref = (lbItemType.Items[0] as ItemType).Resref;
            item.ItemRequirements = BuildItemRequirements();
            Model.Items.Add(item);
            int index = lbItems.Items.IndexOf(item);
            lbItems.SelectedItem = lbItems.Items[index];
            
            
        }

        private BindingList<ItemClassRequirement> BuildItemRequirements()
        {
            BindingList<ItemClassRequirement> requirements = new BindingList<ItemClassRequirement>();

            BindingList<CharacterClass> classes = WorkingManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory);

            foreach (CharacterClass charClass in classes)
            {
                ItemClassRequirement req = new ItemClassRequirement
                {
                    ClassResref = charClass.Resref,
                    IsAvailable = true,
                    LevelRequired = EngineConstants.MaxLevel
                };

                requirements.Add(req);
            }

            return requirements;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Item item = lbItems.SelectedItem as Item;

            if (item != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this item?", "Delete Item?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Items.Remove(item);
                    Model.SelectedItem = null;
                    Model.IsItemSelected = false;
                    imgInventoryGraphic.Source = null;
                    imgWorldGraphic.Source = null;
                }
            }
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Items.Cast<IGameObject>().ToList(), WorkingPaths.ItemsDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save items.", "Saving items failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackManager.GetGameResources(ResourceTypeEnum.Graphic);
            GameResource graphic = new GameResource("", "(No Graphic)", ResourceTypeEnum.None);
            Model.Graphics.Insert(0, graphic);

            Model.Items = WorkingManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.ItemTypes = WorkingManager.GetAllGameObjects<ItemType>(ModulePaths.ItemTypesDirectory);
            Model.Scripts = WorkingManager.GetAllScriptNames();
            Model.AvailableItemProperties = WorkingManager.GetAllGameObjects<ItemProperty>(ModulePaths.ItemPropertiesDirectory);

            foreach (Item item in Model.Items)
            {
                GameResource resource = Model.Graphics.SingleOrDefault(x => x.FileName == item.InventoryGraphic.FileName);
                if (resource != null)
                {
                    item.InventoryGraphic = resource;
                }
                resource = Model.Graphics.SingleOrDefault(x => x.FileName == item.WorldGraphic.FileName);
                if (resource != null)
                {
                    item.WorldGraphic = resource;
                }
            }

            if (Model.Items.Count > 0)
            {
                lbItems.SelectedItem = Model.Items[0];
            }
        }

        private void ItemSelected(object sender, SelectionChangedEventArgs e)
        {
            Item item = lbItems.SelectedItem as Item;
            Model.SelectedItem = item;
            Model.IsItemSelected = item == null ? false : true;
            
            if(item != null)
            {
                lbItemType.SelectedItem = Model.ItemTypes.SingleOrDefault(x => x.Resref == item.ItemTypeResref);
            }
        }

        private void ItemTypeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (lbItemType.SelectedItem != null)
            {
                Model.SelectedItem.ItemTypeResref = (lbItemType.SelectedItem as ItemType).Resref;
            }
        }

        public void InventoryGraphicSelected(object sender, SelectionChangedEventArgs e)
        {
            GameResource resource = lbInventoryGraphic.SelectedItem as GameResource;

            if (resource != null)
            {
                if (resource.ResourceType == ResourceTypeEnum.None)
                {
                    imgInventoryGraphic.Source = null;
                }
                else
                {
                    BitmapImage image = Processor.ToBitmapImage(resource);
                    Model.SelectedItem.InventoryGraphic = resource;
                    imgInventoryGraphic.Source = image;
                }
            }
        }

        public void WorldGraphicSelected(object sender, SelectionChangedEventArgs e)
        {
            GameResource resource = lbWorldGraphic.SelectedItem as GameResource;

            if (resource != null)
            {
                if (resource.ResourceType == ResourceTypeEnum.None)
                {
                    imgWorldGraphic.Source = null;
                }
                else
                {
                    BitmapImage image = Processor.ToBitmapImage(resource);
                    Model.SelectedItem.WorldGraphic = resource;
                    imgWorldGraphic.Source = image;
                }
            }
        }

        private void AddItemProperty(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedItem != null && Model.SelectedAvailableItemProperty != null)
            {
                Type type = tvAvailableProperties.SelectedItem.GetType();

                // Item property selected (I.E: Ability Bonus)
                if (type == typeof(ItemProperty))
                {
                    ItemProperty ip = tvAvailableProperties.SelectedItem as ItemProperty;
                    AssignedItemProperty aip = Model.SelectedItem.AssignedItemProperties.SingleOrDefault(x => x.ItemPropertyResref == ip.Resref);

                    if (ip.Options.Count <= 0 && aip == null)
                    {
                        aip = new AssignedItemProperty
                        {
                            ItemPropertyType = ip.ItemPropertyType,
                            ItemPropertyResref = ip.Resref
                        };

                        Model.SelectedItem.AssignedItemProperties.Add(aip);
                    }
                }
                // Option selected (I.E: Strength)
                else if (type == typeof(ItemPropertyOption))
                {
                    ItemPropertyOption ipo = tvAvailableProperties.SelectedItem as ItemPropertyOption;
                    ItemProperty ip = Model.AvailableItemProperties.SingleOrDefault(x => x.Resref == ipo.ParentItemPropertyResref);
                    AssignedItemProperty aip = Model.SelectedItem.AssignedItemProperties
                        .SingleOrDefault(x => x.ItemPropertyResref == ipo.ParentItemPropertyResref && 
                                              x.ItemPropertyOptionResref == ipo.Resref);
                    

                    if (ipo.Values.Count <= 0 && aip == null)
                    {
                        aip = new AssignedItemProperty
                        {
                            ItemPropertyOptionResref = ipo.Resref,
                            ItemPropertyResref = ipo.ParentItemPropertyResref,
                            ItemPropertyType = ip.ItemPropertyType
                        };

                        Model.SelectedItem.AssignedItemProperties.Add(aip);
                    }
                }
                // Option value selected (I.E: +15)
                else if(type == typeof(ItemPropertyOptionValue))
                {
                    ItemPropertyOptionValue optionValue = tvAvailableProperties.SelectedItem as ItemPropertyOptionValue;
                    ItemProperty ip = Model.AvailableItemProperties.SingleOrDefault(x => x.Options.SingleOrDefault(y => y.Resref == optionValue.ParentOptionResref) != null);
                    
                    if(ip != null)
                    {
                        ItemPropertyOption ipo = ip.Options.SingleOrDefault(x => x.Resref == optionValue.ParentOptionResref);
                    
                        if(ipo != null)
                        {
                            AssignedItemProperty aip = Model.SelectedItem.AssignedItemProperties.SingleOrDefault(
                                    x => x.ItemPropertyResref == ip.Resref &&
                                    x.ItemPropertyOptionResref == ipo.Resref
                                );

                            if (aip != null)
                            {
                                Model.SelectedItem.AssignedItemProperties.Remove(aip);
                            }

                            aip = new AssignedItemProperty
                            {
                                ItemPropertyResref = ip.Resref,
                                ItemPropertyType = ip.ItemPropertyType,
                                ItemPropertyOptionResref = ipo.Resref,
                                ItemPropertyOptionValueName = optionValue.Key,
                                ItemPropertyOptionValueValue = optionValue.Value
                            };

                            Model.SelectedItem.AssignedItemProperties.Add(aip);
                            
                        }
                    }

                }
            }
        }

        private void RemoveItemProperty(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedItem != null)
            {
                Model.SelectedItem.AssignedItemProperties.Remove(Model.SelectedAssignedItemProperty);
            }
        }

        private void AvailablePropertiesDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddItemProperty(sender, e);
        }

        private void AssignedPropertiesDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RemoveItemProperty(sender, e);
        }
    }
}
