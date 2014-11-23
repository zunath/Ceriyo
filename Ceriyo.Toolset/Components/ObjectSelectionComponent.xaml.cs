using System.Windows.Controls;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for ObjectSelectionComponent.xaml
    /// </summary>
    public partial class ObjectSelectionComponent : UserControl
    {
        private ObjectSelectionVM Model { get; set; }

        public ObjectSelectionComponent()
        {
            InitializeComponent();
            Model = new ObjectSelectionVM();
            DataContext = Model;
        }

        public void LoadArea(object sender, GameObjectEventArgs e)
        {
            Area area = e.GameObject as Area;

            Model.Creatures = area.CreatureInstances;
            Model.Items = area.ItemInstances;
            Model.Placeables = area.PlaceableInstances;


        }
    }
}
