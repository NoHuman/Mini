namespace Mini.Screens
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Mini.Controls;

    public class LoginScreen : DrawableGameComponent
    {
        protected Game game { get; set; }

        protected List<DrawableGameComponent> Components { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        private SpriteFont font { get; set; }
        private Texture2D texture { get; set; }

        public LoginScreen(Game game) : base(game)
        {
            this.game = game;
            Components = new List<DrawableGameComponent>();
        }

        public override void Initialize()
        {
            var txtUsername = TxtUsername();
            txtUsername.Initialize();
            Components.Add(txtUsername);
            var txtPassword = TxtPassword();
            txtPassword.Initialize();
            Components.Add(txtPassword);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>("font");
            texture = game.Content.Load<Texture2D>("TextBox");
            foreach (var component in Components)
            {
                if (component is TextBox)
                {
                    var textBox = component as TextBox;
                    textBox.font = font;
                    textBox.texture = texture;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && Enabled)
            {
                foreach (var component in Components)
                {
                    if (component is TextBox)
                    {
                        var textBox = component as TextBox;
                        textBox.IsSelected = textBox.Element.Intersects(new Rectangle(mouseState.X, mouseState.Y, 1, 1));
                    }
                }
            }
            TabWasPressed();
            EnterWasPressed();
            foreach (var component in Components)
            {
                component.Update(gameTime);
            }
        }
        private void EnterWasPressed()
        {
            var enter = TextInput.Instance.Enter;
            if (enter)
            {
                
            }
        }

        private void TabWasPressed()
        {
            var tab = TextInput.Instance.Tab;
            if (tab)
            {
                bool selectNext = false;
                TextBox first = null;
                var hasSelection = false;
                foreach (var component in Components)
                {
                    if (component is TextBox)
                    {
                        var textBox = component as TextBox;
                        if (first == null)
                        {
                            first = textBox;
                        }
                        if (selectNext)
                        {
                            textBox.IsSelected = true;
                            selectNext = false;
                            break;
                        }
                        if (textBox.IsSelected)
                        {
                            hasSelection = true;
                            textBox.IsSelected = false;
                            selectNext = true;
                        }
                    }
                }
                if (first != null && (!hasSelection || selectNext))
                {
                    first.IsSelected = true;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                component.Draw(gameTime);
            }
        }

        private TextBox TxtUsername()
        {
            var txtUsername = new TextBox(game)
            {
                Element = new Rectangle(20, 20, 200, 20),
                IsSelected = false,
                Watermark = "username (a-z,0-9)",
                MaxCharacters = 16,
                ActiveColor = Color.DarkGreen,
                DeactiveColor = Color.DarkBlue,
                Pulse = new Pulse<string>(500)
            };
            txtUsername.Pulse.Add("_");
            txtUsername.Pulse.Add("");
            return txtUsername;
        }

        private TextBox TxtPassword()
        {
            var txtPassword = new TextBox(game)
                                  {
                                      Element = new Rectangle(20, 50, 200, 20),
                                      IsSelected = false,
                                      Watermark = "password (8-16 chars)",
                                      MaxCharacters = 16,
                                      ActiveColor = Color.DarkGreen,
                                      DeactiveColor = Color.DarkBlue,
                                      Pulse = new Pulse<string>(500),
                                      PasswordLetter = "*"
                                  };
            txtPassword.Pulse.Add("_");
            txtPassword.Pulse.Add("");
            return txtPassword;
        }
    }
}