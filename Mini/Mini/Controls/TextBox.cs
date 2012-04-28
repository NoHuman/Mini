namespace Mini.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class TextBox : DrawableGameComponent
    {
        public Game game;
        public SpriteFont font { get; set; }
        public Texture2D texture { get; set; }
        public Color ActiveColor { get; set; }
        public Color DeactiveColor { get; set; }
        public Rectangle Element { get; set; }
        public string Text { get; set; }
        public string Watermark { get; set; }
        public string PasswordLetter { get; set; }
        public bool IsSelected { get; set; }
        public int MaxCharacters { get; set; }
        protected SpriteBatch spriteBatch { get; set; }

        public Pulse<string> Pulse { get; set; }

        public TextBox(Game game) : base(game)
        {
            this.game = game;
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
                if (Text.Length > MaxCharacters)
                {
                    Text = Text.Substring(0, MaxCharacters);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            var value = string.Format("{0}", !string.IsNullOrEmpty(Text)
                                                 ? (PasswordLetter == null
                                                        ? Text
                                                        : RepeatLetter(Text.Length))
                                                 : (IsSelected ? "" : Watermark));
            value += IsSelected
                         ? Pulse.Next((int) gameTime.TotalGameTime.TotalMilliseconds)
                         : "";
            var color = IsSelected ? ActiveColor : DeactiveColor;
            spriteBatch.Draw(texture, Element, color);
            spriteBatch.DrawString(font, value, new Vector2(Element.Left + 2, Element.Top), Color.White);
            spriteBatch.End();
        }

        private string RepeatLetter(int length)
        {
            var password = "";
            for (var i = 0; i < length; i++)
            {
                password += PasswordLetter;
            }
            return password;
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
    }
}