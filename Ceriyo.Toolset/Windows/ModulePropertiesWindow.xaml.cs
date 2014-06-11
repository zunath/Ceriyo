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
using Ceriyo.Data.Enumerations;
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
            Model = new ModulePropertiesVM();
            SetDataContexts();
            SetLimits();
        }

        public void Open()
        {
            GameModule gameModule = WorkingDataManager.GetGameModule();
            Model.Comments = gameModule.Comments;
            Model.Description = gameModule.Description;
            Model.LocalVariables = gameModule.LocalVariables;
            Model.Name = gameModule.Name;
            Model.Resref = gameModule.Resref;
            Model.Scripts = WorkingDataManager.GetAllScriptNames();
            Model.Tag = gameModule.Tag;
            Model.Levels = gameModule.Levels.Levels;

            this.Show();
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

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            GameModule module = new GameModule(Model.Name, Model.Tag, Model.Resref, Model.Description, Model.Comments);
            module.Levels = new LevelChart(Model.Levels);
            module.LocalVariables = Model.LocalVariables;

            module.Scripts.Add(ScriptEventTypeEnum.OnAreaHeartbeat, Model.OnHeartbeatScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModuleLoad, Model.OnModuleLoadScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnPlayerDeath, Model.OnPlayerDeathScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnPlayerDying, Model.OnPlayerDyingScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModulePlayerEnter, Model.OnPlayerEnterScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModulePlayerLeaving, Model.OnPlayerLeavingScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModulePlayerLeft, Model.OnPlayerLeftScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnPlayerRespawn, Model.OnPlayerRespawnScript);

            FileOperationResultTypeEnum result = WorkingDataManager.SaveModuleSettings(module);

            if (result == FileOperationResultTypeEnum.Success)
            {
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to save module properties.", "Failed to save", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
