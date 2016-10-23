using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;


namespace EllixEngine
{
    class Ellix
    {
        Renderer gameRender;
        RenderWindow gameWindow;
        
        public void Init(String windowTitle,uint ResWidth,uint ResHeight) {
            gameRender = new Renderer();
            gameWindow = gameRender.init(windowTitle,ResWidth,ResHeight);
            //Input
            gameWindow.Closed += new EventHandler(OnClose);
            gameWindow.KeyPressed += new EventHandler<KeyEventArgs>(KeyPressed);
            gameWindow.Resized += new EventHandler<SizeEventArgs>(Resized);

        }
        //Input methods
        void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                gameWindow.Close();
            }
        }

        public keyInput getInput(Keyboard.Key key) {
            return new keyInput(key, gameWindow);
        }

        
        public void Resized(object sender,SizeEventArgs resize)
        {
            gameWindow.SetView(new View(((SFML.System.Vector2f) gameWindow.Size) / 2,(SFML.System.Vector2f) gameWindow.Size));
        }

        void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        public bool checkWindowOpen() {
            return gameWindow.IsOpen;
        }

        //GameEngine Methods
        public void renderFrame() {
            gameRender.frame(gameWindow);
        }

        public void getInput() {
            gameWindow.DispatchEvents();
        }

        public void registerObject(GameObject obj)
        {
            gameRender.registerObject(obj);
        }

        public void registerCamera(Camera cam)
        {
            gameRender.registerCamera(cam);
        }

        static void Main(string[] args)
        {
            Ellix gameEngine = new Ellix();
            gameEngine.Init("Powered By Ellix",960,540);

            GameObject obj1 = new GameObject();
            obj1.setImage("C:\\Assets\\Pokemon_Go.png");
            gameEngine.registerObject(obj1);
            obj1.scale = new SFML.System.Vector2f(1f, 1f);
            GameObject obj2 = new GameObject();
            obj2.setImage("C:\\Assets\\Pokemon_Go.png");
            obj2.position = new SFML.System.Vector2f(300, 0);
            gameEngine.registerObject(obj2);
           
            Player player = new Player();
            player.setImage("C:\\Assets\\HorsePlayer.png");
            player.setAnimation(400, 248, 3, 5);
            gameEngine.registerObject(player);

            PlayerCam camera = new PlayerCam(player);
            gameEngine.registerCamera(camera);

            keyInput left = gameEngine.getInput(Keyboard.Key.A);
            keyInput up = gameEngine.getInput(Keyboard.Key.W);
            keyInput right = gameEngine.getInput(Keyboard.Key.D);
            keyInput down = gameEngine.getInput(Keyboard.Key.S);
            int frameCounter = 0;
            int count = 0;
            while (gameEngine.checkWindowOpen()) {
                gameEngine.renderFrame();
                gameEngine.getInput();
                count += 1;
                if (frameCounter >= 15) {
                    frameCounter = 0;
                }
                if (count == 0 || count == 3)
                {
                    player.setFrame(frameCounter);
                    frameCounter++;
                    count = 0;
                }

                if (left.keyDown())
                {
                    player.position.X -= 5;
                }
                if (up.keyDown()) {
                    player.position.Y -= 5;
                }
                if (right.keyDown()) {
                    player.position.X += 5;
                }
                if (down.keyDown())
                {
                    player.position.Y += 5;
                }
                System.Threading.Thread.Sleep(17);
                
            }
        }
    }
}
