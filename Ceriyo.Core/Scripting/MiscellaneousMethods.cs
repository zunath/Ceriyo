using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Scripting
{
    public class MiscellaneousMethods: IScriptMethodGroup
    {
        public static void Print(string message)
        {
            Console.WriteLine(message);
        }

        public static string GetScriptName(Entity entity)
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
