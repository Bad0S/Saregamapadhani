using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public float Camspeed;
	public Transform trigger1;
	public Transform trigger2;
	public Transform triggerEntrée;
	private bool cinematique;
	private Camera cam;
    private AudioSource camSource;
    public AudioClip calme;
    public AudioClip combat;
	private Vector3 originalPos;
	public bool Effect;
	void Start()
	{
		cam = GetComponent<Camera>();
        camSource = GetComponent<AudioSource>();
	}
	//si on appuie sur R, recharge la scène
	public IEnumerator ScreenShake (float TimeToShake, float magnitude)
	{
		Effect = true;
		originalPos = transform.position;


		float elapsed = 0.0f;

		while (elapsed < TimeToShake) {
			float x = Random.Range (-1f, 1f) * magnitude + originalPos.x;
			float y = Random.Range (-1f, 1f) * magnitude + originalPos.y;
			transform.localPosition = new Vector3 (x, y, originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;


		}
		Effect = false;
		transform.localPosition = originalPos;

	}
	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			print ("a");

			StartCoroutine(ScreenShake(0.3f,0.3f));
		}


		if (target.position.y > trigger1.position.y && target.position.y < trigger2.position.y) 
		{
			cinematique = true;
			transform.position = Vector3.Lerp(transform.position, new Vector3 (60.5f,101.5f,-10f),Time.deltaTime);
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 19f, Time.deltaTime*0.9f);
		}
		if (target.position.y > trigger2.position.y || (target.position.y < trigger1.position.y && target.position.y > triggerEntrée.position.y))
		{
			cinematique = false;
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 7f, Time.deltaTime);
		}
		if (target.position.y < triggerEntrée.position.y) 
		{
			cinematique = true;
			transform.position = Vector3.Lerp(transform.position, new Vector3 (-2.6f,2.5f,-10f),Time.deltaTime);
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 17f, Time.deltaTime*0.9f);
		}
        if (GameObject.FindGameObjectWithTag("Enemy") == true && (GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>().idling == false))
        {
            if (camSource.clip == calme)
            { camSource.volume -= 0.1f * Time.deltaTime * 5f; }
            if (camSource.volume == 0f)
            {
                camSource.clip = combat;
                camSource.Play();
            }
            if (camSource.volume < 1f && camSource.clip == combat)
            {
                camSource.volume += 0.1f * Time.deltaTime*5f;
            }
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == false ||(GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>().idling == true))
        {
            if (camSource.clip == combat)
            { camSource.volume -= 0.1f * Time.deltaTime * 5f; }
            if (camSource.volume == 0f)
            {
                camSource.clip = calme;
                camSource.Play();
            }
            if (camSource.volume < 1f && camSource.clip == calme)
            {
                camSource.volume += 0.1f * Time.deltaTime * 5f;
            }
        }

    }

    void FixedUpdate()
    {
		//création de 2 nouveaux vecteurs, un qui suit la target sur x, l'autre sur y, si le joueur s'éloigne trop du centre de l'écran(valeurs numériques float), la caméra le suit
        Vector3 targetPositionX = new Vector3(target.position.x, transform.position.y, transform.position.z);
        Vector3 targetPositionY = new Vector3(transform.position.x, target.position.y, transform.position.z);
		if (cinematique == false) 
				{
			if (target.position.x <= transform.position.x - 0.8f || target.position.x >= transform.position.x + 0.8f) {
				transform.position = Vector3.Lerp (transform.position, targetPositionX, Time.deltaTime * Camspeed);
			}
			if (target.position.y <= transform.position.y - 0.45f || target.position.y >= transform.position.y + 0.45f) {
				transform.position = Vector3.Lerp (transform.position, targetPositionY, Time.deltaTime * Camspeed);
			}
		}
    }
}
