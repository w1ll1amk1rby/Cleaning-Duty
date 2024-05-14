using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/**
    factory for building any prefab items based on their itemId 
*/
public class ItemFactory : MonoBehaviour
{
    private SortedDictionary<Item.ID, Item> itemDictionary;
    public void Awake()
    {
        this.itemDictionary = this.GenerateItemDictionary();
    }
    public Item CreateItem(Item.ID itemId)
    {
        Item itemToCopy = this.itemDictionary.GetValueOrDefault(itemId);
        if (!itemToCopy)
        {
            return null;
        }
        return Instantiate(itemToCopy, new Vector3(0, 0, 0), Quaternion.identity);
    }
    private SortedDictionary<Item.ID, Item> GenerateItemDictionary()
    {
        SortedDictionary<Item.ID, Item> dictionary = new SortedDictionary<Item.ID, Item>();
        string folderPath = "Items/ItemPrefabs";
        string fullPath = Application.dataPath + "/" + folderPath;
        string[] files = System.IO.Directory.GetFiles(fullPath, "*.prefab", System.IO.SearchOption.AllDirectories);
        foreach (string file in files)
        {
            string assetPath = "Assets/" + file.Substring(Application.dataPath.Length + 1);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            Item item = prefab.GetComponent<Item>();
            if (item)
            {
                dictionary.Add(item.GetItemID(), item);
            }
        }
        return dictionary;
    }
}