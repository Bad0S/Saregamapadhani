using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueEstoc : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.localScale.y < 3)
        {
            transform.localScale += Vector3.up/3;
        }
        if (transform.localScale.y >= 3)
        {
            Destroy(gameObject);
        }
	}
}
