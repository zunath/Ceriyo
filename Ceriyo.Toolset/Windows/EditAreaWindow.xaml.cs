using System;
using System.Collections.Generic;
using System.Windows;
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

        public EditAreaWindow()
        {
            InitializeComponent();
            Model = new EditAreaVM();
            InitializeModel();
            SetDataContexts();
            SetLimits();
        }

        public EditAreaWindow(Area area)
        {
            InitializeComponent();
            Model = new EditAreaVM();
            InitializeModel(area);
            SetDataContexts();
            SetLimits();
        }

        private void InitializeModel()
        {
            Model.Tilesets = WorkingDataManager.GetAllGameObjects(ModulePaths.TilesetsDirectory) as List<Tileset>;
            Model.Scripts = WorkingDataManager.GetAllScriptNames() as List<string>;
            
        }

        private void InitializeModel(Area area)
        {
            Model.Comments = area.Comments;
            Model.Description = area.Description;
            Model.Height = area.MapHeight;
            Model.Name = area.Name;
            Model.Resref = area.Resref;
            Model.Tag = area.Tag;
            Model.Width = area.MapWidth;
            Model.OnAreaEnterScript = area.Scripts[ScriptEventTypeEnum.OnAreaEnter];
            Model.OnAreaExitScript = area.Scripts[ScriptEventTypeEnum.OnAreaExit];
            Model.OnAreaHeartbeatScript = area.Scripts[ScriptEventTypeEnum.OnHeartbeat];

            InitializeModel();
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
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Area area = new Area(Model.Name, Model.Tag, Model.Resref, Model.Width, Model.Height, EngineConstants.AreaMaxLayers);
            area.Comments = Model.Comments;
            area.Description = Model.Description;
            area.LocalVariables = Model.LocalVariables;
            area.Scripts.Add(ScriptEventTypeEnum.OnAreaEnter, Model.OnAreaEnterScript);
            area.Scripts.Add(ScriptEventTypeEnum.OnAreaExit, Model.OnAreaExitScript);
            area.Scripts.Add(ScriptEventTypeEnum.OnHeartbeat, Model.OnAreaHeartbeatScript);

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
}
