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
        public Image Img { get; private set; }
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
        { }
    }
    
    class Player:GameObject
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
