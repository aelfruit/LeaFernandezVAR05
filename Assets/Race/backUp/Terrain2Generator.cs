using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain2Generator : MonoBehaviour
{
    public int depth = 20;

    public int width = 500;
    public int height = 200;

    public float scale = 20f;

    public float offsetX = 10f;
    public float offsetY = 100f;

    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0f, 99);
        offsetY = Random.Range(0f, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offsetX += Time.deltaTime * speed;
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
