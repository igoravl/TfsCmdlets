﻿using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators.Generators.Models
{
    // [Generator]
    public class Generator : BaseGenerator<Filter, TypeProcessor>
    {
        protected override string GeneratorName => "ModelGenerator";
    }
}