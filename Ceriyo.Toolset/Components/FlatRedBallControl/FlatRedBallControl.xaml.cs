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

namespace Ceriyo.Toolset
{
    /// <summary>
    /// Interaction logic for FlatRedBallControl.xaml
    /// </summary>
    public partial class FlatRedBallControl : UserControl
    {
        public FlatRedBallControl()
        {
            InitializeComponent();
        }

        public IntPtr Handle
        {
            get { return GamePanel.Handle; }
        }
    }
}
