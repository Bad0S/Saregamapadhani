using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class health : MonoBehaviour {
	public GameObject healItem;
	public int counterHeal;// compte les combos ici
	public static bool counterReset;// remet le compteur de combos à 0
	public int life = 1;
	public int damage = 1;
	public bool invincible;
	public float invincibleTime;
	float currentTime;
	public List<Sprite> LifeBar;
	public Sprite pvVide;


	// Use this for initialization
	void Start () 
	{
		
	}
	//si se fait soigner
	public void Heal( int lifeToGain)// la fonction pour soigner
	{
		life += lifeToGain;
	}
	//si prend du dégât
	public void Hurt( int lifeToLose)
	{
		//permet des frames d'invincibilité
		if (invincible == false) 
		{
			if (gameObject.tag == "Player") 
			{
				LifeBar [life-1] = pvVide;
			}
			if (gameObject.tag == "Enemy") 
			{
				
				GameObject.FindGameObjectWithTag ("Player").GetComponent <Rythme>().combo += lifeToLose ;
			}
			life -= lifeToLose;
		}
		//fait drop un objet de soin
		if (life <= 0f)
		{
			if(gameObject.tag == "Enemy")
			{
				//GameObject drop = (GameObject)Instantiate (healItem, transform.position, transform.rotation);
				try
				{
					if(GetComponent <EnemyBehaviour>().grabbed == true)
					{
						GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().grabbed = false ;
						GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().GrabUngrab () ;
					}
				}
				catch 
				{
					
				}
				GetComponent <EnemyBehaviour>().playerVictimeRend.color = Color.white;

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

	void OnTriggerEnter2D (Collider2D other)
	{
		if (gameObject.tag == "Enemy" && other.tag == "PlayerAttack") 
		{
			Hurt (other.GetComponentInParent<health>().damage);
		}
	}

	IEnumerator PlayerDeath()
	{
		gameObject.GetComponent<Animator>().SetTrigger ("Mort");
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Destroy (gameObject);
	}
}
