using UnityEngine;

public enum BlockType
{
    Grass,
    Stone,
    Water,
    Gold,

}

public class Block : MonoBehaviour
{
    [Header("UI ¼³Á¤")]
    public Color itemIcon;

    [Header("Block Stat")]
    public BlockType type = BlockType.Stone;
    public int maxHP = 3;
    [HideInInspector] public int hp;



    public int dropCount = 1;
    public bool mineable = true;
    private bool isDie = false;

    private void Awake()
    {
        hp = maxHP;
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
        if (string.IsNullOrEmpty(gameObject.tag) || gameObject.tag == "Untagged")
            gameObject.tag = "Block";
    }

    public void Hit(int damage, Inventory inven)
    {
        if (!mineable) return;

        hp -= damage;
        if (hp <= 0)
        {
            if (inven != null && dropCount > 0)
            {
                inven.Add(type, dropCount);
            }

            Die();
        }
    }

    void Die()
    {
        gameObject.AddComponent<Rigidbody>();
        transform.localScale *= 0.5f;
        isDie = true;
    }

    public void GetItem()
    {
        if (!isDie)
            return;

        InventoryManager.instance.UpdateUI(type, dropCount);
        Destroy(gameObject);
    }


   
}
