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
        bool[][] layerInteraction;
        Collision[] collisionArray;
        int numCollision = 0;


        public Physics()
        {
            clock = new Clock();
            time1 = clock.ElapsedTime;
            collisionArray = new Collision[12];
        }

        public void initInteraction(bool[][] layerInteraction)
        {
            this.layerInteraction = layerInteraction;
        }

        private float min(float f1,float f2) {
            if (f1 < f2) {
                return f1;
            }
            return f2;
        }

        private float max(float f1, float f2) {
            if (f1 > f2) {
                return f1;
            }
            return f2;
        }

        public void collisionDetection(GameObject obj1, GameObject obj2) {
            float halfWidth = (float)(0.5 * ((obj1.colliderWidth * 2) + (obj2.colliderWidth * 2)));
            float halfHeight = (float)(0.5 * ((obj1.colliderHeight * 2) + (obj2.colliderHeight * 2)));
            float overlapTop = 0;
            float overlapBottom = 0;
            float overlapRight = 0;
            float overlapLeft = 0;
            float dx = obj1.position.X - obj2.position.X;
            float dy = obj1.position.Y - obj2.position.Y;

            if (Math.Abs(dx) < halfWidth && Math.Abs(dy) < halfHeight)
            {
                collisionArray[numCollision] = new Collision();
                float wy = halfWidth * dy;
                float hx = halfHeight * dx;
                collisionArray[numCollision].collisionObject = obj2;
                collisionArray[numCollision].velocity = obj1.velocity;
                collisionArray[numCollision].translation = obj1.position;
                if (wy > hx)
                    if (wy > -hx)
                    {
                        collisionArray[numCollision].translation.Y = obj2.position.Y + obj1.colliderHeight + obj2.colliderHeight;
                        collisionArray[numCollision].velocity.Y = 0;
                    }
                    else
                    {
                        collisionArray[numCollision].translation.X = obj2.position.X - obj1.colliderWidth - obj2.colliderWidth;
                        collisionArray[numCollision].velocity.X = 0;
                    }
                else
                {
                    if (wy > -hx)
                    {
                        collisionArray[numCollision].translation.X = obj2.position.X + obj1.colliderWidth + obj2.colliderWidth;
                        collisionArray[numCollision].velocity.X = 0;

                    }
                    else
                    {
                        collisionArray[numCollision].translation.Y = obj2.position.Y - obj1.colliderHeight - obj2.colliderHeight;
                        collisionArray[numCollision].velocity.Y = 0;
                    }
                }

                overlapTop = max(obj2.position.Y - obj2.colliderHeight ,obj1.position.Y - obj2.colliderHeight);
                overlapBottom = min(obj2.position.Y + obj2.colliderHeight, obj1.position.Y + obj2.colliderHeight);
                overlapLeft = max(obj1.position.X - obj1.colliderWidth, obj2.position.X - obj2.colliderWidth);
                overlapRight = min(obj1.position.X + obj1.colliderWidth, obj2.position.X + obj2.colliderWidth);

                collisionArray[numCollision].collisionArea = Math.Abs((overlapBottom - overlapTop) * (overlapRight - overlapLeft));
     
                numCollision++;
            }

        }

        public void applyCollision(GameObject entity) {
            Collision maxAreaCol = new Collision();
            float currentMaxArea = 0;
            if (numCollision > 0)
            {
                for (int i = 0; i < numCollision; i++)
                {
                    if (collisionArray[i].collisionArea > currentMaxArea)
                    {
                        currentMaxArea = collisionArray[i].collisionArea;
                        maxAreaCol = collisionArray[i];
                    }
                }
                collisionArray = new Collision[12];
                numCollision = 0;
                entity.position = maxAreaCol.translation;
                entity.velocity = maxAreaCol.velocity;
            }
        }

        private long deltaTime() {
            time2 = clock.ElapsedTime;
            long deltaTime = time2.AsMicroseconds() - time1.AsMicroseconds();
            time1 = time2;
            Console.WriteLine(1000000 / deltaTime);
            return deltaTime;
        }

        private vector2f calculateDistance(GameObject obj, long deltaT) {
            return (obj.velocity * (float)deltaT / 10000);
        }

        public vector2f calculateVelocity(GameObject obj, long deltaT)
        {
            return (obj.acceleration * (float)deltaT / 10000);
        }

        public void updatePhysics(GameObject[,] objArray, int[] numObj)
        {
            long deltaT = deltaTime();
            for (int i = 0; i < numObj.Length; i++) {
                for (int j = 0; j < numObj[i]; j++) {
                    if (!objArray[i, j].Fixed)
                    {
                        objArray[i, j].velocity += calculateVelocity(objArray[i, j], deltaT);
                        objArray[i, j].position += calculateDistance(objArray[i, j], deltaT);
                    }
                }
            }
        }

    }
    class Collision{
        public float collisionArea = 0;
        public GameObject collisionObject;
        public Vector2f translation = new vector2f(0,0);
        public Vector2f velocity = new vector2f(0, 0);
    }
}
