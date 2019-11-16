﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class Rule
    {
        public int Department { get; }
        [NotNull] public string Post { get; }
        [NotNull] public Dictionary<Platform, Role> PlatformAccesses { get; }
        [NotNull] public Dictionary<Platform, List<ProductRole>> ProductAccesses { get; }

        public Rule(int department, [NotNull] string post,
            [NotNull] Dictionary<Platform, List<ProductRole>> productAccesses,
            [NotNull] Dictionary<Platform, Role> platformAccesses)
        {
            Department = department;
            Post = post;
            ProductAccesses = productAccesses;
            PlatformAccesses = platformAccesses;
        }
    }
}
