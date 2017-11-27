// -----------------------------------------------------------
//  
//  This file was generated, please do not modify.
//  
// -----------------------------------------------------------
namespace EmptyKeys.UserInterface.Generated.Dictionary_Bindings {
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
    using EmptyKeys.UserInterface.Media.Animation;
    using EmptyKeys.UserInterface.Media.Imaging;
    using EmptyKeys.UserInterface.Shapes;
    using EmptyKeys.UserInterface.Renderers;
    using EmptyKeys.UserInterface.Themes;
    
    
    [GeneratedCodeAttribute("Empty Keys UI Generator", "3.0.0.0")]
    public class PCTransferObject_Name_PropertyInfo : IPropertyInfo {
        
        public System.Type PropertyType {
            get {
                return typeof(string);
            }
        }
        
        public bool IsResolved {
            get {
                return true;
            }
        }
        
        public object GetValue(object obj) {
            return ((Ceriyo.Infrastructure.Network.TransferObjects.PCTransferObject)(obj)).Name;
        }
        
        public object GetValue(object obj, object[] index) {
            return null;
        }
        
        public void SetValue(object obj, object value) {
        }
        
        public void SetValue(object obj, object value, object[] index) {
        }
    }
}
