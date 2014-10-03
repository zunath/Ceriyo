using Ceriyo.Entities.DrawableBatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squid;
using Ceriyo.Library.SquidGUI;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.EventArguments;

namespace Ceriyo.Entities.GUI
{
    public class CharacterSelectionMenuLogic : GUIDrawableBatch
    {
        #region Controls

        private ListBox CharacterSelectionListBox { get; set; }
        private Button CreateButton { get; set; }
        private Button DeleteButton { get; set; }
        private Button EnterServerButton { get; set; }
        private Button DisconnectButton { get; set; }

        #endregion

        #region Events

        public event EventHandler<ResrefEventArgs> OnEnterServer;
        public event EventHandler<EventArgs> OnDisconnected;
        public event EventHandler<EventArgs> OnCreateCharacter;
        public event EventHandler<ResrefEventArgs> OnDeleteCharacter;

        #endregion

        public CharacterSelectionMenuLogic(bool allowDelete, List<Player> characterList)
            : base("CharacterSelectionMenu")
        {
            LoadControls();
            HookEvents();

            DeleteButton.Enabled = allowDelete;
            foreach (Player character in characterList)
            {
                CharacterSelectionListBox.Items.Add(new ListBoxItem
                {
                    Text = character.Name,
                    Size = new Point(100, 50),
                    UserData = character.Resref
                });
            }
        }

        private void LoadControls()
        {
            CharacterSelectionListBox = GetControl("lbCharacters") as ListBox;
            CreateButton = GetControl("btnCreate") as Button;
            DeleteButton = GetControl("btnDelete") as Button;
            EnterServerButton = GetControl("btnEnter") as Button;
            DisconnectButton = GetControl("btnDisconnect") as Button;

        }

        private void HookEvents()
        {
            CreateButton.MouseClick += CreateButton_MouseClick;
            DeleteButton.MouseClick += DeleteButton_MouseClick;
            DisconnectButton.MouseClick += DisconnectButton_MouseClick;
            EnterServerButton.MouseClick += EnterServerButton_MouseClick;

        }

        private void EnterServerButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (OnEnterServer != null)
            {
                string resref = string.Empty;

                if (CharacterSelectionListBox.SelectedItem != null)
                {
                    resref = CharacterSelectionListBox.SelectedItem.UserData as string;
                }

                OnEnterServer(this, new ResrefEventArgs(resref));
            }
        }

        private void DisconnectButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (OnDisconnected != null)
            {
                OnDisconnected(this, new EventArgs());
            }
        }

        private void CreateButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (OnCreateCharacter != null)
            {
                OnCreateCharacter(this, new EventArgs());
            }
        }

        private void DeleteButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (CharacterSelectionListBox.SelectedItem != null)
            {
                MessageBox popUp = MessageBox.Show(new Point(300, 150), "Really Delete?", "Are you sure you want to delete this character?", MessageBoxButtonTypeEnum.YesNo, this._desktop);
                popUp.OnYesClicked += ConfirmDelete;
            }
        }

        private void ConfirmDelete(object sender, EventArgs e)
        {
            if (OnDeleteCharacter != null)
            {
                ResrefEventArgs args = new ResrefEventArgs(CharacterSelectionListBox.SelectedItem.UserData as string);
                OnDeleteCharacter(this, args);
            }
        }

        public void PerformCharacterDelete()
        {
            CharacterSelectionListBox.Items.Remove(CharacterSelectionListBox.SelectedItem);
        }
    }
}
