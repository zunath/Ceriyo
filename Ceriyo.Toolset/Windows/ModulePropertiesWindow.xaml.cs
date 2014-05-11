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
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ModulePropertiesWindow.xaml
    /// </summary>
    public partial class ModulePropertiesWindow : Window
    {
        private ModulePropertiesVM Model { get; set; }

        public ModulePropertiesWindow()
        {
            InitializeComponent();
            InitializeModel();
            SetDataContexts();
            SetLimits();
        }

        private void InitializeModel()
        {
            Model = new ModulePropertiesVM();
            GameModule gameModule = WorkingDataManager.GetGameModule();
            Model.Comments = gameModule.Comments;
            Model.Description = gameModule.Description;
            Model.LocalVariables = gameModule.LocalVariables;
            Model.Name = gameModule.Name;
            Model.Resref = gameModule.Resref;
            Model.Scripts = WorkingDataManager.GetAllScriptNames();
            Model.Tag = gameModule.Tag;
        }

        private void SetDataContexts()
        {
            txtComments.DataContext = Model;
            txtDescription.DataContext = Model;
            txtName.DataContext = Model;
            txtResref.DataContext = Model;
            txtTag.DataContext = Model;
            ddlOnHeartbeatScript.DataContext = Model;
            ddlOnModuleLoadScript.DataContext = Model;
            ddlOnPlayerDeathScript.DataContext = Model;
            ddlOnPlayerEnterScript.DataContext = Model;
            ddlOnPlayerLeavingScript.DataContext = Model;
            ddlOnPlayerLeftScript.DataContext = Model;
            ddlOnPlayerRespawnScript.DataContext = Model;
            numMaxLevel.DataContext = Model;
            dgLevelChart.DataContext = Model;
            dgLocalVariables.DataContext = Model;
        }

        private void SetLimits()
        {
            numMaxLevel.Minimum = 1;
            numMaxLevel.Maximum = EngineConstants.MaxLevel;
        }
    }
}
