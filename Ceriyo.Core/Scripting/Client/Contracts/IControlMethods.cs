using Squid;

namespace Ceriyo.Core.Scripting.Client.Contracts
{
    public interface IControlMethods
    {
        void AddToControl(Control parentControl, Control controlToAdd);
        Window CreateWindow(int width, int height, int x, int y, string header, bool isResizeable);
        Button CreateButton();
        CheckBox CreateCheckBox();
        DropDownList CreateComboBox();
        Dialog CreateDialog();
        FlowLayoutFrame CreateFlowLayoutFrame();
        Frame CreateFrame();
        ImageControl CreateImageControl();
        Label CreateLabel();
        ListBox CreateListBox();
        ListView CreateListView();
        Panel CreatePanel();
        RadioButton CreateRadioButton();
        Resizer CreateResizer();
        ScrollBar CreateScrollBar();
        Slider CreateSlider();
        SplitContainer CreateSplitContainer();
        TabControl CreateTabControl();
        TextArea CreateTextArea();
        TextBox CreateTextBox();
        TreeView CreateTreeView();
    }
}
