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
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for PaintObjectsControl.xaml
    /// </summary>
    public partial class PaintObjectsControl : UserControl
    {
        private PaintObjectsVM Model { get; set; }
        public event EventHandler<EventArgs> OnModeChanged;

        public PaintObjectsControl()
        {
            InitializeComponent();
            Model = new PaintObjectsVM();
            PopulateModel();
        }

        private void PopulateModel()
        {
            Model.Creatures = WorkingDataManager.GetAllGameObjects<Creature>(ModulePaths.CreaturesDirectory);
            Model.Items = WorkingDataManager.GetAllGameObjects<Item>(ModulePaths.ItemsDirectory);
            Model.Placeables = WorkingDataManager.GetAllGameObjects<Placeable>(ModulePaths.PlaceablesDirectory);
            Model.PaintMode = PaintObjectModeTypeEnum.None;
        }

    }
}
