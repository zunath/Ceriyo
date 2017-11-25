using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace Ceriyo.Toolset.WPF.Views.ScriptEditorView
{
    /// <summary>
    /// Interaction logic for ScriptEditor
    /// </summary>
    public partial class ScriptEditor : UserControl
    {
        public ScriptEditor()
        {
            InitializeComponent();
        }


        private void ScriptEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            Uri uri = new Uri("/AvalonLanguageFiles/lua.xshd", UriKind.Relative);
            StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
            if (resourceInfo == null)
                throw new Exception("Could not load lua language file for script editor.");

            using (XmlTextReader reader = new XmlTextReader(resourceInfo.Stream))
            {
                var xshd = HighlightingLoader.LoadXshd(reader);
                Editor.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
            }

        }
    }
}
