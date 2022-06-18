using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public GameObject slotPrefab;
    public InventorySystem inventorySystem;
    [Space]
    [Header("Parametrs")]
    [SerializeField] private int Columns;
    [SerializeField] private int Rows;

    public int inventorySize;
    private void Start()
    {
        //FillInventory();
        inventorySize = Columns * Rows;
    }
    public void UpdateInventory()
    {
        //Debug.Log("destroy");
        foreach (Transform t in transform)
            Destroy(t.gameObject);

        FillInventory();
    }

    private void FillInventory()
    {
        //int startPos = 0;
        //int endPos = 5;
        //Debug.Log("Draw inventory");
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
            {
                //Debug.Log("i: " + i + " j: " + j + " size: " + inventorySize);
                //Debug.Log(inventorySize * i + j);
                if (Columns * i + j < inventorySystem.inventory.Count)
                {
                    AddInventorySlot(inventorySystem.inventory[Columns * i + j]);
                }
                else
                {
                    AddSlot();
                }
            }
    }

    private void AddInventorySlot(InventorySlot item)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);

        SlotDraw slot = obj.GetComponent<SlotDraw>();
        slot.Set(item);
    }
    private void AddSlot()
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);
    }
}
