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
    public sealed class Dictionary : ResourceDictionary {
        
        private static Dictionary singleton = new Dictionary();
        
        public Dictionary() {
            this.InitializeResources();
        }
        
        public static Dictionary Instance {
            get {
                return singleton;
            }
        }
        
        private void InitializeResources() {
            // Resource - [DataTemplateKey(Ceriyo.Infrastructure.Network.TransferObjects.PCTransferObject)] DataTemplate
            Func<UIElement, UIElement> r_0_dtFunc = r_0_dtMethod;
            this.Add(typeof(Ceriyo.Infrastructure.Network.TransferObjects.PCTransferObject), new DataTemplate(typeof(Ceriyo.Infrastructure.Network.TransferObjects.PCTransferObject), r_0_dtFunc));
            GeneratedPropertyInfo.RegisterGeneratedProperty(typeof(Ceriyo.Infrastructure.Network.TransferObjects.PCTransferObject), "Name", typeof(EmptyKeys.UserInterface.Generated.Dictionary_Bindings.PCTransferObject_Name_PropertyInfo));
        }
        
        private static UIElement r_0_dtMethod(UIElement parent) {
            // e_0 element
            TextBlock e_0 = new TextBlock();
            e_0.Parent = parent;
            e_0.Name = "e_0";
            e_0.Margin = new Thickness(5F, 5F, 5F, 5F);
            Binding binding_e_0_Text = new Binding("Name");
            binding_e_0_Text.UseGeneratedBindings = true;
            e_0.SetBinding(TextBlock.TextProperty, binding_e_0_Text);
            return e_0;
        }
    }
}
