using System;
using SFML.Window;

namespace EllixEngine
{
    class mainClass
    {
        //Main
        static void Main(string[] args)
        {
            //Create Game Engine
            Ellix gameEngine = new Ellix();
            gameEngine.Init("Powered By Ellix", 960, 540);

            gameEngine.registerLayers(3, 11000);

            //Create Objects
            MyPlayer player = new MyPlayer();
            player.setImage("C:\\Assets\\HorsePlayer.png");
            player.setAnimation(400, 248, 3, 5, 15, 4);
            gameEngine.registerObject(player, 0);

            GameObject obj1 = new GameObject();
            obj1.setImage("C:\\Assets\\Pokemon_Go.png");
            gameEngine.registerObject(obj1,0);
            obj1.scale = new SFML.System.Vector2f(1f, 1f);

            GameObject obj2 = new GameObject();
            obj2.setImage("C:\\Assets\\Pokemon_Go.png");
            obj2.position = new SFML.System.Vector2f(300, 0);
            gameEngine.registerObject(obj2, 0);

            PlayerCam camera = new PlayerCam(player);
            gameEngine.registerCamera(camera);

            //Create inputs
            gameEngine.registerInput(Keyboard.Key.A, "left");
            gameEngine.registerInput(Keyboard.Key.W, "up");
            gameEngine.registerInput(Keyboard.Key.D, "right");
            gameEngine.registerInput(Keyboard.Key.S, "down");

            //Main loop
            while (gameEngine.checkWindowOpen())
            {
                
                gameEngine.renderFrame();
                gameEngine.update();
                gameEngine.getInput();
                gameEngine.applyPhysics();

                System.Threading.Thread.Sleep(17);
            }
        }
    }

    class MyPlayer:Player
    {
        public override void update(Input input)
        {
            base.update(input);
            velocity.X = 0;
            if (input.keyDown("left"))
            {
                velocity.X = -10;
            }

            if (input.keyDown("right"))
            {
                velocity.X = 10;
            }

            if (input.keyDown("up"))
            {
                position.Y -= 5;
            }
            if (input.keyDown("down"))
            {
                position.Y += 5;
            }
        }
    }
}
