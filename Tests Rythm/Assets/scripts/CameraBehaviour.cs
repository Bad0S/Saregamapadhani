using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Transform cam1;

	//si on appuie sur R, recharge la scène

	void Update()
	{
		if (Input.GetKey (KeyCode.R) == true || Input.GetKey(KeyCode.JoystickButton5))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
	}

    void FixedUpdate()
    {
		//création de 2 nouveaux vecteurs, un qui suit la target sur x, l'autre sur y, si le joueur s'éloigne trop du centre de l'écran(valeurs numériques float), la caméra le suit
        Vector3 targetPositionX = new Vector3(target.position.x, transform.position.y, transform.position.z);
        Vector3 targetPositionY = new Vector3(transform.position.x, target.position.y, transform.position.z);
        if (target.position.x <= cam1.position.x - 0.5f || target.position.x >= cam1.position.x + 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPositionX, Time.deltaTime * speed);
        }
        if (target.position.y <= cam1.position.y - 0.5f || target.position.y >= cam1.position.y + 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPositionY, Time.deltaTime * speed);
        }
    }
}
