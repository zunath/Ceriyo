using System.ComponentModel;
using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for NewModuleWindow.xaml
    /// </summary>
    public partial class NewModuleWindow : Window
    {
        private NewModuleVM Model { get; set; }

        public NewModuleWindow()
        {
            InitializeComponent();
            Model = new NewModuleVM();
            SetDataContexts();
            SetLimits();
            txtName.Focus();
        }

        private void SetDataContexts()
        {
            txtName.DataContext = Model;
            txtTag.DataContext = Model;
            txtResref.DataContext = Model;
        }

        private void SetLimits()
        {
            txtName.MaxLength = EngineConstants.NameMaxLength;
            txtTag.MaxLength = EngineConstants.TagMaxLength;
            txtResref.MaxLength = EngineConstants.ResrefMaxLength;
        }

        private void btnCreateModule_Click(object sender, RoutedEventArgs e)
        {
            ModuleDataManager.CreateModule(Model.Name, Model.Tag, Model.Resref);
            ModuleDataManager.LoadModule(Model.Resref);
            ResourcePackDataManager.BuildModule(new BindingList<string>());
            ModuleDataManager.SaveModule(Model.Resref);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
