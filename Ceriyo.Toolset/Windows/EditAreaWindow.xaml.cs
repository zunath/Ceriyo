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
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
