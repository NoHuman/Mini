using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mini.Controls
{
    public class TextBox : DrawableGameComponent
    {
        private Game _game;
        public SpriteFont font { get; set; }
        public SpriteBatch spriteBatch { get; set; }
        public Texture2D texture { get; set; }
        public Color ActiveColor { get; set; }
        public Color DeactiveColor { get; set; }
        public Rectangle Element { get; set; }
        public string Text { get; set; }
        public string Watermark { get; set; }
        public bool IsSelected { get; set; }
        public int MaxCharacters { get; set; }

        public Pulse Pulse { get; set; }

        public TextBox(Game game) : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("font");
            texture = Game.Content.Load<Texture2D>("TextBox");
        }

        public override void Update(GameTime gameTime)
        {
            if (IsSelected)
            {
                TextInput textInput = TextInput.Instance;
                Text += textInput.Buffer;
                textInput.clearBuffer();
                if (textInput.BackSpace)
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Substring(0, Text.Length - 1);
                    }
                }
                if(Text.Length > MaxCharacters)
                {
                    Text = Text.Substring(0, MaxCharacters);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            var value = string.Format("{0}", Text ?? Watermark);
            var color = IsSelected ? ActiveColor : DeactiveColor;
            spriteBatch.Draw(texture, Element, color);
            spriteBatch.DrawString(font, value, new Vector2(22, 20), Color.White);
            spriteBatch.End();
        }
    }
}