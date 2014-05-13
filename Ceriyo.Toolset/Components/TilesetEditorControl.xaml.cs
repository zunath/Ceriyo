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
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for TilesetEditorControl.xaml
    /// </summary>
    public partial class TilesetEditorControl : UserControl
    {
        private TilesetEditorVM Model { get; set; }

        public TilesetEditorControl()
        {
            InitializeComponent();
            Model = new TilesetEditorVM();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            lbTilesets.DataContext = Model;
            ddlGraphics.DataContext = Model;
            txtName.DataContext = Model;
            txtTag.DataContext = Model;
            txtResref.DataContext = Model;
        }

        private void New(object sender, RoutedEventArgs e)
        {
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
        }

        public void Save(object sender, EventArgs e)
        {

        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackDataManager.GetGameResources(ResourceTypeEnum.Graphic);
        }

    }
}
