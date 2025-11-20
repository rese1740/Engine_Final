using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<BlockType, int> items = new();

    InventoryManager invenUI;

    // Unity ¸Þ½ÃÁö ÂüÁ¶ 0°³
    void Start()
    {
        invenUI = FindObjectOfType<InventoryManager>();
    }

    public void Add(BlockType type, int count = 1)
    {
        if (!items.ContainsKey(type)) items[type] = 0;
        items[type] += count;
        Debug.Log($"[inven] +{count} {type} (ÃÑ {items[type]})");
        invenUI.UpdateInventory(this);
    }

    public bool Consume(BlockType type, int count = 1)
    {
        if (!items.TryGetValue(type, out var have) || have < count) return false;

        items[type] = have - count;
        Debug.Log($"[inven] -{count} {type} (ÃÑ {items[type]})");

        if (items[type] == 0)
        {
            items.Remove(type);
            invenUI.selectedIndex = -1;
            invenUI.ResetSelection();
        }

        invenUI.UpdateInventory(this);
        return true;
    }
}
