    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage;
    public Text itemTxt;
    public BlockType blockType;

    public void ItemSetting(Sprite color, string txt,BlockType type)
    {
        itemImage.sprite = color;
        itemTxt.text = txt;
        blockType = type;
    }
}
