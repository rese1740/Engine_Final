using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{

    public GameObject dirtBlock;
    public GameObject grassBlock;
    public GameObject waterBlock;
    public GameObject goldBlock;
    public void PlaceTile(Vector3Int pos, BlockType type)
    {
        switch (type)
        {
            case BlockType.Stone:
                PlaceStone(pos.x, pos.y, pos.z);
                break;

            case BlockType.Grass:
                PlaceGrass(pos.x, pos.y, pos.z);
                break;

            case BlockType.Water:
                PlaceWater(pos.x, pos.y, pos.z);
                break;

            case BlockType.Gold:
                PlaceGold(pos.x, pos.y, pos.z);
                break;
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
