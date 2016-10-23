using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace EllixEngine
{
    class Renderer
    {
        Color clearColor = Color.White;
        GameObject[] objArray = new GameObject[100];
        GameObject camera;
        int numObj = 0;

        public RenderWindow init()
        {
            RenderWindow window = new RenderWindow(new VideoMode(1080, 760), "test", Styles.Default);
            return window;
        }

        public void setClearColor(Color newColor) {
            clearColor = newColor;
        }

        public void registerObject(GameObject obj)
        {
            objArray[numObj] = obj;
            numObj++;
        }

        public void registerCamera(Camera cam)
        {
            camera = cam;
        }

        /*public Sprite drawImage() {
            Image image = new Image("C:\\Users\\Danh\\Downloads\\Pokemon_Go.png");
            Sprite sprite = new Sprite();
            Texture texture = new Texture(1080,760);
            texture.Update(image,100,100);
            sprite.Texture = texture;
            sprite.Scale = new SFML.System.Vector2f(0.3f,0.3f);
            return sprite;
        } */

        public void frame(RenderWindow window)
        {
            window.Clear(clearColor);
            //window.Draw(drawImage());

            Sprite sprite = new Sprite();
            for(int i = 0; i < numObj; i++)
            {
                if(objArray[i].Visible && objArray[i].ImageExists)
                {
                    sprite.Texture = new Texture(objArray[i].Img);
                    sprite.Scale = objArray[i].scale;
                    sprite.Position = objArray[i].position - camera.position;
                    window.Draw(sprite);
                    sprite.Texture.Dispose();
                }
            }
            window.Display();
        }
    }
}
