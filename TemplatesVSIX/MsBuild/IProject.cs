﻿using System.Collections.Generic;

namespace TemplatesVSIX.MsBuild
{
    internal interface IProject
    {
        IEnumerable<Error> Errors { get; }
        IEnumerable<ItemGroup> ItemGroups { get; }
        IEnumerable<IReference> References { get; }
        string TargetVersion { get; set; }

        void AddProperty(string name, string value);
    }
}