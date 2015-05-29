using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for NewAreaWindow.xaml
    /// </summary>
    public partial class EditAreaWindow
    {
        private EditAreaVM Model { get; set; }
        public event EventHandler<AreaPropertiesChangedEventArgs> OnSaveAreaProperties;
        private bool IsEditing { get; set; }

        public EditAreaWindow()
        {
            InitializeComponent();
            Model = new EditAreaVM();
        }

        private void PopulateModel(Area area)
        {
            Model.Tilesets = WorkingDataManager.GetAllGameObjects<Tileset>(ModulePaths.TilesetsDirectory);
            Model.Scripts = WorkingDataManager.GetAllScriptNames();
            Model.Comments = area.Comments;
            Model.Description = area.Description;
            Model.Name = area.Name;
            Model.Resref = area.Resref;
            Model.Tag = area.Tag;
            Model.Width = area.MapWidth <= 0 ? EngineConstants.AreaMaxWidth : area.MapWidth;
            Model.Height = area.MapHeight <= 0 ? EngineConstants.AreaMaxHeight : area.MapHeight;
            Model.LocalVariables = area.LocalVariables;
            Model.SelectedTileset = Model.Tilesets.SingleOrDefault(x => x.Resref == area.AreaTilesetResref);

            if (area.Scripts.ContainsKey(ScriptEventType.OnAreaEnter))
            {
                Model.OnAreaEnterScript = area.Scripts[ScriptEventType.OnAreaEnter];
            }

            if (area.Scripts.ContainsKey(ScriptEventType.OnAreaExit))
            {
                Model.OnAreaExitScript = area.Scripts[ScriptEventType.OnAreaExit];
            }

            if (area.Scripts.ContainsKey(ScriptEventType.OnAreaHeartbeat))
            {
                Model.OnAreaHeartbeatScript = area.Scripts[ScriptEventType.OnAreaHeartbeat];
            }
        }

        private void SetLimits()
        {
            txtName.MaxLength = EngineConstants.NameMaxLength;
            txtTag.MaxLength = EngineConstants.TagMaxLength;
            txtResref.MaxLength = EngineConstants.ResrefMaxLength;
            txtDescription.MaxLength = EngineConstants.DescriptionMaxLength;
            txtComments.MaxLength = EngineConstants.CommentsMaxLength;
            
            numHeight.Maximum = EngineConstants.AreaMaxHeight;
            numWidth.Maximum = EngineConstants.AreaMaxWidth;
            numHeight.Minimum = EngineConstants.AreaMinHeight;
            numWidth.Minimum = EngineConstants.AreaMinWidth;
        }

        public void Open(Area area, bool isEditing)
        {
            DataContext = Model;
            SetLimits();
            PopulateModel(area);
            IsEditing = isEditing;
            txtResref.IsEnabled = !IsEditing;
            ddlTileset.IsEnabled = !isEditing;
            numHeight.IsEnabled = !isEditing;
            numWidth.IsEnabled = !isEditing;

            Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Area area = new Area(Model.Name, Model.Tag, Model.Resref, Model.Width, Model.Height, EngineConstants.AreaMaxLayers);

            if (WorkingDataManager.DoesGameObjectExist(area) && !IsEditing)
            {
                MessageBox.Show("An area with that resref already exists. Please select a different resref.", "Resref in use", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                area.Comments = Model.Comments;
                area.Description = Model.Description;
                area.LocalVariables = Model.LocalVariables;
                area.Scripts.Add(ScriptEventType.OnAreaEnter, Model.OnAreaEnterScript);
                area.Scripts.Add(ScriptEventType.OnAreaExit, Model.OnAreaExitScript);
                area.Scripts.Add(ScriptEventType.OnAreaHeartbeat, Model.OnAreaHeartbeatScript);
                area.AreaTilesetResref = Model.SelectedTileset.Resref;

                FileOperationResultType result = WorkingDataManager.SaveGameObjectFile(area);

                if (result == FileOperationResultType.Success)
                {
                    if (OnSaveAreaProperties != null)
                    {
                        OnSaveAreaProperties(this, new AreaPropertiesChangedEventArgs(area, IsEditing));
                    }

                    Close();
                }
                else if (result == FileOperationResultType.Failure)
                {
                    MessageBox.Show("Could not save area.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
