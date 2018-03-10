using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour {
	public GameObject healItem;
	public int counterHeal;// compte les combos ici
	public static bool counterReset;// remet le compteur de combos à 0
	public float life = 1f;
	public float damage = 1f;
	public bool invincible;
	public float invincibleTime;
	float currentTime;


	// Use this for initialization
	void Start () {
		
	}
	//si se fait soigner
	public void Heal( float lifeToGain)// la fonction pour soigner
	{
		life += lifeToGain;
	}
	//si prend du dégât
	public void Hurt( float lifeToLose)
	{
		//permet des frames d'invincibilité
		if (invincible == false) 
		{
			life -= lifeToLose;
			//print (life);
		}
		//fait drop un objet de soin
		if (life <= 0f)
		{
			if(gameObject.tag == "Enemy")
			{
				GameObject drop = (GameObject)Instantiate (healItem, transform.position, transform.rotation);
				Destroy (gameObject);
			}
			if (gameObject.tag == "Player") 
			{
				StartCoroutine (PlayerDeath());
			}
		}
	}
		
	// Update is called once per frame
	void Update () 
	{
		// /!\NE PAS TOUCHER, pour les futurs combos
		/*counterHeal = combo.counter;//appelle dans combo
		if (counterHeal >= 5){
			GameObject clone = (GameObject)Instantiate (healItem, transform.position, transform.rotation);// créé les objets de soin
			counterReset = true;
		}
		if (counterHeal == 0){
			counterReset = false;
		}*/
		//gère les frmes d'invincibilité, par défaut à 0
		currentTime += Time.deltaTime;
		if(currentTime < invincibleTime)
		{
			invincible = true;
		}
		else
		{
			invincible = false;
		}
	}

	IEnumerator PlayerDeath()
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Destroy (gameObject);
	}
}
