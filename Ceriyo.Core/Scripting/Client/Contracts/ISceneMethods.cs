using Squid;

namespace Ceriyo.Core.Scripting.Client.Contracts
{
    public interface ISceneMethods
    {
        Desktop CreateScene();
        void AddControlToScene(Desktop scene, Control control);
        void RemoveControlFromScene(Desktop scene, Control control);
        void ChangeScene(Desktop scene);
    }
}
