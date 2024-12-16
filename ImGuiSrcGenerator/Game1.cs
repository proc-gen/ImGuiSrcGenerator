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
        TestClass TestClass = new TestClass();

        bool display = false;

        string toConvert =
@"<Container className=""TestContainer"" >
<Button name=""##MyFirstButton"" text=""Click Me!"" />
<Checkbox name=""##MyFirstCheckbox"" text=""Check Me!"" />
<RadioButton name=""##RadioGroup"" text=""Radio 1"" value=""0""/>
<RadioButton name=""##RadioGroup"" text=""Radio 2"" value=""1""/>
<RadioButton name=""##RadioGroup"" text=""Radio 3"" value=""2""/>
<RadioButton name=""##RadioGroup"" text=""Radio 4"" value=""3""/>
<InputText name=""##InputText"" maxLength=""100"" />
<InputText name=""##InputHint"" hint=""Hint Hint"" maxLength=""100"" />
<InputText name=""##InputMulti"" maxLength=""100"" multiline=""true"" width=""200"" height=""200""/>
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
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                display = false;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                display = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.End();

            base.Draw(gameTime);

            GuiRenderer.BeginLayout(gameTime);

            if (display)
            {
                TestClass.Render();
            }
            else
            {
                Main.Render();
            }
            GuiRenderer.EndLayout();
        }
    }
}
