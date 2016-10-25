using System;
using SFML.System;
using vector2f = SFML.System.Vector2f;
using System.Threading.Tasks;


namespace EllixEngine
{
    class Physics
    {
        bool independentTime = true;
        long currentTime;
        Clock clock;
        Time time1;
        Time time2;

        public Physics() {
            clock = new Clock();
            time1 = clock.ElapsedTime;
        }

        private void collisionDetection()
        {

        }

        private long deltaTime() {
            time2 = clock.ElapsedTime;
            long deltaTime = time2.AsMicroseconds() - time1.AsMicroseconds();
            time1 = time2;
            return deltaTime;
        }

        private vector2f calculateDistance(GameObject obj,long deltaT) {
            return (obj.velocity *(float) deltaT/10000);
        }

        /*public calculateAcceleration(GameObject obj,long deltaT)
        {

        }*/

        public void updatePhysics(GameObject[,] objArray, int[] numObj)
        {
            long deltaT = deltaTime();
            for (int i = 0; i < numObj.Length; i++) {
                for (int j = 0; j < numObj[i]; j++) {

                    if (objArray[i, j].hasCollider) {
                        collisionDetection();
                    }
                    objArray[i, j].position += calculateDistance(objArray[i,j],deltaT);  
                }
            }
        }

    }
}
