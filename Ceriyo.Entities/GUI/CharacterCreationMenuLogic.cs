using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.Packets;
using Ceriyo.Entities.DrawableBatches;
using Squid;
using System;

namespace Ceriyo.Entities.GUI
{
    public class CharacterCreationMenuLogic : GUIDrawableBatch
    {
        #region Controls
        private ListBox StepsListBox { get; set; }
        private ListBoxItem DetailsListBoxItem { get; set; }
        private ListBoxItem RaceListBoxItem { get; set; }
        private ListBoxItem PortraitListBoxItem { get; set; }
        private ListBoxItem ClassListBoxItem { get; set; }
        private ListBoxItem AbilitiesListBoxItem { get; set; }
        private ListBoxItem SkillsListBoxItem { get; set; }
        private Button PlayButton { get; set; }
        private Button ResetButton { get; set; }
        private Button CancelButton { get; set; }

        private Panel DetailsPanel { get; set; }
        private TextBox NameTextBox { get; set; }
        private TextArea DescriptionTextArea { get; set; }

        private Panel RacePanel { get; set; }
        private ListBox RaceListBox { get; set; }

        private Panel PortraitPanel { get; set; }
        private Panel ClassPanel { get; set; }
        private ListBox ClassListBox { get; set; }

        private Panel AbilitiesPanel { get; set; }
        private ListBox AbilitiesListBox { get; set; }

        private Panel SkillsPanel { get; set; }
        private ListBox SkillsListBox { get; set; }

        #endregion

        #region Events

        public event EventHandler<GameObjectEventArgs> OnPlayButtonClicked;
        public event EventHandler<EventArgs> OnCancelButtonClicked;

        #endregion

        public CharacterCreationMenuLogic()
            : base("CharacterCreationMenu")
        {
            LoadControls();
            HookEvents();
            InitializeUI();
        }

        private void LoadControls()
        {
            StepsListBox = GetControl("lbSteps") as ListBox;
            DetailsListBoxItem = GetControl("lbiDetails") as ListBoxItem;
            RaceListBoxItem = GetControl("lbiRace") as ListBoxItem;
            PortraitListBoxItem = GetControl("lbiPortrait") as ListBoxItem;
            ClassListBoxItem = GetControl("lbiClass") as ListBoxItem;
            AbilitiesListBoxItem = GetControl("lbiAbilities") as ListBoxItem;
            SkillsListBoxItem = GetControl("lbiSkills") as ListBoxItem;
            PlayButton = GetControl("btnPlay") as Button;
            ResetButton = GetControl("btnReset") as Button;
            CancelButton = GetControl("btnCancel") as Button;

            DetailsPanel = GetControl("pnlDetails") as Panel;
            NameTextBox = GetControl("txtName") as TextBox;
            DescriptionTextArea = GetControl("txtDescription") as TextArea;

            RacePanel = GetControl("pnlRace") as Panel;
            RaceListBox = GetControl("lbRaces") as ListBox;

            PortraitPanel = GetControl("pnlPortrait") as Panel;
            ClassPanel = GetControl("pnlClass") as Panel;
            ClassListBox = GetControl("lbCharacterClasses") as ListBox;

            AbilitiesPanel = GetControl("pnlAbilities") as Panel;
            AbilitiesListBox = GetControl("lbAbilities") as ListBox;

            SkillsPanel = GetControl("pnlSkills") as Panel;
            SkillsListBox = GetControl("lbSkills") as ListBox;
        }

        private void HookEvents()
        {
            StepsListBox.SelectedItemChanged += StepsListBox_SelectedItemChanged;
            PlayButton.MouseClick += PlayButton_MouseClick;
            CancelButton.MouseClick += CancelButton_MouseClick;
        }

        private void PlayButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (OnPlayButtonClicked != null)
            {
                Player pc = new Player
                {
                    Description = DescriptionTextArea.Text,
                    Name = NameTextBox.Text
                };

                OnPlayButtonClicked(this, new GameObjectEventArgs(pc));
            }
        }

        private void CancelButton_MouseClick(Control sender, MouseEventArgs args)
        {
            if (OnCancelButtonClicked != null)
            {
                OnCancelButtonClicked(this, new EventArgs());
            }
        }

        private void StepsListBox_SelectedItemChanged(Control sender, ListBoxItem value)
        {
            DetailsPanel.Visible = false;
            RacePanel.Visible = false;
            PortraitPanel.Visible = false;
            ClassPanel.Visible = false;
            AbilitiesPanel.Visible = false;
            SkillsPanel.Visible = false;

            if (value == DetailsListBoxItem) DetailsPanel.Visible = true;
            else if (value == RaceListBoxItem) RacePanel.Visible = true;
            else if (value == PortraitListBoxItem) PortraitPanel.Visible = true;
            else if (value == ClassListBoxItem) ClassPanel.Visible = true;
            else if (value == AbilitiesListBoxItem) AbilitiesPanel.Visible = true;
            else if (value == SkillsListBoxItem) SkillsPanel.Visible = true;
            

        }

        private void InitializeUI()
        {
            StepsListBox.SelectedItem = StepsListBox.Items[0];
        }

        public void LoadServerData(CharacterCreationScreenPacket packet)
        {
            foreach (Ability ability in packet.Abilities)
            {
                AbilitiesListBox.Items.Add(new ListBoxItem
                {
                    Text = ability.Name,
                    Size = new Point(50, 50)
                });
            }

            foreach (CharacterClass characterClass in packet.CharacterClasses)
            {
                ClassListBox.Items.Add(new ListBoxItem
                {
                    Text = characterClass.Name,
                    Size = new Point(50, 50)
                });
            }

            foreach (Race race in packet.Races)
            {
                RaceListBox.Items.Add(new ListBoxItem
                {
                    Text = race.Name,
                    Size = new Point(50, 50)
                });
            }

            foreach (Skill skill in packet.Skills)
            {
                SkillsListBox.Items.Add(new ListBoxItem
                {
                    Text = skill.Name,
                    Size = new Point(50, 50)
                });
            }
        }

    }
}
