using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cheats : MonoBehaviour 
{
	public Text titresPartie;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R) == true || Input.GetKey(KeyCode.JoystickButton5))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		if (Input.GetKeyDown (KeyCode.H) == true) 
		{
			gameObject.GetComponent<health>().Hurt(1);
		}
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			titresPartie.text = "Présentation du jeu";
		}
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			titresPartie.text = "Ce qui a été fait jusqu'à maintenant";
		}
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			titresPartie.text = "Ce que nous avons appris";
		}
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			titresPartie.text = "Ce qu'il reste à faire";
		}
		if (Input.GetKeyDown (KeyCode.Keypad7) == true)
		{
			SceneManager.LoadScene ("scene_LD_0.1");
		}
		if (Input.GetKeyDown (KeyCode.Keypad8) == true)
		{
				SceneManager.LoadScene ("scene_enigmes");
		}
		if (Input.GetKeyDown (KeyCode.Keypad9) == true)
		{
			SceneManager.LoadScene ("Showcase");
		}
        if (Input.GetKeyDown(KeyCode.B) == true)
        {
            gameObject.transform.position = new Vector3 (60,73,0);
        }
    }
}
