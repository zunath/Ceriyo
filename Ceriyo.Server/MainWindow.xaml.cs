using Ceriyo.Data.EventArguments;
using Ceriyo.Data.Server;
using Ceriyo.Data.Settings;
using Ceriyo.Data.ViewModels;
using FlatRedBall.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using Ceriyo.Data.Engine;

namespace Ceriyo.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ServerVM Model { get; set; }
        private BackgroundWorker GameThread { get; set; }
        private ServerGame Game { get; set; } // Do not access directly from the GUI thread.
        private Timer GUIToGameUpdateTimer { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Model = new ServerVM();
            DataContext = Model;
            
            // Every 2 seconds, send the current state of the GUI to the Game thread
            GUIToGameUpdateTimer = new Timer(2000.0f);
            GUIToGameUpdateTimer.Elapsed += GUIToGameUpdateTimer_Elapsed;

            // Set up the Game thread
            GameThread = new BackgroundWorker();
            GameThread.DoWork += RunGameThread;
            GameThread.ProgressChanged += GameThread_ProgressChanged;
            GameThread.RunWorkerCompleted += GameThread_RunWorkerCompleted;
            GameThread.WorkerReportsProgress = true;

            LoadModules();
        }


        #region Game Thread

        private void RunGameThread(object sender, DoWorkEventArgs e)
        {
            Game = new ServerGame(e.Argument as ServerStartupArgs);
                
            Game.OnSignalGUIUpdate += GameToGUIUpdate;
            Game.OnGameStarting += Game_OnGameStarting;
            Game.OnGameExiting += Game_OnGameExiting;

            Game.Run();    

            Game.OnSignalGUIUpdate -= GameToGUIUpdate;
            Game.OnGameStarting -= Game_OnGameStarting;
            Game.OnGameExiting -= Game_OnGameExiting;
        }

        // Updates from the game thread are sent to the GUI thread every few seconds.
        private void GameToGUIUpdate(object sender, ServerStatusUpdateEventArgs e)
        {
            // Fires GameThread_ProgressChanged on the GUI thread.
            GameThread.ReportProgress(0, e);
        }

        private void GameThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw e.Error;
            }
        }

        private void Game_OnGameStarting(object sender, EventArgs e)
        {
            ServerStatusUpdateEventArgs args = new ServerStatusUpdateEventArgs
            {
                GameJustStarted = true
            };

            GameThread.ReportProgress(0, args);
        }

        private void Game_OnGameExiting(object sender, EventArgs e)
        {
            ServerStatusUpdateEventArgs args = new ServerStatusUpdateEventArgs
            {
                GameJustShutDown = true
            };

            GameThread.ReportProgress(100, args);
        }

        #endregion


        #region UI Thread

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            string path = EnginePaths.SettingsDirectory + "ServerSettings" + EnginePaths.DataExtension;

            FileManager.XmlSerialize(Model.ServerSettings, path);
        }

        private void LoadSettings()
        {
            string path = EnginePaths.SettingsDirectory + "ServerSettings" + EnginePaths.DataExtension;

            if (FileManager.FileExists(path))
            {
                Model.ServerSettings = FileManager.XmlDeserialize<ServerSettings>(path);
            }
        }

        private void LoadModules()
        {
            Model.Modules = new BindingList<string>();
            string[] filePaths = Directory.GetFiles(EnginePaths.ModulesDirectory, "*" + EnginePaths.ModuleExtension);

            foreach (string file in filePaths)
            {
                Model.Modules.Add(Path.GetFileNameWithoutExtension(file));
            }

            Model.Modules.Insert(0, "-- Select a Module --");
            Model.SelectedModule = Model.Modules[0];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
            LoadModules();
        }

        private void AddToBlacklist(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Model.BlacklistUsername) &&
                !Model.ServerSettings.Blacklist.Contains(Model.BlacklistUsername))
            {
                Model.ServerSettings.Blacklist.Add(Model.BlacklistUsername);
            }

            Model.BlacklistUsername = string.Empty;
        }

        private void RemoveSelectedFromBlacklist(object sender, RoutedEventArgs e)
        {
            List<string> namesToRemove = lbBlacklist.SelectedItems.Cast<string>().ToList();

            foreach(string name in namesToRemove)
            {
                Model.ServerSettings.Blacklist.Remove(name);
            }
        }

        private void GUIToGameUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Game != null)
            {
                ServerGUIStatus status = new ServerGUIStatus
                {
                    Settings = Model.ServerSettings,
                    IsServerRunning = Model.IsServerRunning
                };

                Game.GUIStatusUpdateQueue.Enqueue(status);
            }
        }

        private void GameThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ServerStatusUpdateEventArgs args = e.UserState as ServerStatusUpdateEventArgs;

            if (args == null) return;

            if (args.GameJustShutDown)
            {
                Model.ConnectedUsernames.Clear();
                btnStartStop.IsEnabled = true;
                btnStartStop.Content = "Start Server";
                GUIToGameUpdateTimer.Stop();
            }
            else if (args.GameJustStarted)
            {
                btnStartStop.IsEnabled = true;
                btnStartStop.Content = "Stop Server";
                GUIToGameUpdateTimer.Start();
            }
            else
            {
                List<string> toRemove = Model.ConnectedUsernames.Except(args.ConnectedUsernames).ToList();
                List<string> toAdd = args.ConnectedUsernames.Except(Model.ConnectedUsernames).ToList();

                foreach (string current in toRemove)
                {
                    Model.ConnectedUsernames.Remove(current);
                }

                foreach (string current in toAdd)
                {
                    Model.ConnectedUsernames.Add(current);
                }
            }
        }

        private void StartStopServer(object sender, RoutedEventArgs e)
        {
            if (Model.IsServerRunning)
            {
                Model.IsServerRunning = false;
            }
            else
            {
                if (Model.SelectedModule == Model.Modules[0])
                {
                    MessageBox.Show("Please select a module to load.", 
                        "Select a Module", 
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                ServerStartupArgs startUpArgs = new ServerStartupArgs
                {
                    Port = Model.ServerSettings.Port,
                    ServerPassword = Model.ServerSettings.PlayerPassword,
                    ModuleFileName = Model.SelectedModule
                };

                GameThread.RunWorkerAsync(startUpArgs);
                Model.IsServerRunning = true;
            }

            btnStartStop.IsEnabled = false;
        }

        #endregion


    }
}
