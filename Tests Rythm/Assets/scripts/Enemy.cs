using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Player player;
	public float damage = 0.2f;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "Player" && player.isDashing == false ) 
		{
			other.GetComponent <health>().Hurt(damage);
		}
	}
}
