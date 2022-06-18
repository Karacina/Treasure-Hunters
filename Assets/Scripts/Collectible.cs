using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private Item item;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Debug.Log("You got a " + gameObject.name);
            FindObjectOfType<InventorySystem>().Add(item);
            Destroy(gameObject);
        }
    }
}
