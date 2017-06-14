// -----------------------------------------------------------
//  
//  This file was generated, please do not modify.
//  
// -----------------------------------------------------------
namespace EmptyKeys.UserInterface.Generated {
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using EmptyKeys.UserInterface;
    using EmptyKeys.UserInterface.Charts;
    using EmptyKeys.UserInterface.Data;
    using EmptyKeys.UserInterface.Controls;
    using EmptyKeys.UserInterface.Controls.Primitives;
    using EmptyKeys.UserInterface.Input;
    using EmptyKeys.UserInterface.Interactions.Core;
    using EmptyKeys.UserInterface.Interactivity;
    using EmptyKeys.UserInterface.Media;
    using EmptyKeys.UserInterface.Media.Effects;
    using EmptyKeys.UserInterface.Media.Animation;
    using EmptyKeys.UserInterface.Media.Imaging;
    using EmptyKeys.UserInterface.Shapes;
    using EmptyKeys.UserInterface.Renderers;
    using EmptyKeys.UserInterface.Themes;
    
    
    [GeneratedCodeAttribute("Empty Keys UI Generator", "3.0.0.0")]
    public partial class CharacterSelectionView : UIRoot {
        
        private Grid e_0;
        
        private TextBox e_1;
        
        private Button e_2;
        
        private Button e_3;
        
        private ListBox e_4;
        
        private DockPanel e_5;
        
        private Grid e_6;
        
        private Button e_7;
        
        private Button e_8;
        
        public CharacterSelectionView() : 
                base() {
            this.Initialize();
        }
        
        public CharacterSelectionView(int width, int height) : 
                base(width, height) {
            this.Initialize();
        }
        
        private void Initialize() {
            Style style = RootStyle.CreateRootStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            InitializeElementResources(this);
            // e_0 element
            this.e_0 = new Grid();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            RowDefinition row_e_0_0 = new RowDefinition();
            row_e_0_0.Height = new GridLength(100F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_0);
            RowDefinition row_e_0_1 = new RowDefinition();
            row_e_0_1.Height = new GridLength(1F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_1);
            RowDefinition row_e_0_2 = new RowDefinition();
            row_e_0_2.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_2);
            ColumnDefinition col_e_0_0 = new ColumnDefinition();
            col_e_0_0.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_0);
            ColumnDefinition col_e_0_1 = new ColumnDefinition();
            col_e_0_1.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_1);
            ColumnDefinition col_e_0_2 = new ColumnDefinition();
            col_e_0_2.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_2);
            // e_1 element
            this.e_1 = new TextBox();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            this.e_1.IsReadOnly = true;
            this.e_1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Grid.SetColumn(this.e_1, 0);
            Grid.SetRow(this.e_1, 0);
            Grid.SetColumnSpan(this.e_1, 3);
            Binding binding_e_1_Text = new Binding("ServerInformationDetails");
            this.e_1.SetBinding(TextBox.TextProperty, binding_e_1_Text);
            // e_2 element
            this.e_2 = new Button();
            this.e_0.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            this.e_2.Margin = new Thickness(1F, 1F, 1F, 1F);
            this.e_2.Content = "Create Character";
            Grid.SetColumn(this.e_2, 1);
            Grid.SetRow(this.e_2, 2);
            Binding binding_e_2_Command = new Binding("CreateCharacterCommand");
            this.e_2.SetBinding(Button.CommandProperty, binding_e_2_Command);
            // e_3 element
            this.e_3 = new Button();
            this.e_0.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Margin = new Thickness(1F, 1F, 1F, 1F);
            this.e_3.Content = "Disconnect";
            Grid.SetColumn(this.e_3, 2);
            Grid.SetRow(this.e_3, 2);
            Binding binding_e_3_Command = new Binding("DisconnectCommand");
            this.e_3.SetBinding(Button.CommandProperty, binding_e_3_Command);
            // e_4 element
            this.e_4 = new ListBox();
            this.e_0.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.SelectionMode = SelectionMode.Single;
            Grid.SetColumn(this.e_4, 0);
            Grid.SetRow(this.e_4, 1);
            Grid.SetRowSpan(this.e_4, 2);
            Binding binding_e_4_ItemsSource = new Binding("PCs");
            this.e_4.SetBinding(ListBox.ItemsSourceProperty, binding_e_4_ItemsSource);
            Binding binding_e_4_SelectedItem = new Binding("SelectedPC");
            this.e_4.SetBinding(ListBox.SelectedItemProperty, binding_e_4_SelectedItem);
            // e_5 element
            this.e_5 = new DockPanel();
            this.e_0.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            Grid.SetColumn(this.e_5, 1);
            Grid.SetRow(this.e_5, 1);
            Grid.SetColumnSpan(this.e_5, 2);
            Binding binding_e_5_Visibility = new Binding("IsPCSelected");
            this.e_5.SetBinding(DockPanel.VisibilityProperty, binding_e_5_Visibility);
            // e_6 element
            this.e_6 = new Grid();
            this.e_5.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            RowDefinition row_e_6_0 = new RowDefinition();
            row_e_6_0.Height = new GridLength(1F, GridUnitType.Star);
            this.e_6.RowDefinitions.Add(row_e_6_0);
            RowDefinition row_e_6_1 = new RowDefinition();
            row_e_6_1.Height = new GridLength(1F, GridUnitType.Star);
            this.e_6.RowDefinitions.Add(row_e_6_1);
            RowDefinition row_e_6_2 = new RowDefinition();
            row_e_6_2.Height = new GridLength(1F, GridUnitType.Star);
            this.e_6.RowDefinitions.Add(row_e_6_2);
            RowDefinition row_e_6_3 = new RowDefinition();
            row_e_6_3.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_6.RowDefinitions.Add(row_e_6_3);
            ColumnDefinition col_e_6_0 = new ColumnDefinition();
            col_e_6_0.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_0);
            ColumnDefinition col_e_6_1 = new ColumnDefinition();
            col_e_6_1.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_1);
            ColumnDefinition col_e_6_2 = new ColumnDefinition();
            col_e_6_2.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_2);
            ColumnDefinition col_e_6_3 = new ColumnDefinition();
            col_e_6_3.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_3);
            // e_7 element
            this.e_7 = new Button();
            this.e_6.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(1F, 1F, 1F, 1F);
            this.e_7.Content = "Join Server";
            Grid.SetColumn(this.e_7, 0);
            Grid.SetRow(this.e_7, 3);
            Grid.SetColumnSpan(this.e_7, 2);
            Binding binding_e_7_Command = new Binding("JoinServerCommand");
            this.e_7.SetBinding(Button.CommandProperty, binding_e_7_Command);
            // e_8 element
            this.e_8 = new Button();
            this.e_6.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Margin = new Thickness(1F, 1F, 1F, 1F);
            this.e_8.Content = "Delete Character";
            Grid.SetColumn(this.e_8, 2);
            Grid.SetRow(this.e_8, 3);
            Grid.SetColumnSpan(this.e_8, 2);
            Binding binding_e_8_IsEnabled = new Binding("IsCharacterDeletionEnabled");
            this.e_8.SetBinding(Button.IsEnabledProperty, binding_e_8_IsEnabled);
            Binding binding_e_8_Command = new Binding("DeleteCharacterCommand");
            this.e_8.SetBinding(Button.CommandProperty, binding_e_8_Command);
        }
        
        private static void InitializeElementResources(UIElement elem) {
            elem.Resources.MergedDictionaries.Add(Dictionary.Instance);
        }
    }
}
