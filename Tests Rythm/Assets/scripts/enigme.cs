using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;
using System;
using UnityEngine.EventSystems;

public class enigme : MonoBehaviour {
	// script de lenigme
	public string[] button;
	public int[] timeStarting;
	public int[] timeFrame;
	private object[] parameters;
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
		parameters = new object[button.Length + timeStarting.Length + timeFrame.Length];
		//créé parametres de la fonction qui gère l'enigme
		for (int i = 0; i < parameters.Length-2; i+=3) {
			for (int j = 0; j < 3; j++) {
				if (j == 0) {
					parameters [i + j] = button [i/3];
					continue;
				}
				else if (j == 1) {
					parameters [i + j] = timeStarting [i/3];
					continue;
				}
				else if (j == 2) {
					parameters [i + j] = timeFrame [i/3];
					continue;
				}
			}
		}

	}
	//est appellé par RoomDetection pour déter si joueur dans salle
	public void Detection(GameObject Player){
		entered = true;
	} 
		//la fonction principale
	void enigma (object[] parameters){
		if(Input.GetButtonDown ("Fire3")== true || Input.GetButtonDown ("Fire2")== true ||Input.GetButtonDown ("Fire1")== true || checker==true ){
			checker = true;//tant qu'il est true, ne relance pas la fonct°
			enigmaRenderer.color = Color.gray; //est gris quand il ne faut pas appuyer sur un bouton
				//placeholder, donne la couleur du bouton sur lequel appuyer
			if (Convert.ToString (parameters[count]) == "Fire3" && (Convert.ToInt32 ( parameters[count+1])) < timerFrames*100 && timerFrames*100< ((Convert.ToInt32 ( parameters[count+1]) + Convert.ToInt32 ( parameters[count+2]))))
				{
					enigmaRenderer.color = Color.yellow;
				}
			else if (Convert.ToString (parameters[count]) =="Fire2" && (Convert.ToInt32 ( parameters[count+1])) < timerFrames*100 && timerFrames*100< ((Convert.ToInt32 ( parameters[count+1]) + Convert.ToInt32 ( parameters[count+2]))))
				{
					enigmaRenderer.color = Color.blue;
				}
			else if(Convert.ToString (parameters[count]) == "Fire1" && (Convert.ToInt32 ( parameters[count+1])) < timerFrames*100 && timerFrames*100< ((Convert.ToInt32 ( parameters[count+1]) + Convert.ToInt32 ( parameters[count+2]))))
				{
					enigmaRenderer.color = Color.green;
				}
			// si appuyé sur un bouton  trop tôt , redémarre l'enigme
			if((Input.GetButtonDown("Fire1") == true ||Input.GetButtonDown("Fire2") == true||Input.GetButtonDown("Fire3") == true)  &&Input.GetButtonDown (Convert.ToString (parameters [count])) == false && (Convert.ToInt32 ( parameters[count+1])) > timerFrames*100 && timerFrames*100>0 ){
				checker = false;
				timerFrames = 0f;
				count = 0;
			}
				// si appuyé sur le bon moment, passe à la phase suivante, si dernière phase kill l'énigme
			if (Input.GetButtonDown (Convert.ToString ( parameters [count])) == true && (Convert.ToInt32 ( parameters[count+1])) < timerFrames*100 && timerFrames*100 <  ((Convert.ToInt32 ( parameters[count+1]) + Convert.ToInt32 ( parameters[count+2])))) {
				timerFrames = 0f;
				if (count < (parameters.Length - 4)) {
					count += 3;
				}
				else{
					kill = true;
				}
			}
			//si appuyé sur le mauvais bouton, redémarre l'énigme
			else if ((Input.GetButtonDown("Fire1") == true ||Input.GetButtonDown("Fire2") == true||Input.GetButtonDown("Fire3") == true)  &&Input.GetButtonDown (Convert.ToString (parameters [count])) == false){
				checker = false;
				timerFrames = 0f;
				count = 0;
			}
			//si temps écoulé redémarre l'énigme
			if (( Convert.ToInt32 ( parameters[count+1]) + Convert.ToInt32 ( parameters[count+2])) < timerFrames*100){
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
		//le timer qui mesure le temps de chaque phase de l'énigme
		if (checker == true){
			timerFrames += Time.deltaTime;
		}
		//l'énigme est blanche si inactive
		else{
			enigmaRenderer.color = Color.white;
		}
		//détecte si l'éngime doit être kill ainsi que le bloc
		if (kill == true) {
			block.kill = true;
			Destroy (gameObject);
		}
	}
}
