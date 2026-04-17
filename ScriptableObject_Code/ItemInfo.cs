using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Info", fileName = "NewItem")]
public class ItemInfo : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    [TextArea]
    public string description;
    public GameObject itemPrefab;

    [Header("속성")]
    public ItemType type;
    public Sprite icon;

    [Header("코인")]
    public int coinValue;

    [Header("스탯 보정")]
    public List<ItemStatModifier> statModifiers;

    public override string ToString()
    {
        return $"{itemName} ({type}) - {statModifiers.Count} stats";
    }
}

[System.Serializable]
public class ItemStatModifier
{
    public ItemStat stat;
    public float value;
    public float duration;
}
