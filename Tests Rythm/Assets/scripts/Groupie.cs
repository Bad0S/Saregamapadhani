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
	private Vector2 versJoueur;
	private float angleVersJoueur;
	public Rigidbody2D PlayerRB;
    public float vitesseChasse;
    public bool JoueurBond;
    public float vitesseBond;
	// Use this for initialization
	void Awake ()
    {
        groupieRB = GetComponent<Rigidbody2D>();
		PlayerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		versJoueur = new Vector2(PlayerRB.position.x - groupieRB.position.x, PlayerRB.position.y - groupieRB.position.y);
		angleVersJoueur = (Mathf.Atan2(versJoueur.x, versJoueur.y) * -Mathf.Rad2Deg);
	
		if (pointsDeVie <= 0) 
		{
			Destroy (gameObject);
		}
        if (JoueurDetecte == false && idleCanMove == true)
        {
			StartCoroutine(MoveAndWait(Random.Range(0.7f,1.2f),Random.Range(1f,2f)));
        }
		if (Physics2D.OverlapCircle (transform.position, 10f, PlayerMask) != null)
        {
			JoueurDetecte = true;
		} 
		else 
		{
			JoueurDetecte = false;
		}
		if (JoueurDetecte == true && JoueurBond == false) 
		{
            groupieRB.transform.rotation = Quaternion.Euler(0, 0, angleVersJoueur);
            groupieRB.position += versJoueur * vitesseChasse;
		}
        if (Physics2D.OverlapCircle (transform.position, 6f, PlayerMask) != null)
        {
            JoueurBond = true;
        }
        else
        {
            JoueurBond = false;
        }
        if (JoueurBond == true)
        {
            StartCoroutine(Bond());
        }
	}
	IEnumerator MoveAndWait(float secMove,float secWait)
    {
		idleCanMove = false;
		groupieRB.velocity = (new Vector2 (Random.Range(-5,5),Random.Range(-5,5)));
        yield return new WaitForSeconds(secMove);
		groupieRB.velocity = (new Vector2 (0,0));
		yield return new WaitForSeconds (secWait);
        idleCanMove = true;
    }
    IEnumerator Bond()
    {
        JoueurBond = false;
        vitesseChasse = 0f;
        yield return new WaitForSeconds(1.5f);
        groupieRB.AddForce(versJoueur*vitesseBond, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.5f);
        JoueurBond = true;
    }
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Mur") 
		{
			groupieRB.velocity *= 0;
		}
		if (other.tag == "PlayerAttack") 
		{
            //pointsDeVie -= other.gameObject.GetComponent<Caractéristiques>().Degats;
		}
	}
}
