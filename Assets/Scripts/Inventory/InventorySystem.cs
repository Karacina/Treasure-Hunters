using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
     public Dictionary<Item, InventorySlot> dictionary;
    public List<InventorySlot> inventory;

    //public event Action onInventoryChanged;

    private void Awake()
    {
        inventory = new List<InventorySlot>();
        dictionary = new Dictionary<Item, InventorySlot>();
    }
    public InventorySlot Get(Item item)
    {
        if (dictionary.TryGetValue(item, out InventorySlot value))
            return value;
        return null;
    }

    public void Add(Item source)
    {
        if (dictionary.TryGetValue(source, out InventorySlot value))
        {
            Debug.Log(source.displayName + "++");
            value.AddToStack();
        }
        else
        {
            Debug.Log("Add To Inventory - " + source.displayName);
            InventorySlot newItem = new InventorySlot(source);
            inventory.Add(newItem);
            dictionary.Add(source, newItem);
        }
        //onInventoryChanged?.Invoke();
    }
    public void Remove(Item source)
    {
        if (dictionary.TryGetValue(source, out InventorySlot value))
        {
            Debug.Log("Remove from Inventory");
            value.RemoveFromStack();
            if (value.sizeSlot == 0)
            {
                inventory.Remove(value);
                dictionary.Remove(source);
            }
        }
        //onInventoryChanged?.Invoke();
    }
    public void ListItems()
    {
        foreach (var item in inventory)
            Debug.Log(item.item.displayName);
    }
}
public class InventorySlot
{
    public Item item;
    public int sizeSlot=0;

    public InventorySlot(Item reference)
    {
        item = reference;
        AddToStack();
    }
    public void AddToStack()
    {
        sizeSlot++;
    }
    public void RemoveFromStack()
    {
        sizeSlot--;
    }
}