using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groupie : MonoBehaviour
{
    public bool JoueurDetecte;
    public int pointsDeVie = 1;
    private Rigidbody2D groupieRB;
    public bool idleCanMove = true;
	public LayerMask PlayerMask;
	public Vector2 versJoueur;
	private float angleVersJoueur;
	public Rigidbody2D PlayerRB;
    public float vitesseChasse;
    public bool JoueurBond;
    public float vitesseBond = 50;
	//public Transform JumpCache;
	public float timer; // permet de dire tous les cb il saute 
	// Use this for initialization
	void Awake ()
    {
        groupieRB = GetComponent<Rigidbody2D>();
		PlayerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
			
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if (JoueurDetecte == false && idleCanMove == true)
        {
			StartCoroutine(MoveAndWait(Random.Range(0.7f,1.2f),Random.Range(1f,2f)));
        }

		/*if (JoueurDetecte == true && JoueurBond == false) 
		{
            groupieRB.transform.rotation = Quaternion.Euler(0, 0, angleVersJoueur);
            groupieRB.position += versJoueur * vitesseChasse;
		}*/


        if (JoueurBond == true)
        {
            
			//StartCoroutine(Bond());
        }

		timer += Time.deltaTime;
		print (versJoueur); 
		//versJoueur = new Vector2(PlayerRB.position.x - groupieRB.position.x, PlayerRB.position.y - groupieRB.position.y).normalized;	
	}

	void FixedUpdate ()
	{
		print (versJoueur);
		versJoueur = new Vector2(PlayerRB.position.x - groupieRB.position.x, PlayerRB.position.y - groupieRB.position.y).normalized;
	}

	IEnumerator MoveAndWait(float secMove,float secWait) // l'idle
    {
		idleCanMove = false;
		groupieRB.velocity = (new Vector2 (Random.Range(-50,50),Random.Range(-50,50)));
        yield return new WaitForSeconds(secMove);
		groupieRB.velocity = (new Vector2 (0,0));
		yield return new WaitForSeconds (secWait);
        idleCanMove = true;
    }
	 IEnumerator Bond()
    {
		yield return new WaitForSeconds(0.5f);
		//versJoueur = new Vector2 (PlayerRB.position.x -groupieRB.position.x , PlayerRB.position.y - groupieRB.position.y);
		JoueurBond = false;
		//versJoueur = new Vector2(PlayerRB.position.x - groupieRB.position.x, PlayerRB.position.y - groupieRB.position.y).normalized;
		groupieRB.AddForce (versJoueur.normalized * vitesseBond, ForceMode2D.Impulse);// C la ki saute
		vitesseChasse = 0f;
		print (versJoueur);
        yield return new WaitForSeconds(0.2f);
        //JoueurBond = true; 
		timer = 0; 
		//GetComponent<AIPath>().maxSpeed = 6; Juste pour avoir toujours cette ligne sous le coude ;)

    }
}
