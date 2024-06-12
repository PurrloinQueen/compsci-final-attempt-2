using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace compsci_final_attempt_2
{

    enum Screen
    {
        Opening,
        Main,
        Pause,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Screen _screen;

        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState, prevKeyboardState;

        Texture2D mole, opening, yard, pause;
        Rectangle window, windowBounds, background;
        SpriteFont gameFont;

        SoundEffect bonk;
        SoundEffectInstance bonkInstance;

        List<Texture2D> pauseButton, startButton, quitButton;

        List<Rectangle> moleSpawns, buttons;

        List<Vector2> textBoxes;

        int score, antiPoofX, antiPoofY, pauseClick, startClick, quitClick;
        bool moleHit, pauseButtonPressed;
        string titleText, pauseText, scoreText;
        Random Generator = new Random();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 800, 500);

            windowBounds = window;

            score = 0;
            antiPoofX = 801;
            antiPoofY = 501;
            pauseClick = 0;
            startClick = 0;
            quitClick = 0;

            _screen = Screen.Opening;

            moleHit = false;
            pauseButtonPressed  = false;

            titleText = "Whack-A-Mole";
            pauseText = "GAME PAUSED";
            // scoreText = Convert.ToString(int score($"0000"));

            pauseButton = new List<Texture2D>();

            startButton = new List<Texture2D>();

            quitButton = new List<Texture2D>();

            base.Initialize();


            moleSpawns = new List<Rectangle>();
            moleSpawns.Add(new Rectangle(0, 0, 100, 100));
            moleSpawns.Add(new Rectangle(100, 100, 100, 100));
            moleSpawns.Add(new Rectangle(300, 100, 100, 100));
            moleSpawns.Add(new Rectangle(400, 100, 100, 100));

            buttons = new List<Rectangle>();
            buttons.Add(new Rectangle(400, 150, 100, 200));
            buttons.Add(new Rectangle(400, 300, 100, 200));

            textBoxes = new List<Vector2>();
            textBoxes.Add(new Vector2(250, 250));
            textBoxes.Add(new Vector2(0, 0));
            textBoxes.Add(new Vector2(350, 0));

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            mole = Content.Load<Texture2D>("mole");

            bonk = Content.Load<SoundEffect>("bonk");

            gameFont = Content.Load<SpriteFont>("gameFont");

            pauseButton.Add(Content.Load<Texture2D>("Paused_Unpressed"));
            pauseButton.Add(Content.Load<Texture2D>("Paused_Pressed"));

            startButton.Add(Content.Load<Texture2D>("Play_Unpressed"));
            startButton.Add(Content.Load<Texture2D>("Play_Pressed"));

            quitButton.Add(Content.Load<Texture2D>("Cross_Unpressed"));
            quitButton.Add(Content.Load<Texture2D>("Cross_Pressed"));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            prevKeyboardState = keyboardState;
            prevMouseState = mouseState;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_screen == Screen.Opening)
            {
                if (buttons[0].Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        pauseClick = 1;
                        startClick = 1;
                    }
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        pauseClick = 0;
                        startClick = 0;
                    }
                    
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (_screen == Screen.Opening)
            {
                _spriteBatch.DrawString(gameFont, titleText, textBoxes[0], Color.White);
                _spriteBatch.Draw(pauseButton[pauseClick], buttons[1], Color.White);
                _spriteBatch.Draw(startButton[startClick], buttons[0], Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}