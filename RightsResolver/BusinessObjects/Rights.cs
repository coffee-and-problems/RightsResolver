﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class Rights
    {
        [NotNull] public Dictionary<Platform, Role> PlatformAccesses {get;}
        [NotNull] public Dictionary<Platform, Dictionary<string, Role>> ProductAccesses { get; }

        public Rights([NotNull] Dictionary<Platform, Role> platformAccesses,
            [NotNull] Dictionary<Platform, Dictionary<string, Role>> productAccesses)
        {
            PlatformAccesses = platformAccesses;
            ProductAccesses = productAccesses;
        }
    }
}