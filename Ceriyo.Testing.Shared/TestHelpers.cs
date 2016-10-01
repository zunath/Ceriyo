using System;
using System.IO;
using System.Reflection;
using Artemis;

namespace Ceriyo.Testing.Shared
{
    public static class TestHelpers
    {
        public static EntityWorld CreateEntityWorld()
        {
            return new EntityWorld();
        }

        public static void SetUpEnvironmentDirectory()
        {
            var directory = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            if (directory == null)
            {
                throw new Exception("Directory cannot be null.");
            }

            Environment.CurrentDirectory = directory;
        }
    }
}
