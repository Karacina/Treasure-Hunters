using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().TakeHit();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Boop");
        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2.5f), ForceMode2D.Impulse);
    }
}
