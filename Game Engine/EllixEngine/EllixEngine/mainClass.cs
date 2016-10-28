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

            gameEngine.registerLayers(5, 11000);

            //Create Objects
            MyPlayer player = new MyPlayer();
            player.setImage("player.png");
            player.position.X = -400;
            player.setupCollider();
            player.setAnimation(30,30,1,1,1,1);
            gameEngine.registerObject(player,3);

            /*
            GameObject obj = new GameObject();
            obj.position.X = 60;
            obj.position.Y = -30;
            obj.setImage("C:\\Assets\\square.png");
            obj.setupCollider();
            gameEngine.registerObject(obj);

            
            GameObject[] gameObj = new GameObject[40];
            for (int i = 0; i < 40; i++)
            {
                GameObject obj1 = new GameObject();
                gameObj[i] = obj1;
                obj1.setImage("C:\\Assets\\square.png");
                gameEngine.registerObject(obj1, 4);
                obj1.position.X = (30 * i);
                obj1.setupCollider();
                obj1.scale = new SFML.System.Vector2f(1f, 1f);
            }*/

            Chunk chunk1 = new Chunk(0, 450);
            chunk1.create("block.png", 1, gameEngine, 4);

            PlayerCam camera = new PlayerCam(player);
            gameEngine.registerCamera(camera);

            //Create inputs
            gameEngine.registerInput(Keyboard.Key.A, "left");
            gameEngine.registerInput(Keyboard.Key.W, "up");
            gameEngine.registerInput(Keyboard.Key.D, "right");
            gameEngine.registerInput(Keyboard.Key.S, "down");
            gameEngine.registerInput(Keyboard.Key.Space,"jump");

            Physics physicEngine = gameEngine.getPhysicsEngine();

            //Main loop
            while (gameEngine.checkWindowOpen())
            {
                gameEngine.getInput();
                gameEngine.applyPhysics();
                /*
                for (int i = 0; i < 40; i++)
                    physicEngine.collisionDetection(player,gameObj[i]);
                physicEngine.collisionDetection(player, obj);
                physicEngine.applyCollision(player);
                for (int i = 0; i < 40; i++)
                    physicEngine.collisionDetection(player, gameObj[i]);
                physicEngine.collisionDetection(player, obj);
                physicEngine.applyCollision(player);
                */
                chunk1.checkCollisionInChunk(player, physicEngine);
                chunk1.checkCollisionInChunk(player, physicEngine);
                gameEngine.update();
                gameEngine.renderFrame();

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
            velocity.Y = 0;
          
            if (input.keyDown("left"))
            {
                velocity.X = -3;
            }

            if (input.keyDown("right"))
            {
                velocity.X = 3;
            }

            if (input.keyDown("up"))
            {
                velocity.Y = -3;
            }
            if (input.keyDown("down"))
            {
                velocity.Y = 3;
            }
        }
    }
}
