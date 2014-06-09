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

namespace Ceriyo.Toolset.Components
{
    public partial class ItemEditorControl : UserControl
    {
        private ItemEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }

        public ItemEditorControl()
        {
            InitializeComponent();
            Model = new ItemEditorVM();
            Processor = new GameResourceProcessor();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            txtComments.DataContext = Model;
            txtDescription.DataContext = Model;
            txtName.DataContext = Model;
            txtResref.DataContext = Model;
            txtTag.DataContext = Model;
            lbAssignedProperties.DataContext = Model;
            lbAvailableProperties.DataContext = Model;
            lbInventoryGraphic.DataContext = Model;
            lbItems.DataContext = Model;
            lbWorldGraphic.DataContext = Model;
            lbItemType.DataContext = Model;
            chkIsPlot.DataContext = Model;
            chkIsStolen.DataContext = Model;
            chkIsUndroppable.DataContext = Model;
            dgLocalVariables.DataContext = Model;
            dgItemRequirements.DataContext = Model;
        }

        public void Save(object sender, EventArgs e)
        {
        }

        public void Open(object sender, EventArgs e)
        {

        }
    }
}
