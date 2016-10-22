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
        public void Init() {
            gameRender = new Renderer();
            gameWindow = gameRender.init();
            //Input
            gameWindow.Closed += new EventHandler(OnClose);
            gameWindow.KeyPressed += new EventHandler<KeyEventArgs>(KeyPressed);
        }
        //Input methods
        void KeyPressed(object sender,KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) {
                gameWindow.Close();
            }
        }
        public bool checkWindowOpen() {
            return gameWindow.IsOpen;
        }

        void OnClose(object sender, EventArgs e)
        {
            // Close the window when OnClose event is received
            RenderWindow window = (RenderWindow) sender;
            window.Close();
        }
        //GameEngine Methods
        public void renderFrame() {
            gameRender.frame(gameWindow);
        }

        public void getInput() {
            gameWindow.DispatchEvents();
        }

        static void Main(string[] args)
        {
            Ellix gameEngine = new Ellix();
            gameEngine.Init();
            //while (gameEngine.checkWindowOpen()) {
                gameEngine.renderFrame();
                gameEngine.getInput();
            Console.ReadLine();
            //}
        }
    }
}
