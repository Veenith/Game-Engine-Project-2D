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
        public RenderWindow init()
        {
            RenderWindow window = new RenderWindow(new VideoMode(1080, 760), "test", Styles.Default);
            return window;
        }
        public void setClearColor(Color newColor) {
            clearColor = newColor;
        }

        public Sprite drawImage() {
            Image image = new Image("C:\\Users\\Danh\\Downloads\\Pokemon_Go.png");
            Sprite sprite = new Sprite();
            Texture texture = new Texture(1080,760);
            texture.Update(image,100,100);
            sprite.Texture = texture;
            sprite.Scale = new SFML.System.Vector2f(0.3f,0.3f);
            return sprite;
        } 

        public void frame(RenderWindow window)
        {
            window.Clear(clearColor);
            window.Draw(drawImage());
            window.Display();
        }
    }
}
