﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.Enumerations
{
    public enum FileOperationResultTypeEnum
    {
        Unknown = 0,
        Success = 1,
        FileExists = 2,
        FileDoesNotExist = 3,
        Failure = 4
    }
}
