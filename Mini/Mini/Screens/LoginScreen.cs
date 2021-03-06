﻿namespace Mini.Screens
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Mini.Controls;

    public class LoginScreen : DrawableGameComponent
    {
        private TextBox _txtUsername;
        private TextBox _txtPassword;
        private Button _loginButton;
        protected Game game { get; set; }

        protected List<DrawableGameComponent> Components { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        private SpriteFont font { get; set; }
        private Texture2D textboxTexture { get; set; }
        private Texture2D buttonTexture { get; set; }

        public LoginScreen(Game game) : base(game)
        {
            this.game = game;
            Components = new List<DrawableGameComponent>();
        }

        public override void Initialize()
        {
            _txtUsername = TxtUsername();
            _txtUsername.Initialize();
            Components.Add(_txtUsername);
            _txtPassword = TxtPassword();
            _txtPassword.Initialize();
            Components.Add(_txtPassword);
            _loginButton = BtnLogin();
            _loginButton.Initialize();
            _loginButton.OnClick += ClickLogin;
            Components.Add(_loginButton);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>("font");
            textboxTexture = game.Content.Load<Texture2D>("TextBox");
            buttonTexture = game.Content.Load<Texture2D>("Button");
            foreach (var component in Components)
            {
                if (component is TextBox)
                {
                    var textBox = component as TextBox;
                    textBox.font = font;
                    textBox.texture = textboxTexture;
                }
                if (component is Button)
                {
                    var textBox = component as Button;
                    textBox.font = font;
                    textBox.texture = textboxTexture;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            foreach (var component in Components)
            {
                if (component is TextBox)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && Enabled)
                    {
                        var textBox = component as TextBox;
                        textBox.IsSelected = textBox.Element.Intersects(new Rectangle(mouseState.X, mouseState.Y, 1, 1));
                    }
                }
                if (component is Button)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && Enabled)
                    {
                        var button = component as Button;
                        if (button.Element.Intersects(new Rectangle(mouseState.X, mouseState.Y, 1, 1)))
                        {
                            button.TriggerClick();
                        }
                    }
                    else if (Enabled)
                    {
                        var button = component as Button;
                        button.IsSelected = button.Element.Intersects(new Rectangle(mouseState.X, mouseState.Y, 1, 1));
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
                TryLogin();
            }
        }

        public void ClickLogin()
        {
            TryLogin();
        }

        private bool TryLogin()
        {
            if (_txtPassword.Text == null)
            {
                return false;
            }
            if (_txtPassword.Text.Length < 8)
            {
                return false;
            }
            if (_txtUsername.Text == null)
            {
                return false;
            }
            if (AcceptedCredentials != null)
            {
                var args = new LoginEventArgs
                               {
                                   Username = _txtUsername.Text,
                                   Password = _txtPassword.Text
                               };
                AcceptedCredentials(args);
            }
            return true;
        }

        public delegate bool Success(LoginEventArgs args);

        public event Success AcceptedCredentials;

        private void TabWasPressed()
        {
            var tab = TextInput.Instance.Tab;
            if (tab)
            {
                var selectNext = false;
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

        private Button BtnLogin()
        {
            return new Button(game)
                       {
                           Element = new Rectangle(70, 80, 100, 30),
                           Text = "Login",
                           ActiveColor = Color.DarkGoldenrod,
                           DeactiveColor = Color.DarkCyan
                       };
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}