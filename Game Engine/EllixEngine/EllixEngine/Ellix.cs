using System;
using SFML.Graphics;
using SFML.Window;


namespace EllixEngine
{
    class Ellix
    {
        Renderer gameRender;
        RenderWindow gameWindow;
        Input input = new Input();
        GameObject[] objArray = new GameObject[100];
        int numObj = 0;
        
        //Initialization
        public void Init(String windowTitle,uint ResWidth,uint ResHeight)
        {
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

        public void registerInput(Keyboard.Key key, string name, bool requireFocus = true)
        {
            keyInput inputKey = new keyInput(key, gameWindow, name, requireFocus);
            input.registerKey(inputKey);
        }


        public bool checkWindowOpen() {
            return gameWindow.IsOpen;
        }

        //GameEngine Methods
        public void update()
        {
            for(int i = 0; i < numObj; i++)
            {
                objArray[i].update(input);
            }
        }

        public void renderFrame()
        {
            gameRender.frame(gameWindow, objArray, numObj);
        }

        public void getInput()
        {
            gameWindow.DispatchEvents();
        }

        public void registerObject(GameObject obj)
        {
            objArray[numObj] = obj;
            numObj++;
        }

        public void registerCamera(Camera cam)
        {
            gameRender.registerCamera(cam);
            registerObject(cam);
        }


        
    }
}
