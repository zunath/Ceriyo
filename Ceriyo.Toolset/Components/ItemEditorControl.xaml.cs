using System.Collections.Generic;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Ceriyo.Toolset.Components
{
    public partial class ItemEditorControl
    {
        private ItemEditorVM Model { get; set; }

        public ItemEditorControl()
        {
            InitializeComponent();
            Model = new ItemEditorVM();
            DataContext = Model;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Item item = new Item();
            string resref = GameResourceProcessor.GenerateUniqueResref(Model.Items.Cast<IGameObject>().ToList(), item.CategoryName);

            item.Name = resref;
            item.Tag = resref;
            item.Resref = resref;

            item.InventoryGraphic = lbInventoryGraphic.Items[0] as GameResource;
            item.WorldGraphic = lbWorldGraphic.Items[0] as GameResource;
            item.ItemTypeResref = ((ItemType) lbItemType.Items[0]).Resref;
            item.ItemRequirements = BuildItemRequirements();
            Model.Items.Add(item);
            int index = lbItems.Items.IndexOf(item);
            lbItems.SelectedItem = lbItems.Items[index];
            
            
        }

        private BindingList<ItemClassRequirement> BuildItemRequirements()
        {
            BindingList<ItemClassRequirement> requirements = new BindingList<ItemClassRequirement>();

            foreach (CharacterClass charClass in Model.CharacterClasses)
            {
                ItemClassRequirement req = new ItemClassRequirement(false, charClass.Name)
                {
                    ClassName = charClass.Name,
                    ClassResref = charClass.Resref,
                    IsAvailable = true,
                    LevelRequired = 0
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
            FileOperationResultType result = WorkingDataManager.ReplaceAllGameObjectFiles(Model.Items.Cast<IGameObject>().ToList(), WorkingPaths.ItemsDirectory);

            if (result != FileOperationResultType.Success)
            {
                MessageBox.Show("Unable to save items.", "Saving items failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackDataManager.GetGameResources(ResourceType.Graphic, ResourceSubType.InventoryIcon);
            GameResource graphic = new GameResource("", "(No Graphic)", ResourceType.None, ResourceSubType.None);
            Model.Graphics.Insert(0, graphic);

            Model.Items = WorkingDataManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.ItemTypes = WorkingDataManager.GetAllGameObjects<ItemType>(ModulePaths.ItemTypesDirectory);
            Model.Scripts = WorkingDataManager.GetAllScriptNames();
            Model.AvailableItemProperties = WorkingDataManager.GetAllGameObjects<ItemProperty>(ModulePaths.ItemPropertiesDirectory);
            Model.CharacterClasses = WorkingDataManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory);

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
            Model.IsItemSelected = item != null;
            
            if(item != null)
            {
                lbItemType.SelectedItem = Model.ItemTypes.SingleOrDefault(x => x.Resref == item.ItemTypeResref);
            }
        }

        private void ItemTypeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (lbItemType.SelectedItem != null)
            {
                Model.SelectedItem.ItemTypeResref = ((ItemType) lbItemType.SelectedItem).Resref;
            }
        }

        private void InventoryGraphicSelected(object sender, SelectionChangedEventArgs e)
        {
            GameResource resource = lbInventoryGraphic.SelectedItem as GameResource;

            if (resource != null)
            {
                if (resource.ResourceType == ResourceType.None)
                {
                    imgInventoryGraphic.Source = null;
                }
                else
                {
                    BitmapImage image = GameResourceProcessor.ToBitmapImage(resource);
                    Model.SelectedItem.InventoryGraphic = resource;
                    imgInventoryGraphic.Source = image;
                }
            }
        }

        private void WorldGraphicSelected(object sender, SelectionChangedEventArgs e)
        {
            GameResource resource = lbWorldGraphic.SelectedItem as GameResource;

            if (resource != null)
            {
                if (resource.ResourceType == ResourceType.None)
                {
                    imgWorldGraphic.Source = null;
                }
                else
                {
                    BitmapImage image = GameResourceProcessor.ToBitmapImage(resource);
                    Model.SelectedItem.WorldGraphic = resource;
                    imgWorldGraphic.Source = image;
                }
            }
        }

        private void AddItemProperty(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedItem == null || tvAvailableProperties.SelectedItem == null) return;

            Type type = tvAvailableProperties.SelectedItem.GetType();

            // Item property selected (I.E: Ability Bonus)
            if (type == typeof(ItemProperty))
            {
                ItemProperty ip = tvAvailableProperties.SelectedItem as ItemProperty;
                AssignedItemProperty aip = Model.SelectedItem.AssignedItemProperties.SingleOrDefault(x => x.ItemPropertyResref == ip.Resref);

                if (ip == null || string.IsNullOrEmpty(ip.Resref) || ip.Options.Count > 0 || aip != null) return;

                aip = new AssignedItemProperty
                {
                    ItemPropertyType = ip.ItemPropertyType,
                    ItemPropertyResref = ip.Resref
                };

                Model.SelectedItem.AssignedItemProperties.Add(aip);
            }
                // Option selected (I.E: Strength)
            else if (type == typeof(ItemPropertyOption))
            {
                ItemPropertyOption ipo = tvAvailableProperties.SelectedItem as ItemPropertyOption;
                ItemProperty ip = Model.AvailableItemProperties.SingleOrDefault(x => x.Resref == ipo.ParentItemPropertyResref);
                AssignedItemProperty aip = Model.SelectedItem.AssignedItemProperties
                    .SingleOrDefault(x => x.ItemPropertyResref == ipo.ParentItemPropertyResref && 
                                          x.ItemPropertyOptionResref == ipo.Resref);


                if (ipo == null || string.IsNullOrWhiteSpace(ipo.Resref) || ipo.Values.Count > 0 || aip != null) return;
                aip = new AssignedItemProperty
                {
                    ItemPropertyOptionResref = ipo.Resref,
                    ItemPropertyResref = ipo.ParentItemPropertyResref,
                    ItemPropertyType = ip.ItemPropertyType
                };

                Model.SelectedItem.AssignedItemProperties.Add(aip);
            }
                // Option value selected (I.E: +15)
            else if(type == typeof(ItemPropertyOptionValue))
            {
                ItemPropertyOptionValue optionValue = tvAvailableProperties.SelectedItem as ItemPropertyOptionValue;
                ItemProperty ip = Model.AvailableItemProperties.SingleOrDefault(x => x.Options.SingleOrDefault(y => y.Resref == optionValue.ParentOptionResref) != null);

                if (ip == null || string.IsNullOrWhiteSpace(ip.Resref) || optionValue == null) return;
                ItemPropertyOption ipo = ip.Options.SingleOrDefault(x => x.Resref == optionValue.ParentOptionResref);

                if (ipo == null) return;

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

        private void RemoveItemProperty(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedItem == null) return;
            Model.SelectedItem.AssignedItemProperties.Remove(Model.SelectedAssignedItemProperty);

            if (Model.SelectedItem.AssignedItemProperties.Count > 0)
            {
                Model.SelectedAssignedItemProperty = Model.SelectedItem.AssignedItemProperties[0];
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

        public void ClassesModified(object sender, EditorItemChangedEventArgs e)
        {
            CharacterClass charClass = Model.CharacterClasses.SingleOrDefault(x => x.Resref == e.Resref);

            if (e.IsChanged)
            {
                if (charClass == null) return;
                int index = Model.CharacterClasses.IndexOf(charClass);
                Model.CharacterClasses[index] = e.GameObject as CharacterClass;

                foreach (Item item in Model.Items)
                {
                    ItemClassRequirement requirement =
                        item.ItemRequirements.SingleOrDefault(x => x.ClassResref == e.Resref);
                    index = item.ItemRequirements.IndexOf(requirement);
                    item.ItemRequirements[index] = new ItemClassRequirement(false, e.GameObject.Name)
                    {
                        ClassResref = e.Resref,
                        IsAvailable = item.ItemRequirements[index].IsAvailable,
                        LevelRequired = item.ItemRequirements[index].LevelRequired
                    };


                }
            }
            else if (e.IsAdded)
            {
                Model.CharacterClasses.Add(e.GameObject as CharacterClass);

                foreach (Item item in Model.Items)
                {
                    ItemClassRequirement requirement = new ItemClassRequirement(false, e.GameObject.Name)
                    {
                        ClassResref = e.GameObject.Resref,
                        IsAvailable = true,
                        LevelRequired = 0
                    };
                    item.ItemRequirements.Add(requirement);
                }
            }
            else
            {
                Model.CharacterClasses.Remove(charClass);

                foreach (Item item in Model.Items)
                {
                    ItemClassRequirement requirement =
                        item.ItemRequirements.SingleOrDefault(x => x.ClassResref == e.Resref);

                    item.ItemRequirements.Remove(requirement);
                }
            }
        }

    }
}
