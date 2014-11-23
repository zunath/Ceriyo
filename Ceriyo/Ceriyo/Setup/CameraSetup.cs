using FlatRedBall;
using Microsoft.Xna.Framework;

namespace Ceriyo.Setup
{
    internal static class CameraSetup
    {
        internal static void SetupCamera(Camera cameraToSetUp, GraphicsDeviceManager graphicsDeviceManager)
        {
            cameraToSetUp.UsePixelCoordinates();
        }
    }
}
