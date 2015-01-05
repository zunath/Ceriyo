using System;
using System.Windows.Controls;

namespace Ceriyo.Toolset.Components.FRBControl
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
