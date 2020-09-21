using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureSightHandler : MonoBehaviour
{

    public Transform playerPos;
    public GameObject boulder;
    public GameObject future;

    public bool isActive = false;


    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //future.transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 1, playerPos.position.z);
            float a = boulder.transform.position.x - playerPos.position.x;
            float b = boulder.transform.position.y - playerPos.position.y;
            float angle = 90/Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2)) * a; // change to formula
            
            future.transform.rotation = Quaternion.Euler(0, 0, -angle);
            future.SetActive(true);
        } else
        {
            future.SetActive(false);
        }
    }
}
