using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ModulePropertiesWindow.xaml
    /// </summary>
    public partial class ModulePropertiesWindow
    {
        private ModulePropertiesVM Model { get; set; }

        public ModulePropertiesWindow()
        {
            InitializeComponent();
            Model = new ModulePropertiesVM();
            DataContext = Model;
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

            Show();
        }

        private void SetLimits()
        {
            numMaxLevel.Minimum = 1;
            numMaxLevel.Maximum = EngineConstants.MaxLevel;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            GameModule module = new GameModule(Model.Name, Model.Tag, Model.Resref, Model.Description, Model.Comments)
            {
                Levels = new LevelChart(Model.Levels),
                LocalVariables = Model.LocalVariables
            };

            module.Scripts[ScriptEventTypeEnum.OnModuleLoad] = Model.OnModuleLoadScript;
            module.Scripts[ScriptEventTypeEnum.OnAreaHeartbeat] = Model.OnHeartbeatScript;
            module.Scripts[ScriptEventTypeEnum.OnModuleLoad] = Model.OnModuleLoadScript;
            module.Scripts[ScriptEventTypeEnum.OnPlayerDeath] = Model.OnPlayerDeathScript;
            module.Scripts[ScriptEventTypeEnum.OnPlayerDying] = Model.OnPlayerDyingScript;
            module.Scripts[ScriptEventTypeEnum.OnModulePlayerEnter] = Model.OnPlayerEnterScript;
            module.Scripts[ScriptEventTypeEnum.OnModulePlayerLeaving] = Model.OnPlayerLeavingScript;
            module.Scripts[ScriptEventTypeEnum.OnModulePlayerLeft] = Model.OnPlayerLeftScript;
            module.Scripts[ScriptEventTypeEnum.OnPlayerRespawn] = Model.OnPlayerRespawnScript;

            FileOperationResultTypeEnum result = WorkingDataManager.SaveModuleSettings(module);

            if (result == FileOperationResultTypeEnum.Success)
            {
                Hide();
            }
            else
            {
                MessageBox.Show("Failed to save module properties.", "Failed to save", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

    }
}
