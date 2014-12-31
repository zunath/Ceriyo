using System;
using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for NewModuleWindow.xaml
    /// </summary>
    public partial class NewModuleWindow
    {
        private NewModuleVM Model { get; set; }

        public event EventHandler<GameModuleEventArgs> OnModuleCreated;

        public NewModuleWindow()
        {
            InitializeComponent();
            Model = new NewModuleVM();
            DataContext = Model;
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
            ModuleDataManager.CreateModule(Model.Name, Model.Tag, Model.Resref);

            if (OnModuleCreated != null)
            {
                OnModuleCreated(this, new GameModuleEventArgs(Model.Resref));
            }

            ResourcePackDataManager.BuildModule(ResourcePackDataManager.GetAllResourcePackNames());
            ModuleDataManager.SaveModule(Model.Resref);
            

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
