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
        Rectangle window, windowBounds, background, moleSpawn1, moleSpawn2, moleSpawn3, pauseButtonRect, startButtonRect, quitButtonRect;
        SpriteFont gameFont;

        SoundEffect bonk, missed;
        SoundEffectInstance bonkInstance, missedInstance;

        List<Texture2D> pauseButton, startButton, quitButton;        

        List<Vector2> textBoxes;

        int score, antiPoofX, antiPoofY, pauseClick, startClick, quitClick;
        bool moleHit, pauseButtonPressed, startButtonPressed, quitButtonPressed;
        float timer;
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
            background = window;

            pauseButtonRect = new Rectangle(0, 0, 50, 100);
            startButtonRect = new Rectangle(350, 150, 100, 150);
            quitButtonRect = new Rectangle(350, 300, 100, 150);

            moleSpawn1 = new Rectangle(100, 100, 100, 100);
            moleSpawn2 = new Rectangle(250, 300, 100, 100);
            moleSpawn3 = new Rectangle(400, 50, 100, 100);

            score = 0;
            antiPoofX = 801 - 100;
            antiPoofY = 501 - 100;
            pauseClick = 0;
            startClick = 0;
            quitClick = 0;
            timer = 0;

            _screen = Screen.Opening;

            moleHit = false;
            pauseButtonPressed  = false;
            startButtonPressed = false;
            quitButtonPressed = false;

            titleText = "Whack-A-Mole";
            pauseText = "GAME PAUSED";
            scoreText = score.ToString("0000");

            pauseButton = new List<Texture2D>();

            startButton = new List<Texture2D>();

            quitButton = new List<Texture2D>();

            base.Initialize();

            textBoxes = new List<Vector2>();
            textBoxes.Add(new Vector2(250, 250));
            textBoxes.Add(new Vector2(0, 0));
            textBoxes.Add(new Vector2(350, 0));

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            mole = Content.Load<Texture2D>("mole");
            pause = Content.Load<Texture2D>("pauseMole");

            bonk = Content.Load<SoundEffect>("bonk");
            missed = Content.Load<SoundEffect>("honk");

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
            prevKeyboardState = keyboardState;
            prevMouseState = mouseState;


            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_screen == Screen.Opening)
            {
                if (startButtonRect.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        startClick = 1;
                        startButtonPressed = true;
                    }
                    else
                    {
                        startClick = 0;
                    }
                }
                if (startButtonPressed == true)
                {
                    _screen = Screen.Main;
                    startButtonPressed = false;
                }
                if (quitButtonRect.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        quitClick = 1;
                        quitButtonPressed = true;
                    }
                    else
                    {
                        quitClick = 0;
                    }
                }
                if (quitButtonPressed == true)
                {
                    Exit();
                }
            }
            else if (_screen == Screen.Main)
            {
                if (moleSpawn1.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        bonk.Play();
                        moleSpawn1.X = Generator.Next(0, antiPoofX);
                        moleSpawn1.Y = Generator.Next(0, antiPoofY);
                        timer = 0;
                        score = score + 1;
                    }
                    else if (timer == Generator.Next(10, 30))
                    {
                        missed.Play();
                        moleSpawn1.X = Generator.Next(0, antiPoofX);
                        moleSpawn1.Y = Generator.Next(0, antiPoofY);
                        timer = 0;
                    }
                }
                if (moleSpawn2.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        bonk.Play();
                        moleSpawn2.X = Generator.Next(0, antiPoofX);
                        moleSpawn2.Y = Generator.Next(0, antiPoofY);
                        timer = 0;
                        score = score + 1;
                    }
                    else if (timer == Generator.Next(10, 30))
                    {
                        missed.Play();
                        moleSpawn2.X = Generator.Next(0, antiPoofX);
                        moleSpawn2.Y = Generator.Next(0, antiPoofY);
                        timer = 0;
                    }
                }
                if (moleSpawn3.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        bonk.Play();
                        moleSpawn3.X = Generator.Next(0, antiPoofX);
                        moleSpawn3.Y = Generator.Next(0, antiPoofY);
                        timer = 0;
                        score = score + 1;
                    }
                    else if (timer == Generator.Next(10, 30))
                    {
                        missed.Play();
                        moleSpawn3.X = Generator.Next(0, antiPoofX);
                        moleSpawn3.Y = Generator.Next(0, antiPoofY);
                        timer = 0;
                    }
                }

                // Looks for user pausing the game
                if (pauseButtonRect.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        pauseClick = 1;
                        pauseButtonPressed = true;
                        _screen = Screen.Pause;

                    }
                    else
                    {
                        pauseClick = 0;
                    }
                }
               
            }
            else if (_screen == Screen.Pause)
            {
                if (startButtonRect.Contains(mouseState.X, mouseState.Y))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        startClick = 1;
                        startButtonPressed = true;
                    }
                    else
                    {
                        startClick = 0;
                    }
                }
                if (startButtonPressed == true)
                {
                    _screen = Screen.Main;
                    startButtonPressed = false;
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
                _spriteBatch.Draw(quitButton[quitClick], quitButtonRect, Color.White);
                _spriteBatch.Draw(startButton[startClick], startButtonRect, Color.White);
            }
            if (_screen == Screen.Main)
            {
                // _spriteBatch.DrawString(gameFont, scoreText, textBoxes[3], Color.White);
                _spriteBatch.Draw(pauseButton[pauseClick], pauseButtonRect, Color.White);
                _spriteBatch.Draw(mole, moleSpawn1, Color.White);
                _spriteBatch.Draw(mole, moleSpawn2, Color.White);
                _spriteBatch.Draw(mole, moleSpawn3, Color.White);
                _spriteBatch.DrawString(gameFont, scoreText, textBoxes[2], Color.White);
            }
            if (_screen == Screen.Pause)
            {
                _spriteBatch.Draw(pause, background, Color.White);
                _spriteBatch.DrawString(gameFont, pauseText, textBoxes[0], Color.White);
                _spriteBatch.Draw(startButton[startClick], startButtonRect, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}