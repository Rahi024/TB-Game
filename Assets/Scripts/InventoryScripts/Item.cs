using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : MonoBehaviour
{
    public Sprite icon; // Icon of the item
    public string itemName; // Name of the item
}
