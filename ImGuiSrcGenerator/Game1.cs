using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.ImGuiNet;
using ImGuiNET;
using ImGuiSrcGenerator.Display;
using ImGuiSrcGenerator.Generators;

namespace ImGuiSrcGenerator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static ImGuiRenderer GuiRenderer;

        Main Main = new Main();

        string toConvert =
@"<Container className=""TestContainer"" >
<Button name=""MyFirstButton"" text=""Click Me!"" />
<Checkbox name=""MyFirstCheckbox"" text=""Check Me!"" />
</Container>
";

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            GuiRenderer = new ImGuiRenderer(this);
            Main.XmlCode = toConvert;
            var generator = new Generator();
            Main.ConvertedCode = generator.ConvertFromString(toConvert);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GuiRenderer.RebuildFontAtlas();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.End();

            base.Draw(gameTime);

            GuiRenderer.BeginLayout(gameTime);

            Main.Render();

            GuiRenderer.EndLayout();
        }
    }
}
