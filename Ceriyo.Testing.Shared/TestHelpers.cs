using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Artemis;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
