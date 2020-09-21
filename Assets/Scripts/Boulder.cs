using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public bool isActive = true;
    public GameObject futureSight;
    public AudioSource crash;
    private bool landed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            landed = true;
            if (Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity) <= 0.5f)
            {
                isActive = false;
                gameObject.layer = 8;
                futureSight.GetComponent<FutureSightHandler>().isActive = false;
            }
            crash.Play();
        }
        if (collision.gameObject.layer == 4)
        {
            landed = true;
            isActive = false;
            gameObject.layer = 8;
            futureSight.GetComponent<FutureSightHandler>().isActive = false;
        }
    }

    private void Update()
    {
        if (isActive && landed && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) <= 5f && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 1f)
        {
            isActive = false;
            gameObject.layer = 8;
            futureSight.GetComponent<FutureSightHandler>().isActive = false;
        }
    }
}
