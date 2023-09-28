using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New_Item", menuName = "Inventory/ItemData")]

public class ItemData : ScriptableObject
{
    public string Name => itemName;
    public string ShortName => shortName;
    public List<string> Description => itemDesc;
    public Image Sprite => itemSprite;

    [SerializeField] private string shortName;
    [SerializeField] private string itemName;
    [SerializeField] private List<string> itemDesc;
    [SerializeField] private Image itemSprite;
}
