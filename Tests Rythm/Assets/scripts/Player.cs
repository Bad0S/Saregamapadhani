using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Rigidbody2D body;
	public float MovSpeed;
	public PlayerAttack Swing;
	public PlayerAttack AttaqueBase;
    public AudioClip SwingSound;
    public AudioClip ThrustSound;
    private AudioSource Source;
    private float PlayerRot;
    public float DashSpeed;
    public bool isDashing;
	public float damageDash = 0.35f;

	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
        Source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        body.transform.rotation =Quaternion.Euler (0,0,PlayerRot);
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		body.velocity = new Vector2 (horizontal * MovSpeed, vertical * MovSpeed);
        if (body.velocity.x > 0) { PlayerRot = 270; }
        if (body.velocity.x < 0) { PlayerRot = 90; }
        if (body.velocity.y > 0) { PlayerRot = 0; }
        if (body.velocity.y < 0) { PlayerRot = 180; }
        Attack ();
        if ((body.velocity.x != 0) || (body.velocity.y != 0)) {Source.mute = false;}
        else { Source.mute = true; }
	}
	void Attack()
	{
		if (Input.GetButton ("Fire1") == true) 
		{
           // Source.clip = ThrustSound;
           // Source.Play();
            AttaqueBase.gameObject.SetActive(true);
            StartCoroutine(DisableObject(AttaqueBase.gameObject, .1f));
            return;
        }
        if (Input.GetButtonDown ("Fire2") == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * DashSpeed);
            isDashing = true;
        }
        if (Input.GetButtonUp ("Fire2") == true) { isDashing = false; }
		if (Input.GetButton ("Fire3") == true)
		{
           // Source.clip = SwingSound;
           // Source.Play();
            Swing.gameObject.SetActive(true);
            StartCoroutine(DisableObject(Swing.gameObject, .1f));
            return;
        } 
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Enemy" && isDashing == true)
        {
			other.GetComponent <health>().Hurt(damageDash);
        }
		if (other.name == "EnigmaRoom")
			enigme.entered = true;
		if (other.name == "EnemyMainRoom")
			EnemyMoveBehaviour.detected = true;
    }
    IEnumerator DisableObject(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive (false);
	}

}
