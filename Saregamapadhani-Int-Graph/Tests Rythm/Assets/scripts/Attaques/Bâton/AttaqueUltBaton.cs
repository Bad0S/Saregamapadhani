using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueUltBaton : MonoBehaviour
{
    public GameObject AttaqueUltBatonP2;
    public Transform PlayerTrans;

    // Use this for initialization
    void Awake ()
    {
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire3"))
        {
            if (transform.localScale.x < 2.5 || transform.localScale.y < 2.5)
            {
                transform.localScale += new Vector3(1, 1, 0) / 10;
            }
        }
        if (Input.GetButtonUp("Fire3") && transform.localScale.x < 2.5 )
        {
            Destroy(gameObject);
        }
            if (transform.localScale.x >= 2.5 || transform.localScale.y >= 2.5)
            {
                transform.localScale = new Vector3(2.5f, 2.5f, 0f);
                if (Input.GetButtonUp("Fire3"))
                {
                Instantiate(AttaqueUltBatonP2, PlayerTrans);
                Destroy(gameObject);
                }
            }
        }
}
