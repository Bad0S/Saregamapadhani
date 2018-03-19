using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceFader : MonoBehaviour {
	public bool enteredEntrance;
	private Color alphaColor;
	float rangeChildren;
	float advancePlayer;

	// Use this for initialization
	void Start () {
		alphaColor = GetComponent<SpriteRenderer>().color;
		alphaColor.a = 0;
	}
	void Update()
	{
		if (enteredEntrance)
		{
			rangeChildren = transform.GetChild (1).InverseTransformPoint(transform.GetChild (0).position).y;
			advancePlayer = GameObject.FindGameObjectWithTag("Player").transform.InverseTransformPoint(transform.GetChild (0).position).y;
			GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, alphaColor, advancePlayer/rangeChildren);
			print (advancePlayer / rangeChildren);
		}
	}
		
	void OnTriggerEnter2D(){
		enteredEntrance = true;
	}

	void OnTriggerExit2D(){
		enteredEntrance = false;
	}
}
