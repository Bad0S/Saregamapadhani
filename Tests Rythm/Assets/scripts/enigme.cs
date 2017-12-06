using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Experimental.UIElements;
using NUnit.Framework.Internal;
using UnityEngine.Networking;
using System;
using UnityEngine.EventSystems;

public class enigme : MonoBehaviour {

	/*public List<int> parameter = new List<int>();
	parameter.Add*/
	public int[] button;
	public int[] timeStarting;
	public int[] timeFrame;
	private int[] parameters;
	private int switcher = 0;
	private float timerFrames;
	private bool checker;
	private SpriteRenderer enigmaRenderer;
	private int count=0;
	private bool kill;
	string test;
	public static bool entered;

	// Use this for initialization
	void Start () 
	{
		enigmaRenderer = GetComponent<SpriteRenderer> ();
		// GetComponent<SpriteRenderer>().color = new Color(1,0,0) change la couleur du sprite, ici en rouge
		parameters = new int[button.Length + timeStarting.Length + timeFrame.Length];
		print (parameters.Length);

		for (int i = 0; i < parameters.Length; i++) {
			if (switcher == 2) {
				parameters [i] = timeFrame [Mathf.FloorToInt (i / 3)];
				switcher = 0;
				continue;
			}

			if (switcher == 1) {
				parameters [i] = timeStarting [Mathf.FloorToInt (i / 3)];
				switcher = 2;
				continue;
			}
				
			if (switcher == 0) {
				parameters [i] = button [Mathf.FloorToInt (i / 3)];
				switcher = 1;
				continue;
			}
		}
	}
		
	void enigma (int[] parameters)
	{
		if(Input.GetButtonDown ("1")== true || Input.GetButtonDown ("2")== true ||Input.GetButtonDown ("3")== true || checker==true ){
			checker = true;
			//for (int i = 0; i <= (parameters.Length -1); i+=3) {
			enigmaRenderer.color = Color.gray;
			//print (parameters [count]);
				//placeholder
			if (parameters[count] ==1 && (parameters[count+1]/100) < timerFrames && timerFrames< ((parameters[count+1] + parameters[count+2])/100))
				{
					enigmaRenderer.color = Color.yellow;
				}
			else if (parameters[count] ==2 && (parameters[count+1]/100) < timerFrames && timerFrames< ((parameters[count+1] + parameters[count+2])/100))
				{
					enigmaRenderer.color = Color.blue;
				}
			else if(parameters[count] ==3 && (parameters[count+1]/100) < timerFrames && timerFrames< ((parameters[count+1] + parameters[count+2])/100))
				{
					enigmaRenderer.color = Color.green;
				}
			if(Input.anyKeyDown == true && (parameters [count + 1] / 100) > timerFrames && timerFrames>0 ){
				checker = false;
				timerFrames = 0f;
				count = 0;
			}
				
			if (Input.GetButtonDown (Convert.ToString (parameters [count])) == true && (parameters [count + 1] / 100) < timerFrames && timerFrames < ((parameters [count + 1] + parameters [count + 2]) / 100)) {
				timerFrames = 0f;
				if (count < (parameters.Length - 4)) {
					count += 3;
				}
				else{
					kill = true;
				}
				print (count);
			}
			else if (timerFrames > ((parameters[count+1] + parameters[count+2])/100) ){
				checker = false;
				timerFrames = 0f;
				count = 0;
			} /*EventSystem.current.currentSelectedGameObject.name retourne le nom du bouton sur lequel t'appuies*/
			if(Input.anyKeyDown == true &&Input.GetButtonDown (Convert.ToString (parameters [count])) == false && (parameters [count + 1] / 100) < timerFrames && timerFrames < ((parameters [count + 1] + parameters [count + 2]) / 100)) {
				checker = false;
				timerFrames = 0f;
				count = 0;
			}
		}
	}
		
	// Update is called once per frame
	void Update () 
	{
		if (entered == true) {
			enigma (parameters);
		}
		if (checker == true){
			timerFrames += Time.deltaTime;
			//print (timerFrames);
		}
		else{
			enigmaRenderer.color = Color.white;
		}
		if (kill == true) {
			print ("enigme resolue");
			block.kill = true;
			Destroy (gameObject);
		}
	//print (EventSystem.current.currentSelectedGameObject.name);
	}
}
