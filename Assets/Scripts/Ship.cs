using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float speed;
    [SerializeField] private bool move=true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Level ended");
            collision.GetComponent<PlayerMovement>().canMove = false;
            StartCoroutine(NextLevel());
        }
    }

    public IEnumerator NextLevel()
    { 
        yield return new WaitForSeconds(waitTime);
        //while (move)
        //{
        //    transform.Translate(Vector2.right * speed * Time.deltaTime);
        //    Debug.Log("move");
            
        //}
        //yield return new WaitForSeconds(5);
    }
}
