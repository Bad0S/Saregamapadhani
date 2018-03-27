using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnde : MonoBehaviour 
{
	public int frequence;
	public float LastDirection;
	public float Direction;
	public float PosX;
	public float PosY;
	public float LastPosX;
	public float LastPosY;
    private AudioSource ondeSource;
    private SpriteRenderer ondeRend;
	// Use this for initialization
	void Start () 
{
		LastDirection = 0;
		Direction = 90;
        ondeSource = GetComponent<AudioSource>();
        ondeRend = GetComponent<SpriteRenderer>();
}
	
	// Update is called once per frame
	void Update () 
	{
		
		PosX = transform.position.x;
		PosY = transform.position.y;

		if (Direction == 90)
		{
			transform.Translate (-transform.up* 4.5f * Time.deltaTime);
		}
		else if (Direction == 180) {
			transform.Translate (-transform.right* 4.5f  * Time.deltaTime);
		}
		else if (Direction == 0)
		{
			transform.Translate (transform.right* 4.5f* Time.deltaTime);
		}
		else if (Direction == -90)
		{
			transform.Translate (transform.up* 4.5f * Time.deltaTime);
		}
	}
	void OnTriggerEnter2D (Collider2D other)
	{
		
		LastDirection = -Direction;
		LastPosX = other.GetComponent <Transform> ().position.x;
		LastPosY = other.GetComponent <Transform> ().position.y;

		if (other.tag == "fréquence1") 
		{
			frequence += 1;
		}

		if (other.tag == "Interrupteur") 
		{
            ondeSource.Play();
            ondeRend.enabled = false;
		}

		if (other.tag == "ReflectD") {

		}if (other.tag == "ReflectG") {

		}if (other.tag == "ReflectH") {

		}if (other.tag == "ReflectB") {

		}
	}
	void OnTriggerExit2D( Collider2D  other)
	{


		if(other.tag == "ReflectD") 
		{

			Direction = 0;
			transform.Rotate (new Vector3 (0, 0, LastDirection));
			transform.Rotate (new Vector3 (0, 0, Direction)); 

			transform.position = new Vector3 (LastPosX + 0.1f , PosY, 0);

		} 

		else if(other.tag == "ReflectG") 
		{
			
			Direction = 180;
			transform.Rotate (new Vector3 (0, 0, LastDirection));
			transform.Rotate (new Vector3 (0,0, Direction));
			transform.position = new Vector3 (LastPosX - 0.1f , PosY, 0);

		}

		else if(other.tag == "ReflectH") 
		{
			Direction = 90;
			transform.Rotate (new Vector3 (0, 0, LastDirection));
			transform.Rotate (new Vector3 (0,0, Direction));
			transform.position = new Vector3 (PosX, LastPosY + 0.1f , 0);
		}

		else if(other.tag == "ReflectB") 
		{
			Direction = -90;
			transform.Rotate (new Vector3 (0, 0, LastDirection));
			transform.Rotate (new Vector3 (0,0, Direction));
			transform.position = new Vector3 (PosX, LastPosY -0.1f , 0);

		}
    }
}
