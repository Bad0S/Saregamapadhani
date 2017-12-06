using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo : MonoBehaviour {
	int combos;// les combos. J'ai séparé le combo des compte de combo pour faciliter la tâche si jamais on veut mettre des combo modifiers
	public static int counter;// compte les combos
	public float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Y")== true || Input.GetButtonDown ("X")== true ||Input.GetButtonDown ("A")== true){
			combos ++;
			counter++;
			timer = 0;
			print (combos); 
		}
		timer += Time.deltaTime;
		if (timer> 4){
			combos = 0;
		}
		if (health.counterReset == true){
			counter = 0;
		}
	}
}
