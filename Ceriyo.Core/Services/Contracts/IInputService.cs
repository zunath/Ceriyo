using Microsoft.Xna.Framework;

namespace Ceriyo.Core.Services.Contracts
{
    public interface IInputService
    {
        void Update();
        bool IsLeftMouseDown();
        bool IsLeftMouseUp();
        bool IsRightMouseDown();
        bool IsRightMouseUp();
        bool IsLeftMousePressed();
        bool IsRightMousePressed();

        Vector2 GetMousePosition();
    }
}
