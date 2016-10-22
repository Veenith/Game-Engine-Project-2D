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

        public void frame(RenderWindow window)
        {
            window.Clear(clearColor);
            window.Display();
        }
    }
}
