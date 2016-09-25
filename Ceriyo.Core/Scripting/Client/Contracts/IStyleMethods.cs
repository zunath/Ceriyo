using Ceriyo.Core.Constants;
using Squid;

namespace Ceriyo.Core.Scripting.Client.Contracts
{
    public interface IStyleMethods
    {
        Skin CreateSkin();
        ControlStyle CreateControlStyle();
        ControlStyle CreateControlStyle(ControlStyle style);
        void SetSkin(Skin skin);
        Margin CreateMargin(int all);
        Margin CreateMargin(int left, int top, int right, int bottom);
        int CreateColor(int red, int green, int blue, int alpha);
        void SetCursor(
            Skin skin,
            CursorType cursorType, 
            string textureFile, 
            int width, 
            int height, 
            int hotSpotX, 
            int hotSpotY);
    }
}
