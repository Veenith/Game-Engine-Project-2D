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
            if (e.Code == Keyboard.Key.Escape) {
                gameWindow.Close();
            }
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
            obj1.setImage("C:\\Users\\Danh\\Downloads\\Pokemon_Go.png");
            gameEngine.registerObject(obj1);
            obj1.scale = new SFML.System.Vector2f(1f, 1f);
            GameObject obj2 = new GameObject();
            obj2.setImage("C:\\Users\\Danh\\Downloads\\Pokemon_Go.png");
            obj2.position = new SFML.System.Vector2f(300, 0);
            //gameEngine.registerObject(obj2);

            Player player = new Player();
            gameEngine.registerObject(player);

            PlayerCam camera = new PlayerCam(player);
            gameEngine.registerCamera(camera);

            while (gameEngine.checkWindowOpen()) {
                gameEngine.renderFrame();
                gameEngine.getInput();
                System.Threading.Thread.Sleep(17);
                
            }
        }
    }
}
