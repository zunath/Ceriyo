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
    public partial class CharacterCreationView : UIRoot {
        
        private Grid e_0;
        
        private Button e_1;
        
        private Button e_2;
        
        private Button e_3;
        
        private TextBlock e_4;
        
        private TextBox e_5;
        
        private TextBlock e_6;
        
        private TextBox e_7;
        
        public CharacterCreationView() : 
                base() {
            this.Initialize();
        }
        
        public CharacterCreationView(int width, int height) : 
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
            // e_0 element
            this.e_0 = new Grid();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            RowDefinition row_e_0_0 = new RowDefinition();
            row_e_0_0.Height = new GridLength(32F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_0);
            RowDefinition row_e_0_1 = new RowDefinition();
            row_e_0_1.Height = new GridLength(32F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_1);
            RowDefinition row_e_0_2 = new RowDefinition();
            row_e_0_2.Height = new GridLength(1F, GridUnitType.Star);
            this.e_0.RowDefinitions.Add(row_e_0_2);
            RowDefinition row_e_0_3 = new RowDefinition();
            row_e_0_3.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_3);
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
            this.e_1 = new Button();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            this.e_1.Margin = new Thickness(1F, 1F, 1F, 1F);
            this.e_1.Content = "Create Character";
            Grid.SetColumn(this.e_1, 0);
            Grid.SetRow(this.e_1, 3);
            Binding binding_e_1_IsEnabled = new Binding("IsModelValid");
            this.e_1.SetBinding(Button.IsEnabledProperty, binding_e_1_IsEnabled);
            Binding binding_e_1_Command = new Binding("CreateCharacterCommand");
            this.e_1.SetBinding(Button.CommandProperty, binding_e_1_Command);
            // e_2 element
            this.e_2 = new Button();
            this.e_0.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            this.e_2.Margin = new Thickness(1F, 1F, 1F, 1F);
            this.e_2.Content = "Back";
            Grid.SetColumn(this.e_2, 1);
            Grid.SetRow(this.e_2, 3);
            Binding binding_e_2_Command = new Binding("BackCommand");
            this.e_2.SetBinding(Button.CommandProperty, binding_e_2_Command);
            // e_3 element
            this.e_3 = new Button();
            this.e_0.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Margin = new Thickness(1F, 1F, 1F, 1F);
            this.e_3.Content = "Disconnect";
            Grid.SetColumn(this.e_3, 2);
            Grid.SetRow(this.e_3, 3);
            Binding binding_e_3_Command = new Binding("DisconnectCommand");
            this.e_3.SetBinding(Button.CommandProperty, binding_e_3_Command);
            // e_4 element
            this.e_4 = new TextBlock();
            this.e_0.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Text = "First Name: ";
            Grid.SetColumn(this.e_4, 0);
            Grid.SetRow(this.e_4, 0);
            // e_5 element
            this.e_5 = new TextBox();
            this.e_0.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Margin = new Thickness(4F, 4F, 4F, 4F);
            Grid.SetColumn(this.e_5, 1);
            Grid.SetRow(this.e_5, 0);
            Grid.SetColumnSpan(this.e_5, 2);
            Binding binding_e_5_Text = new Binding("FirstName");
            this.e_5.SetBinding(TextBox.TextProperty, binding_e_5_Text);
            // e_6 element
            this.e_6 = new TextBlock();
            this.e_0.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Text = "Last Name: ";
            Grid.SetColumn(this.e_6, 0);
            Grid.SetRow(this.e_6, 1);
            // e_7 element
            this.e_7 = new TextBox();
            this.e_0.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 4F, 4F, 4F);
            Grid.SetColumn(this.e_7, 1);
            Grid.SetRow(this.e_7, 1);
            Grid.SetColumnSpan(this.e_7, 2);
            Binding binding_e_7_Text = new Binding("LastName");
            this.e_7.SetBinding(TextBox.TextProperty, binding_e_7_Text);
        }
    }
}
