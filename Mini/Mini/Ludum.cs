using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mini.Controls;
using ServiceModel;
using ServiceStack.ServiceClient.Web;
using Vector2 = Microsoft.Xna.Framework.Vector2;

#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace Mini
{
    using Mini.Screens;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Ludum : Game
    {
        private const string BaseUrl = "http://localhost:82/";
        private Texture2D cellMovingTexture;
        private Texture2D cellTexture;
        private SpriteFont font;
        private GraphicsDeviceManager graphics;

        private Vector2 mousePos;
        private Texture2D mouseTexture;
        private MoveCommand move;
        private SpriteBatch spriteBatch;
        private Texture2D textBoxTexture;
        private bool typeUsername;
        private string username = string.Empty;

        public Ludum()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

#if WINDOWS_PHONE
    // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
#endif
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            mousePos = Vector2.Zero;
            //txtUsername = new Rectangle(20, 20, 200, 20);
            //txtEmail = new Rectangle(20, 20, 200, 20);
            //txtPassword = new Rectangle(20, 20, 200, 20);
            TextInput.Init(Window.Handle);
            Components.Add(new LoginScreen(this));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //load the image
            mouseTexture = Content.Load<Texture2D>("Pointer");
            cellTexture = Content.Load<Texture2D>("Cell");
            textBoxTexture = Content.Load<Texture2D>("TextBox");
            font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // where is the mouse position now ???
            MouseState mouseState = Mouse.GetState();
            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y;

            // TODO: Add your update logic here
            if (mouseState.LeftButton == ButtonState.Pressed && IsActive)
            {
                //using (var jsonClient = new JsonServiceClient(BaseUrl))
                //{
                //    jsonClient.SetCredentials("morten", "pass");
                //    jsonClient.HttpMethod = "POST";

                //    move = new MoveCommand {Vector = new ServiceModel.Vector2 {X = mouseState.X, Y = mouseState.Y}};
                //    jsonClient.AlwaysSendBasicAuthHeader = true;
                //    jsonClient.SendOneWay(move);
                //}
                foreach (IGameComponent component in Components)
                {
                    if (component is TextBox)
                    {
                        var textBox = component as TextBox;
                        textBox.IsSelected = textBox.Element.Intersects(new Rectangle(mouseState.X, mouseState.Y, 1, 1));
                    }
                }
            }

            base.Update(gameTime);
            TextInput.Instance.clearBuffer();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            //spriteBatch.Begin();
            //var center = new Vector2
            //                 {
            //                     X = Window.ClientBounds.Width/2,
            //                     Y = Window.ClientBounds.Height/2
            //                 };
            //float offSetX = cellTexture.Width/2;
            //float offSetY = cellTexture.Height/2;
            //center.X -= offSetX;
            //center.Y -= offSetY;
            //spriteBatch.Draw(cellTexture, center, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None,
            //                 0.1f);
            //spriteBatch.End();

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(mouseTexture, mousePos, null, Color.Tomato, 0f, Vector2.Zero, 1.0f, SpriteEffects.None,
                             0.0f);
            spriteBatch.End();
        }
    }
}