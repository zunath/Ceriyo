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
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AreaToolbarControl.xaml
    /// </summary>
    public partial class AreaToolbarControl : UserControl
    {
        private AreaToolbarVM Model { get; set; }

        public AreaToolbarControl()
        {
            InitializeComponent();
            Model = new AreaToolbarVM();
            this.DataContext = Model;
        }
    }
}
