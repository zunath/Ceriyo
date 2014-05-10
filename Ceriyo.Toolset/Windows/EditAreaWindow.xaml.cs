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
            SetDataContexts();
        }

        public EditAreaWindow(Area area)
        {
            InitializeComponent();
            Model = new EditAreaVM();
            Model.Comments = area.Comments;
            Model.Description = area.Description;
            Model.Height = area.MapHeight;
            Model.Name = area.Name;

            SetDataContexts();
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
    }
}
