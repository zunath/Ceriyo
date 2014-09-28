using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Entities.DrawableBatches;
using Squid;

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
        private Panel RacePanel { get; set; }
        private Panel PortraitPanel { get; set; }
        private Panel ClassPanel { get; set; }
        private Panel AbilitiesPanel { get; set; }
        private Panel SkillsPanel { get; set; }

        #endregion

        #region Events

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
            RacePanel = GetControl("pnlRace") as Panel;
            PortraitPanel = GetControl("pnlPortrait") as Panel;
            ClassPanel = GetControl("pnlClass") as Panel;
            AbilitiesPanel = GetControl("pnlAbilities") as Panel;
            SkillsPanel = GetControl("pnlSkills") as Panel;
        }

        private void HookEvents()
        {
            StepsListBox.SelectedItemChanged += StepsListBox_SelectedItemChanged;
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

            

        }

        private void InitializeUI()
        {
            StepsListBox.SelectedItem = StepsListBox.Items[0];
        }
    }
}
