﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using SiliconStudio.Core.Mathematics;

namespace SiliconStudio.Xenko.Particles
{
    public static class ParticleFields
    {
        /// <summary>
        /// Particle position in 3D space
        /// </summary>
        public static readonly ParticleFieldDescription<Vector3> Position       = new ParticleFieldDescription<Vector3>("Position", new Vector3(0, 0, 0));

        /// <summary>
        /// Particle position from the last frame in 3D space, updated every frame if the particle has a Position field
        /// </summary>
        public static readonly ParticleFieldDescription<Vector3> OldPosition    = new ParticleFieldDescription<Vector3>("OldPosition", new Vector3(0, 0, 0));

        /// <summary>
        /// Particle direction, or offset, in 3D space, calculated from the particle's position
        /// </summary>
        public static readonly ParticleFieldDescription<Vector3> Direction      = new ParticleFieldDescription<Vector3>("Direction", new Vector3(0, 0, 0));

        /// <summary>
        /// Quaternion rotation, for particles which have rotation in 3D
        /// </summary>
        public static readonly ParticleFieldDescription<Quaternion> Quaternion  = new ParticleFieldDescription<Quaternion>("Quaternion", new Quaternion(0f, 0f, 0f, 1f));
        public static readonly ParticleFieldDescription<Quaternion> Rotation3D  = Quaternion;

        /// <summary>
        /// Angular rotation, in RADIANS, for particles which only have 1 axis of rotation
        /// </summary>
        public static readonly ParticleFieldDescription<float> Rotation         = new ParticleFieldDescription<float>("Rotation", 1);
        public static readonly ParticleFieldDescription<float> Rotation1D       = Rotation;
        public static readonly ParticleFieldDescription<float> Angle            = Rotation;

        /// <summary>
        /// Particle velocity in 3D space
        /// </summary>
        public static readonly ParticleFieldDescription<Vector3> Velocity       = new ParticleFieldDescription<Vector3>("Velocity", new Vector3(0, 0, 0));
        public static readonly ParticleFieldDescription<Vector3> Speed          = Velocity;

        /// <summary>
        /// Particle uniform size. If particles are rendered as a 2D quads or 3D meshes, these extra dimensions can be set on the material side.
        /// </summary>
        public static readonly ParticleFieldDescription<float> Size             = new ParticleFieldDescription<float>("Size", 1);
        public static readonly ParticleFieldDescription<float> Scale            = Size;
        
        /// <summary>
        /// Random seed, for generating fast random values in runtime.
        /// </summary>
        public static readonly ParticleFieldDescription<RandomSeed> RandomSeed  = new ParticleFieldDescription<RandomSeed>("RandomSeed", new RandomSeed(0));

        /// <summary>
        /// Particle remaining lifetime. When it reaches 0, the particle dies.
        /// Remaining life is easier to work with because it is an absolute value. Total life needs to know what the maximum life is.
        /// </summary>
        public static readonly ParticleFieldDescription<float> RemainingLife    = new ParticleFieldDescription<float>("RemainingLife", 1);
        public static readonly ParticleFieldDescription<float> Life             = RemainingLife;

        /// <summary>
        /// Particle color, in RGBA.
        /// </summary>
        public static readonly ParticleFieldDescription<Color4> Rgba            = new ParticleFieldDescription<Color4>("RGBA", new Color4(1,1,1,1));
        public static readonly ParticleFieldDescription<Color4> Color           = Rgba;
        public static readonly ParticleFieldDescription<Color4> Color4          = Rgba;

    }
}