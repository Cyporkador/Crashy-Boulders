using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, 1f, transform.position.z);
    }
}
