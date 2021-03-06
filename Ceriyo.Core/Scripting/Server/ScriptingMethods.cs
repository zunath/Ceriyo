﻿using System;
using Artemis;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Scripting.Server.Contracts;

namespace Ceriyo.Core.Scripting.Server
{
    /// <inheritdoc />
    [ScriptNamespace("Scripting")]
    public class ScriptingMethods: IScriptingMethods
    {

        /// <inheritdoc />
        public string GetScriptName(Entity entity, ScriptEvent @event)
        {
            try
            {
                var scriptGroup = entity.GetComponent<ScriptGroup>();
                return scriptGroup[@event];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
