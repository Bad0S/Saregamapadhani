using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour {
	public GameObject healItem;
	public int counterHeal;// compte les combos ici
	public static bool counterReset;// remet le compteur de combos à 0
	public float life = 1f;
	public bool invincible;
	public float invincibleTime;
	float currentTime;


	// Use this for initialization
	void Start () {
		
	}

	public void Heal( float lifeToGain)// la fonction pour soigner
	{
		life += lifeToGain;
	}

	public void Hurt( float lifeToLose)
	{

		if (invincible == false) 
		{
			life -= lifeToLose;
			//print (life);
		}
		if (life <= 0f)
		{
			if(gameObject.tag == "Enemy"){
				GameObject drop = (GameObject)Instantiate (healItem, transform.position, transform.rotation);
			}
			Destroy (gameObject);
		}
	}
		
	// Update is called once per frame
	void Update () {
		/*counterHeal = combo.counter;//appelle dans combo
		if (counterHeal >= 5){
			GameObject clone = (GameObject)Instantiate (healItem, transform.position, transform.rotation);// créé les objets de soin
			counterReset = true;
		}
		if (counterHeal == 0){
			counterReset = false;
		}*/
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
}
