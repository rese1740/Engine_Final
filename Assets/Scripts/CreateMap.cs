using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject dirtBlock;
    public GameObject grassBlock;
    public GameObject waterBlock;
    public GameObject goldBlock;
    public int w = 20;
    public int d = 20;
    public int maxHeight = 16;
    public int waterHeight = 10;
    public float spawnOre = 0.3f;

    [SerializeField] float noiseScale = 20f;
    public List<GameObject> blocks = new List<GameObject>();

    private void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for (int x = 0; x < w; x++)
        {
            for (int z = 0; z < d; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);
                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for (int y = 0; y <= h; y++)
                {
                    float per = Random.Range(0f, 2f);

                    if (y == h)
                    {
                        PlaceGrass(x, y, z);
                    }
                    else if (per >= spawnOre)
                    {
                        PlaceStone(x, y, z);
                    }
                    else
                    {
                        PlaceGold(x, y, z);
                    }
                }

                if (h < waterHeight)
                {
                    for (int y = h + 1; y <= waterHeight; y++)
                    {
                        PlaceWater(x, y, z);
                    }
                }

            }
        }
    }

    void PlaceGold(int x, int y, int z)
    {
        var go = Instantiate(goldBlock, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Gold_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Gold;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

    void PlaceStone(int x, int y, int z)
    {
        var go = Instantiate(dirtBlock, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"S_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Stone;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

    void PlaceGrass(int x, int y, int z)
    {
        var go = Instantiate(grassBlock, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"G_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();   
        b.type = BlockType.Grass;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }
    void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(waterBlock, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"W_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Water;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

}
