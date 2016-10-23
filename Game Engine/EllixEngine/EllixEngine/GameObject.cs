using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace EllixEngine
{
    class GameObject
    {
        public SFML.System.Vector2f position = new SFML.System.Vector2f(0, 0);
        public SFML.System.Vector2f scale = new SFML.System.Vector2f(1, 1);
        public Image Img { get; protected set; }
        public bool ImageExists { get; private set; }
        public bool Visible = true, Fixed = true, CompositeRender = false;

        public void setImage(string path)
        {
            Img = new Image(path);
            ImageExists = true;
        }

        virtual public void internalUpdate()
        {
            update();
        }

        virtual public void update()
        {

        }
    }

    class AnimatedGameObject : GameObject {
        Image[] frames;
        public void setAnimation(int width,int height,int numRows,int numColumns) {
            int x = 0;
            int y = 0;
            int counter = 0;
            frames = new Image[numRows * numColumns];
            // i is vertical & j is horizontal
            for (int i = 0; i < numRows; i++) {
                for (int j = 0; j < numColumns; j++) {
                    x = width * j;
                    IntRect rect = new IntRect(x, y, width, height);
                    frames[counter] = new Image((uint) width,(uint) height);
                    frames[counter].Copy(Img, 0, 0, rect);
                    counter += 1;
                }
                y += height;
            }
            Img = frames[0];
        }
        public void setFrame(int frameNum) {
            Img = frames[frameNum];
        }
    }
    
    class Player:AnimatedGameObject
    {
        public Player()
        {
            Fixed = false;
        }
    }

    class Camera:GameObject
    {
        public Camera()
        {
            Visible = false;
            Fixed = false;
        }
    }

    class PlayerCam:Camera
    {
        Player p1;

        public PlayerCam(Player player)
        {
            Visible = false;
            Fixed = false;
            p1 = player;
        }

        override public void internalUpdate()
        {
            position = p1.position;
            update();
        }
    }
}
