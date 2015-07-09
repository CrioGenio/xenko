using System;
using System.Threading.Tasks;

using NUnit.Framework;

using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Games;

namespace SiliconStudio.Paradox.Graphics.Tests
{
    [TestFixture]
    public class TestSpriteBatch3D : TestGameBase
    {
        private Sprite3DBatch batch;
        private Texture sphere;

        private const int SphereSpace = 4;
        private const int SphereWidth = 150;
        private const int SphereHeight = 150;
        private const int SphereCountPerRow = 6;
        private const int SphereTotalCount = 32;

        private float timeInSeconds;

        private SpriteSheet rotatedImages;

        private RasterizerState rasterizerState;

        public TestSpriteBatch3D()
        {
            CurrentVersion = 1;
            GraphicsDeviceManager.PreferredBackBufferWidth = 640;
            GraphicsDeviceManager.PreferredBackBufferHeight = 640;
        }

        protected override void RegisterTests()
        {
            base.RegisterTests();

            FrameGameSystem.Draw(() => SetTimeAndDrawScene(0)).TakeScreenshot();
            FrameGameSystem.Draw(() => SetTimeAndDrawScene(0.27f)).TakeScreenshot();
        }

        protected override async Task LoadContent()
        {
            await base.LoadContent();

            batch = new Sprite3DBatch(GraphicsDevice);
            sphere = Asset.Load<Texture>("Sphere");
            rotatedImages = Asset.Load<SpriteSheet>("RotatedImages");
            rasterizerState = RasterizerState.New(GraphicsDevice, new RasterizerStateDescription(CullMode.None));
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            timeInSeconds += 1 / 60f; // frame dependent for graphic unit testing.

            if (!ScreenShotAutomationEnabled)
                DrawScene();
        }

        private void SetTimeAndDrawScene(float time)
        {
            timeInSeconds = time;

            DrawScene();
        }

        private void Draw(Sprite sprite, Vector3 position, Vector3? rotationParam = null, Vector2? sizeParam = null, Color? colorParam = null)
        {
            var rotation = rotationParam.HasValue ? rotationParam.Value : Vector3.Zero;
            var worldMatrix = Matrix.RotationYawPitchRoll(rotation.X, rotation.Y, rotation.Z) * Matrix.Translation(position);
            var color = colorParam.HasValue ? colorParam.Value : Color.White;
            var size = sizeParam.HasValue ? sizeParam.Value : sprite.Size;

            batch.Draw(sprite.Texture, ref worldMatrix, ref sprite.RegionInternal, ref size, ref color, sprite.Orientation);
        }

        private void DrawScene()
        {
            var cameraSize = 640f;
            var cameraNear = 100;
            var projectionMatrix = Matrix.PerspectiveRH(cameraSize, cameraSize, cameraNear, 10000);
            var viewMatrix = Matrix.LookAtRH(cameraNear * Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            
            GraphicsDevice.Clear(GraphicsDevice.BackBuffer, Color.Black);
            GraphicsDevice.Clear(GraphicsDevice.DepthStencilBuffer, DepthStencilClearOptions.DepthBuffer);
            GraphicsDevice.SetDepthAndRenderTarget(GraphicsDevice.DepthStencilBuffer, GraphicsDevice.BackBuffer);

            batch.Begin(viewMatrix*projectionMatrix, rasterizerState: rasterizerState);

            var leftTopCorner = new Vector3(-320, -320, 0);
            var pos = leftTopCorner;
            var noRotation = rotatedImages["NoRotation"];
            var rotation90 = rotatedImages["Rotation90"];
            var width = noRotation.Region.Width;
            var height = noRotation.Region.Height;

            // not rotated image
            pos.X = leftTopCorner.X + width / 2;
            pos.Y = leftTopCorner.Y + height / 2;
            Draw(noRotation, pos);
            
            // rotated image
            pos.X = leftTopCorner.X + width / 2;
            pos.Y = leftTopCorner.Y + height + height / 2;
            Draw(rotation90, pos);

            // colored image
            pos.X = leftTopCorner.X + 3 * width / 2;
            pos.Y = leftTopCorner.Y + height / 2;
            Draw(noRotation, pos, null, colorParam: Color.Gray);

            // deformed image
            pos.X = leftTopCorner.X + width + height / 2;
            pos.Y = leftTopCorner.Y + 3 * height / 2;
            Draw(noRotation, pos, sizeParam: new Vector2(height, height));

            // image on background
            pos.X = leftTopCorner.X + width;
            pos.Y = leftTopCorner.Y + 3/2f * height;
            pos.Z = -20;
            Draw(noRotation, pos);
            pos.Z = 0;

            // rotating image (around Z)
            pos.X = leftTopCorner.X + 3 * width;
            pos.Y = leftTopCorner.Y + width;
            Draw(noRotation, pos, new Vector3(0, 0, timeInSeconds));

            // rotating image (around Y)
            pos.X = leftTopCorner.X + 3 * width;
            pos.Y = leftTopCorner.Y + 3 * width;
            pos.Z = -width/2;
            Draw(noRotation, pos, new Vector3(timeInSeconds, 0, 0));
            pos.Z = 0;

            // ball
            pos.Y = leftTopCorner.X + 4 * width;
            pos.X = leftTopCorner.Y + 1 * width;
            
            var time = timeInSeconds;
            var rotation = (float)(time * Math.PI * 2.0);
            var sourceRectangle = GetSphereAnimation(time);
            var world = Matrix.RotationYawPitchRoll(0, 0, rotation) * Matrix.Translation(pos);
            var size = new Vector2(SphereWidth, SphereHeight);
            var color = Color.White;
            batch.Draw(sphere, ref world, ref sourceRectangle, ref size, ref color);
            
            batch.End();
        }

        /// <summary>
        /// Calculates the rectangle region from the original Sphere bitmap.
        /// </summary>
        /// <param name="time">The current time</param>
        /// <returns>The region from the sphere texture to display</returns>
        private RectangleF GetSphereAnimation(float time)
        {
            var sphereIndex = MathUtil.Clamp((int)((time % 1.0f) * SphereTotalCount), 0, SphereTotalCount);

            int sphereX = sphereIndex % SphereCountPerRow;
            int sphereY = sphereIndex / SphereCountPerRow;
            return new RectangleF(sphereX * (SphereWidth + SphereSpace), sphereY * (SphereHeight + SphereSpace), SphereWidth, SphereHeight);
        }

        public static void Main()
        {
            using (var game = new TestSpriteBatch3D())
                game.Run();
        }

        /// <summary>
        /// Run the test
        /// </summary>
        [Test]
        public void RunTestSpriteBatch3D()
        {
            RunGameTest(new TestSpriteBatch3D());
        }
    }
}