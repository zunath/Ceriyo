﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Settings;
using Ceriyo.Data.ViewModels;
using FlatRedBall.IO;
using Ceriyo.Data.EventArguments;

namespace Ceriyo.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerVM Model { get; set; }
        private BackgroundWorker GameThread { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            this.Model = new ServerVM();
            SetDataContexts();
            GameThread = new BackgroundWorker();
            GameThread.DoWork += RunGameThread;
            GameThread.RunWorkerCompleted += GameThread_RunWorkerCompleted;
            GameThread.WorkerReportsProgress = true;

            Model.IsServerRunning = true;
            GameThread.RunWorkerAsync(); // DEBUG
        }

        #region Game Thread

        private void RunGameThread(object sender, DoWorkEventArgs e)
        {
            try
            {
                ServerGame game = new ServerGame();
                game.OnSignalGUIUpdate += GameThread_UpdateGUI;
                
                game.Run();

                game.OnSignalGUIUpdate -= GameThread_UpdateGUI;
            }
            catch
            {
                throw;
            }
        }

        private void GameThread_UpdateGUI(object sender, ServerStatusUpdateEventArgs e)
        {
            // Data from game thread -> GUI thread
        }

        private void GameThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw e.Error;
            }
        }

        #endregion


        #region UI Thread

        private void SetDataContexts()
        {
            txtAnnouncement.DataContext = Model;
            txtBlacklistUsername.DataContext = Model;
            txtDescription.DataContext = Model;
            txtGMPassword.DataContext = Model;
            txtPlayerPassword.DataContext = Model;
            txtServerMessage.DataContext = Model;
            txtServerName.DataContext = Model;
            txtServerStatus.DataContext = Model;
            numMaxLevel.DataContext = Model;
            numMaxPlayers.DataContext = Model;
            numPort.DataContext = Model;
            ddlPVPType.DataContext = Model;
            ddlModules.DataContext = Model;
            lbBlacklist.DataContext = Model;
            lbGameType.DataContext = Model;
            lblIPAddress.DataContext = Model;
            lbLog.DataContext = Model;
            lbPlayers.DataContext = Model;

        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
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
            List<string> namesToRemove = new List<string>();

            foreach (string name in lbBlacklist.SelectedItems)
            {
                namesToRemove.Add(name);
            }
            foreach(string name in namesToRemove)
            {
                Model.ServerSettings.Blacklist.Remove(name);
            }
        }

        #endregion
    }
}
