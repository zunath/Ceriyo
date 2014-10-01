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
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for ClassEditorComponent.xaml
    /// </summary>
    public partial class ClassEditorComponent : UserControl
    {
        private ClassEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public ClassEditorComponent()
        {
            InitializeComponent();
            this.Model = new ClassEditorVM();
            this.Processor = new GameResourceProcessor();
            this.WorkingManager = new WorkingDataManager();
            this.DataContext = Model;
        }


        private void ClassSelected(object sender, SelectionChangedEventArgs e)
        {
            CharacterClass charClass = lbClasses.SelectedItem as CharacterClass;
            Model.SelectedClass = charClass;
            Model.IsClassSelected = charClass == null ? false : true;

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedClass != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this class?", "Delete class?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Classes.Remove(Model.SelectedClass);
                    Model.SelectedClass = null;
                    Model.IsClassSelected = false;
                }
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            CharacterClass charClass = new CharacterClass();
            string resref = Processor.GenerateUniqueResref(Model.Classes.Cast<IGameObject>().ToList(), charClass.CategoryName);

            charClass.Name = resref;
            charClass.Tag = resref;
            charClass.Resref = resref;

            Model.Classes.Add(charClass);
            int index = Model.Classes.IndexOf(charClass);
            Model.SelectedClass = Model.Classes[index];
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Classes.Cast<IGameObject>().ToList(), WorkingPaths.CharacterClassesDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save classes.", "Saving classes failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Classes = WorkingManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory);

            if (Model.Classes.Count > 0)
            {
                Model.SelectedClass = Model.Classes[0];
            }
        }
    }
}
