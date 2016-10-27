﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace EllixEngine
{
    class Chunk
    {
        Image[] blockTextures;
        GameObject[,] terrain;
        int blockSize = 30, chunkSize = 30;
        float x = 0, y = 0;

        public void create(string texturePath, int numTextures, Ellix gameEngine, int layer) //Temporary; should support loading multiple textures
        {
            blockTextures = new Image[numTextures];
            blockTextures[0] = new Image(@texturePath);
            terrain = new GameObject[chunkSize, chunkSize];
            for (int i = 0; i < chunkSize; i++)
            {
                for (int j = 0; j < chunkSize; j++)
                {
                    if (!(i < 10 && j == 0))
                    {
                        terrain[i, j] = new GameObject();
                        terrain[i, j].setImage(blockTextures[0]);
                        terrain[i, j].scale.X = blockSize / terrain[i, j].Img.Size.X;
                        terrain[i, j].scale.Y = blockSize / terrain[i, j].Img.Size.Y;
                        terrain[i, j].setupCollider();
                        terrain[i, j].position.X = x + i * (blockSize);
                        terrain[i, j].position.Y = y + j * (blockSize);
                        gameEngine.registerObject(terrain[i, j], layer);
                    }
                }
            }
        }
    }
}