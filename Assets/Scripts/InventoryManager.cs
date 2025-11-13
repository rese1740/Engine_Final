using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Search;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject inventorySlotPrefab;
    public static InventoryManager instance;

    private void Start()
    {
        instance = this;
    }

    public void UpdateUI(BlockType type, int count)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                InventorySlot slotScript = slots[i].GetComponentInChildren<InventorySlot>();
                if (slotScript.itemTxt.text.StartsWith(type.ToString()))
                {
                    // 같은 블록 발견 → 수량만 증가시킴
                    string[] parts = slotScript.itemTxt.text.Split('x');
                    int currentCount = 1;
                    if (parts.Length > 1)
                        int.TryParse(parts[1].Trim(), out currentCount);

                    currentCount += count;
                    slotScript.ItemSetting(slotScript.itemImage.color, $"{type} x{currentCount}");
                    Debug.Log($"{type} 수량 업데이트: {currentCount}");
                    return;
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount == 0)
            {
                GameObject newSlot = Instantiate(inventorySlotPrefab, slots[i].transform);
                InventorySlot slotScript = newSlot.GetComponent<InventorySlot>();

                Color color = Color.white;
                switch (type)
                {
                    case BlockType.Grass: color = Color.green; break;
                    case BlockType.Stone: color = Color.gray; break;
                    case BlockType.Water: color = Color.blue; break;
                    case BlockType.Gold: color = Color.yellow; break;
                }

                slotScript.ItemSetting(color, $"{type} x{count}");
                Debug.Log($"새 슬롯 생성: {type} x{count}");
                return;
            }
        }

        Debug.LogWarning("빈 슬롯이 없습니다!");
    }
}
