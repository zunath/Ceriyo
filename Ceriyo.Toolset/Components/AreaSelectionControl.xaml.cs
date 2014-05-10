using System;
using System.Collections.Generic;
using System.IO;
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
using Ceriyo.Data;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using FlatRedBall.IO;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AreaSelectionControl.xaml
    /// </summary>
    public partial class AreaSelectionControl : UserControl
    {
        protected AreaSelectionVM Model { get; set; }
        public event EventHandler<AreaEventArgs> OnAreaOpen;

        public AreaSelectionControl()
        {
            InitializeComponent();
            this.Model = new AreaSelectionVM();
            this.Loaded += LoadWindow;

            SetDataContexts();
        }

        private void LoadWindow(object sender, RoutedEventArgs e)
        {
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            lbAreas.DataContext = Model;
        }

        public void Populate()
        {
            Model.Areas.Clear();
            string[] areaFiles = Directory.GetFiles(WorkingPaths.AreasDirectory);
            foreach (string area in areaFiles)
            {
                Model.Areas.Add(FileManager.XmlDeserialize<Area>(area));
            }
        }
    }
}
