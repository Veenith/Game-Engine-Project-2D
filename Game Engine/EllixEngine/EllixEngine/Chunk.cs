using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace EllixEngine
{
    class Chunk
    {
        Image[] blockTextures;
        GameObject[,] terrain;
        GameObject terrainComposite;
        int blockSize = 30, chunkSize = 30;
        float x = 0, y = 0;

        public Chunk(float x = 0, float y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public void create(string texturePath, int numTextures, Ellix gameEngine, int layer) //Temporary; should support loading multiple textures
        {
            blockTextures = new Image[numTextures];             //block texture dimensions (pixels) must match block size
            blockTextures[0] = new Image(@texturePath);
            terrain = new GameObject[chunkSize, chunkSize];

            terrainComposite = new GameObject();
            terrainComposite.position.X = x;
            terrainComposite.position.Y = y;
            terrainComposite.setImage(new Image((uint)(chunkSize * blockSize), (uint)(chunkSize * blockSize), Color.Transparent));

            for (int i = 0; i < chunkSize; i++)
            {
                for (int j = 0; j < chunkSize; j++)
                {
                    if (!(i < 10 && j == 0))
                    {
                        terrain[i, j] = new GameObject();
                        terrain[i, j].setImage(blockTextures[0]);
                        terrain[i, j].setupCollider();
                        terrain[i, j].position.X = x + i * blockSize;
                        terrain[i, j].position.Y = y + j * blockSize;
                        terrainComposite.Img.Copy(terrain[i, j].Img, (uint)(i * blockSize), (uint)(j * blockSize));
                    }
                }
            }

            gameEngine.registerObject(terrainComposite, layer);
        }

        public void checkCollisionInChunk(GameObject obj, ref Physics physicsEngine)
        {
            float distanceThreshold = 70;
            for (int i = 0; i < chunkSize; i++)
                for(int j = 0; j < chunkSize; j++)
                    if(terrain[i,j] != null)
                        //if(distance(obj.position, terrain[i,j].position) < distanceThreshold)
                            physicsEngine.collisionDetection(obj, terrain[i,j]);
            physicsEngine.applyCollision(obj);
        }

        private float distance(Vector2f p1, Vector2f p2)
        {
            return (float)Math.Sqrt(Math.Pow((double)(p1.X - p2.X), 2.0) + Math.Pow((double)(p2.Y - p2.Y), 2.0));
        }
    }
}