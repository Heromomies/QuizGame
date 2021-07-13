using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                GenerateTile(x, z);
            }
        }
    }

    void GenerateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);

        if (pixelColor.a == 0)
        {
            // The pixel is transparent
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                if (colorMapping.parent == null)
                {
                    colorMapping.parent = transform;
                }
                Vector3 position = new Vector3(x, colorMapping.height, z);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, colorMapping.parent);
            }
        }
    }
}
