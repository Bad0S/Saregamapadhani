using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueUltBatonP2 : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 2)
        {
            transform.localScale += Vector3.right / 3;
        }
        if (transform.localScale.x >= 2)
        {
        }
        if (transform.localScale.y < 14)
        {
            transform.localScale += Vector3.up / 3;
        }
        if (transform.localScale.y >= 14)
        {
            Destroy(gameObject);
        }
    }
}
