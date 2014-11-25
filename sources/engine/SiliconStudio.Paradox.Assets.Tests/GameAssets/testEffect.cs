﻿// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Paradox Shader Mixin Code Generator.
// To generate it yourself, please install SiliconStudio.Paradox.VisualStudio.Package .vsix
// and re-save the associated .pdxfx.
// </auto-generated>

using System;
using SiliconStudio.Core;
using SiliconStudio.Paradox.Effects;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.Shaders;
using SiliconStudio.Core.Mathematics;
using Buffer = SiliconStudio.Paradox.Graphics.Buffer;


#line 3 "D:\Code\Paradox\sources\engine\SiliconStudio.Paradox.Assets.Tests\GameAssets\testEffect.pdxfx"
using SiliconStudio.Paradox.Effects.Data;

#line 5
namespace DefaultEffects
{

    #line 7
    internal static partial class ShaderMixins
    {
        internal partial class Default  : IShaderMixinBuilder
        {
            public void Generate(ShaderMixinSourceTree mixin, ShaderMixinContext context)
            {

                #line 13
                context.Mixin(mixin, "ShaderBase");

                #line 14
                context.Mixin(mixin, "TransformationWVP");

                #line 15
                context.Mixin(mixin, "BRDFDiffuseBase");

                #line 16
                context.Mixin(mixin, "BRDFSpecularBase");

                #line 17
                context.Mixin(mixin, "AlbedoFlatShading");

                #line 18
                context.Mixin(mixin, "TransparentShading");

                #line 20
                if (context.GetParam(MaterialParameters.AlbedoDiffuse) != null)

                    {

                        #line 21
                        var __subMixin = new ShaderMixinSourceTree() { Parent = mixin };

                        #line 21
                        context.Mixin(__subMixin, context.GetParam(MaterialParameters.AlbedoDiffuse));
                        mixin.Mixin.AddComposition("albedoDiffuse", __subMixin.Mixin);
                    }

                #line 23
                if (context.GetParam(MaterialParameters.AlbedoSpecular) != null)

                    {

                        #line 24
                        var __subMixin = new ShaderMixinSourceTree() { Parent = mixin };

                        #line 24
                        context.Mixin(__subMixin, context.GetParam(MaterialParameters.AlbedoSpecular));
                        mixin.Mixin.AddComposition("albedoSpecular", __subMixin.Mixin);
                    }
            }

            [ModuleInitializer]
            internal static void __Initialize__()

            {
                ShaderMixinManager.Register("Default", new Default());
            }
        }
    }
}
