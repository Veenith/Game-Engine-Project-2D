using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using Vector2f = SFML.System.Vector2f;
using Vector2u = SFML.System.Vector2u;

namespace EllixEngine
{
    class Renderer
    {
        Vector2f referenceResolution = new Vector2f(1920, 1080);
        Vector2f resolutionScale;
        Color clearColor = Color.White;
        GameObject[] objArray = new GameObject[100];
        GameObject camera;
        int numObj = 0;
        Vector2u screenSize;

        public RenderWindow init(String windowTitle, uint ResWidth,uint ResHeight)
        {
            
            RenderWindow window = new RenderWindow(new VideoMode(ResWidth,ResHeight), windowTitle, Styles.Default);
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

        public void setWindowDimension(RenderWindow window) {
            screenSize = window.Size;
            resolutionScale = divideVector((Vector2f)screenSize, referenceResolution);
        }

        public void registerCamera(Camera cam)
        {
            camera = cam;
        }
        private Vector2f multiplyVector(Vector2f vector1, Vector2f vector2) {
            return new Vector2f(vector1.X * vector2.X, vector1.Y * vector2.Y);
        }

        private Vector2f divideVector(Vector2f vector1, Vector2f vector2)
        {
            return new Vector2f(vector1.X / vector2.X, vector1.Y / vector2.Y);
        }

        public void frame(RenderWindow window)
        {
            window.Clear(clearColor);
            //window.Draw(drawImage());
            setWindowDimension(window);
            Vector2f halfSize = (Vector2f) screenSize / 2;
            Sprite sprite = new Sprite();
            Vector2f halfSpriteSize;
            for(int i = 0; i < numObj; i++)
            {
                if(objArray[i].Visible && objArray[i].ImageExists)
                {
                    sprite.Texture = new Texture(objArray[i].Img);
                    sprite.Scale = multiplyVector(objArray[i].scale, resolutionScale);
                    halfSpriteSize = multiplyVector((Vector2f) sprite.Texture.Size / 2, objArray[i].scale);
                    sprite.Position = multiplyVector(objArray[i].position - camera.position - halfSpriteSize, resolutionScale) +halfSize ;
                    window.Draw(sprite);
                    sprite.Texture.Dispose();
                }
            }
            camera.internalUpdate();
            window.Display();
        }
    }
}
