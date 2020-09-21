using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrigger : MonoBehaviour
{
    public GameObject boulder;
    public GameObject futureSight;
    public float angularVelocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boulder.SetActive(true);
            boulder.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;
            boulder.GetComponent<Rigidbody2D>().velocity = new Vector2(-angularVelocity, 0);
            futureSight.GetComponent<FutureSightHandler>().isActive = true;
            futureSight.GetComponent<FutureSightHandler>().boulder = boulder;
            Destroy(gameObject);
        }
    }
}
