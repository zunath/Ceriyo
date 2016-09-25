using Ceriyo.Core.Scripting.Client.Contracts;
using Ceriyo.Core.Services.Contracts;
using Squid;

namespace Ceriyo.Core.Scripting.Client
{
    public class SceneMethods: ISceneMethods
    {
        private readonly IUIService _uiService;
        public SceneMethods(IUIService uiService)
        {
            _uiService = uiService;
        }
        public Desktop CreateScene()
        {
            return new Desktop();
        }
        public void AddControlToScene(Desktop scene, Control control)
        {
            control.Parent = scene;
        }

        public void RemoveControlFromScene(Desktop scene, Control control)
        {
            scene.Controls.Remove(control);
        }
        public void ChangeScene(Desktop scene)
        {
            _uiService.ChangeDesktop(scene);
        }
    }
}
