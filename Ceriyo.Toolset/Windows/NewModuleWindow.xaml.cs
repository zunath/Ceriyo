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
        private ModuleDataManager ModuleManager { get; set; }
        private ResourcePackDataManager ResourcePackManager { get; set; }

        public NewModuleWindow()
        {
            InitializeComponent();
            Model = new NewModuleVM();
            ModuleManager = new ModuleDataManager();
            ResourcePackManager = new ResourcePackDataManager();
            this.DataContext = Model;
            SetLimits();
            txtName.Focus();
        }

        private void SetLimits()
        {
            txtName.MaxLength = EngineConstants.NameMaxLength;
            txtTag.MaxLength = EngineConstants.TagMaxLength;
            txtResref.MaxLength = EngineConstants.ResrefMaxLength;
        }

        private void btnCreateModule_Click(object sender, RoutedEventArgs e)
        {
            ModuleManager.CreateModule(Model.Name, Model.Tag, Model.Resref);
            ModuleManager.LoadModule(Model.Resref, true);
            ResourcePackManager.BuildModule(new BindingList<string>());
            ModuleManager.SaveModule(Model.Resref);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
