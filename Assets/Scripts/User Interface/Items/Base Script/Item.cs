using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item")]
[System.Serializable] 
public class Item : ScriptableObject
{
    [Header("The Base Script for Items")]

    public int id;
    public string itemName;
    [TextArea(3,3)] public string description;
    public enum ItemType
    {
        placeable,
        misc
    }
    public ItemType type;
    public GameObject prefab;
    public Texture icon;
    public int value;
}
