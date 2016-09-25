function Main() {
    var skin = Style.CreateSkin();

    var baseStyle = Style.CreateControlStyle();

    baseStyle.Tiling = TextureMode.Grid;
    baseStyle.Grid = Style.CreateMargin(3);
    baseStyle.Texture = "Graphics/UI/button_hot.dds";
    baseStyle.Default.Texture = "Graphics/UI/button_default.dds";
    baseStyle.Pressed.Texture = "Graphics/UI/button_down.dds";
    baseStyle.SelectedPressed.Texture = "Graphics/UI/button_down.dds";
    baseStyle.Focused.Texture = "Graphics/UI/button_down.dds";
    baseStyle.SelectedFocused.Texture = "Graphics/UI/button_down.dds";
    baseStyle.Selected.Texture = "Graphics/UI/button_down.dds";
    baseStyle.SelectedHot.Texture = "Graphics/UI/button_down.dds";


    
    var itemStyle = Style.CreateControlStyle(baseStyle);
    itemStyle.TextPadding = Style.CreateMargin(8, 0, 8, 0);
    itemStyle.TextAlign = Alignment.MiddleLeft;

    var buttonStyle = Style.CreateControlStyle(baseStyle);
    buttonStyle.TextPadding = Style.CreateMargin(0);
    buttonStyle.TextAlign = Alignment.MiddleCenter;

    var tooltipStyle = Style.CreateControlStyle(buttonStyle);
    tooltipStyle.TextPadding = Style.CreateMargin(8);
    tooltipStyle.TextAlign = Alignment.TopLeft;

    var inputStyle = Style.CreateControlStyle();
    inputStyle.Texture = "Graphics/UI/input_default.dds";
    inputStyle.Hot.Texture = "Graphics/UI/input_focused.dds";
    inputStyle.Focused.Texture = "Graphics/UI/input_focused.dds";
    inputStyle.TextPadding = Style.CreateMargin(8);
    inputStyle.Tiling = TextureMode.Grid;
    inputStyle.Focused.Tint = Style.CreateColor(255, 0, 0, 255);
    inputStyle.Grid = Style.CreateMargin(3);

    var windowStyle = Style.CreateControlStyle();
    windowStyle.Tiling = TextureMode.Grid;
    windowStyle.Grid = Style.CreateMargin(9);
    windowStyle.Texture = "Graphics/UI/window.dds";

    var frameStyle = Style.CreateControlStyle();
    frameStyle.Tiling = TextureMode.Grid;
    frameStyle.Grid = Style.CreateMargin(4);
    frameStyle.Texture = "Graphics/UI/frame.dds";
    frameStyle.TextPadding = Style.CreateMargin(8);

    var vscrollTrackStyle = Style.CreateControlStyle();
    vscrollTrackStyle.Tiling = TextureMode.Grid;
    vscrollTrackStyle.Grid = Style.CreateMargin(3);
    vscrollTrackStyle.Texture = "Graphics/UI/vscroll_track.dds";

    var vscrollButtonStyle = Style.CreateControlStyle();
    vscrollButtonStyle.Tiling = TextureMode.Grid;
    vscrollButtonStyle.Grid = Style.CreateMargin(3);
    vscrollButtonStyle.Texture = "Graphics/UI/vscroll_button.dds";
    vscrollButtonStyle.Hot.Texture = "Graphics/UI/vscroll_button_hot.dds";
    vscrollButtonStyle.Pressed.Texture = "Graphics/UI/vscroll_button_down.dds";

    var vscrollUp = Style.CreateControlStyle();
    vscrollUp.Default.Texture = "Graphics/UI/vscrollUp_default.dds";
    vscrollUp.Hot.Texture = "Graphics/UI/vscrollUp_hot.dds";
    vscrollUp.Pressed.Texture = "Graphics/UI/vscrollUp_down.dds";
    vscrollUp.Focused.Texture = "Graphics/UI/vscrollUp_hot.dds";

    var hscrollTrackStyle = Style.CreateControlStyle();
    hscrollTrackStyle.Tiling = TextureMode.Grid;
    hscrollTrackStyle.Grid = Style.CreateMargin(3);
    hscrollTrackStyle.Texture = "Graphics/UI/hscroll_track.dds";

    var hscrollButtonStyle = Style.CreateControlStyle();
    hscrollButtonStyle.Tiling = TextureMode.Grid;
    hscrollButtonStyle.Grid = Style.CreateMargin(3);
    hscrollButtonStyle.Texture = "Graphics/UI/hscroll_button.dds";
    hscrollButtonStyle.Hot.Texture = "Graphics/UI/hscroll_button_hot.dds";
    hscrollButtonStyle.Pressed.Texture = "Graphics/UI/hscroll_button_down.dds";

    var hscrollUp = Style.CreateControlStyle();
    hscrollUp.Default.Texture = "Graphics/UI/hscrollUp_default.dds";
    hscrollUp.Hot.Texture = "Graphics/UI/hscrollUp_hot.dds";
    hscrollUp.Pressed.Texture = "Graphics/UI/hscrollUp_down.dds";
    hscrollUp.Focused.Texture = "Graphics/UI/hscrollUp_hot.dds";

    var checkButtonStyle = Style.CreateControlStyle();
    checkButtonStyle.Default.Texture = "Graphics/UI/checkbox_default.dds";
    checkButtonStyle.Hot.Texture = "Graphics/UI/checkbox_hot.dds";
    checkButtonStyle.Pressed.Texture = "Graphics/UI/checkbox_down.dds";
    checkButtonStyle.Checked.Texture = "Graphics/UI/checkbox_checked.dds";
    checkButtonStyle.CheckedFocused.Texture = "Graphics/UI/checkbox_checked_hot.dds";
    checkButtonStyle.CheckedHot.Texture = "Graphics/UI/checkbox_checked_hot.dds";
    checkButtonStyle.CheckedPressed.Texture = "Graphics/UI/checkbox_down.dds";

    var comboLabelStyle = Style.CreateControlStyle();
    comboLabelStyle.TextPadding = Style.CreateMargin(10, 0, 0, 0);
    comboLabelStyle.Default.Texture = "Graphics/UI/combo_default.dds";
    comboLabelStyle.Hot.Texture = "Graphics/UI/combo_hot.dds";
    comboLabelStyle.Pressed.Texture = "Graphics/UI/combo_down.dds";
    comboLabelStyle.Focused.Texture = "Graphics/UI/combo_hot.dds";
    comboLabelStyle.Tiling = TextureMode.Grid;
    comboLabelStyle.Grid = Style.CreateMargin(3, 0, 0, 0);

    var comboButtonStyle = Style.CreateControlStyle();
    comboButtonStyle.Default.Texture = "Graphics/UI/combo_button_default.dds";
    comboButtonStyle.Hot.Texture = "Graphics/UI/combo_button_hot.dds";
    comboButtonStyle.Pressed.Texture = "Graphics/UI/combo_button_down.dds";
    comboButtonStyle.Focused.Texture = "Graphics/UI/combo_button_hot.dds";

    var multilineStyle = Style.CreateControlStyle();
    multilineStyle.TextAlign = Alignment.TopLeft;
    multilineStyle.TextPadding = Style.CreateMargin(8);

    var labelStyle = Style.CreateControlStyle();
    labelStyle.TextPadding = Style.CreateMargin(8, 0, 8, 0);
    labelStyle.TextAlign = Alignment.MiddleLeft;
    labelStyle.TextColor = Style.CreateColor(204, 204, 204, 1);
    labelStyle.BackColor = Style.CreateColor(255, 255, 255, 32);
    labelStyle.Default.BackColor = 0;

    skin.Styles.Add("item", itemStyle);
    skin.Styles.Add("textbox", inputStyle);
    skin.Styles.Add("button", buttonStyle);
    skin.Styles.Add("window", windowStyle);
    skin.Styles.Add("frame", frameStyle);
    skin.Styles.Add("checkBox", checkButtonStyle);
    skin.Styles.Add("comboLabel", comboLabelStyle);
    skin.Styles.Add("comboButton", comboButtonStyle);
    skin.Styles.Add("vscrollTrack", vscrollTrackStyle);
    skin.Styles.Add("vscrollButton", vscrollButtonStyle);
    skin.Styles.Add("vscrollUp", vscrollUp);
    skin.Styles.Add("hscrollTrack", hscrollTrackStyle);
    skin.Styles.Add("hscrollButton", hscrollButtonStyle);
    skin.Styles.Add("hscrollUp", hscrollUp);
    skin.Styles.Add("multiline", multilineStyle);
    skin.Styles.Add("tooltip", tooltipStyle);
    skin.Styles.Add("label", labelStyle);

    Style.SetSkin(skin);

    // Add cursors
    var cursorWidth = 32;
    var cursorHeight = 32;
    var halfWidth = 16;
    var halfHeight = 16;

    Style.SetCursor(skin, Cursor.Default,  "Graphics/Cursors/Arrow.png",    cursorWidth, cursorHeight, 0, 0);
    Style.SetCursor(skin, Cursor.Link,     "Graphics/Cursors/Link.png",     cursorWidth, cursorHeight, 0, 0);
    Style.SetCursor(skin, Cursor.Move,     "Graphics/Cursors/Move.png",     cursorWidth, cursorHeight, halfWidth, halfHeight);
    Style.SetCursor(skin, Cursor.Select,   "Graphics/Cursors/Select.png",   cursorWidth, cursorHeight, halfWidth, halfHeight);
    Style.SetCursor(skin, Cursor.SizeNS,   "Graphics/Cursors/SizeNS.png",   cursorWidth, cursorHeight, halfWidth, halfHeight);
    Style.SetCursor(skin, Cursor.SizeWE,   "Graphics/Cursors/SizeWE.png",   cursorWidth, cursorHeight, halfWidth, halfHeight);
    Style.SetCursor(skin, Cursor.HSplit,   "Graphics/Cursors/SizeNS.png",   cursorWidth, cursorHeight, halfWidth, halfHeight);
    Style.SetCursor(skin, Cursor.VSplit,   "Graphics/Cursors/SizeWE.png",   cursorWidth, cursorHeight, halfWidth, halfHeight);
    Style.SetCursor(skin, Cursor.SizeNESW, "Graphics/Cursors/SizeNESW.png", cursorWidth, cursorHeight, halfWidth, halfHeight);
    Style.SetCursor(skin, Cursor.SizeNWSE, "Graphics/Cursors/SizeNWSE.png", cursorWidth, cursorHeight, halfWidth, halfHeight);

}