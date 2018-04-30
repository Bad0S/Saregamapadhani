using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework.Internal;

public class Enemy : MonoBehaviour {

    public PlayerTuto player;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	// si le joueur entre en contact avec un ennemi alors qu'il ne dash pas, il subit des dégâts

    void OnTriggerStay2D (Collider2D other)
	{
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerAttack") 
		{
			gameObject.GetComponent<health>().Hurt(other.gameObject.GetComponent<health>().damage);
		}
	}
}
