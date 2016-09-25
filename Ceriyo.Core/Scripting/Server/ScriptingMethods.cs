using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Scripting.Server.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    public class ScriptingMethods: IScriptingMethods
    {

        public string GetScriptName(Entity entity)
        {
            try
            {
                return entity.GetComponent<Script>().Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
