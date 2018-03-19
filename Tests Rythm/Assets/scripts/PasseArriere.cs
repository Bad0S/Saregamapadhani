using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasseArriere : MonoBehaviour 
{
	public List<GameObject> elementsPasseArriere;
	private Transform childrenTrans;
	private SpriteRenderer elementRend;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (GameObject checkPasseArriere in elementsPasseArriere)
		{
			childrenTrans = checkPasseArriere.GetComponentInChildren<Transform> ();
			elementRend = checkPasseArriere.GetComponent<SpriteRenderer> ();
			if (childrenTrans.position.y > transform.position.y) 
			{
				elementRend.sortingOrder = 0;
			} 
			if (childrenTrans.position.y < transform.position.y) 
			{
				elementRend.sortingOrder = 2;
			}
		}
	}
}
