﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHSDK.Adaptation
{
    internal interface IAdapterResolver
    {
        object Resolve(Type type);
    }
}
