using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for NewAreaWindow.xaml
    /// </summary>
    public partial class EditAreaWindow : Window
    {
        private EditAreaVM Model { get; set; }
        public event EventHandler<GameObjectEventArgs> OnSaveArea;
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
            Model.Height = area.MapHeight;
            Model.Name = area.Name;
            Model.Resref = area.Resref;
            Model.Tag = area.Tag;
            Model.Width = area.MapWidth;
            Model.LocalVariables = area.LocalVariables;
            Model.SelectedTileset = Model.Tilesets.SingleOrDefault(x => x.Resref == area.AreaTileset.Resref);

            if (area.Scripts.ContainsKey(ScriptEventTypeEnum.OnAreaEnter))
            {
                Model.OnAreaEnterScript = area.Scripts[ScriptEventTypeEnum.OnAreaEnter];
            }

            if (area.Scripts.ContainsKey(ScriptEventTypeEnum.OnAreaExit))
            {
                Model.OnAreaExitScript = area.Scripts[ScriptEventTypeEnum.OnAreaExit];
            }

            if (area.Scripts.ContainsKey(ScriptEventTypeEnum.OnAreaHeartbeat))
            {
                Model.OnAreaHeartbeatScript = area.Scripts[ScriptEventTypeEnum.OnAreaHeartbeat];
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

        private void SetDataContexts()
        {
            txtName.DataContext = Model;
            txtTag.DataContext = Model;
            txtResref.DataContext = Model;
            txtComments.DataContext = Model;
            txtDescription.DataContext = Model;

            ddlTileset.DataContext = Model;
            numHeight.DataContext = Model;
            numWidth.DataContext = Model;

            cboOnAreaEnter.DataContext = Model;
            cboOnAreaExit.DataContext = Model;
            cboOnAreaHeartbeat.DataContext = Model;

            dgLocalVariables.DataContext = Model;
        }

        public void Open(Area area, bool isEditing)
        {
            SetDataContexts();
            SetLimits();
            PopulateModel(area);
            this.IsEditing = isEditing;
            txtResref.IsEnabled = !IsEditing;

            this.Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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
                area.Scripts.Add(ScriptEventTypeEnum.OnAreaEnter, Model.OnAreaEnterScript);
                area.Scripts.Add(ScriptEventTypeEnum.OnAreaExit, Model.OnAreaExitScript);
                area.Scripts.Add(ScriptEventTypeEnum.OnAreaHeartbeat, Model.OnAreaHeartbeatScript);
                area.AreaTileset = Model.SelectedTileset;

                FileOperationResultTypeEnum result = WorkingDataManager.SaveGameObjectFile(area);

                if (result == FileOperationResultTypeEnum.Success)
                {
                    if (OnSaveArea != null)
                    {
                        OnSaveArea(this, new GameObjectEventArgs(area));
                    }

                    this.Close();
                }
                else if (result == FileOperationResultTypeEnum.Failure)
                {
                    MessageBox.Show("Could not save area.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
