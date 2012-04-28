namespace Mini.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Button : DrawableGameComponent
    {
        private Game _game { get; set; }

        public Rectangle Element { get; set; }
        public string Text { get; set; }
        public Color ActiveColor { get; set; }
        public Color DeactiveColor { get; set; }
        public SpriteFont font { get; set; }
        public Texture2D texture { get; set; }
        public bool IsSelected { get; set; }
        protected SpriteBatch spriteBatch { get; set; }

        public Button(Game game) : base(game)
        {
            _game = game;
        }
        public override void Update(GameTime gameTime)
        {
            if (IsSelected)
            {
                var textInput = TextInput.Instance;
                Text += textInput.Buffer;
                textInput.clearBuffer();
                if (textInput.BackSpace)
                {
                    if (Text.Length > 0)
                    {
                        Text = Text.Substring(0, Text.Length - 1);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            var color = IsSelected ? ActiveColor : DeactiveColor;
            spriteBatch.Draw(texture, Element, color);
            spriteBatch.DrawString(font, Text, new Vector2(Element.Left + 2+ 16-(Text.Length/2), Element.Top-10+Element.Height/2), Color.White);
            spriteBatch.End();
        }
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public void TriggerClick()
        {
            if (OnClick != null)
            {
                OnClick();
            }
        }
        public delegate void Click();

        public event Click OnClick;
    }
}