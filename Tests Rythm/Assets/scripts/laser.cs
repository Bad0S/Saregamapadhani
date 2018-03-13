using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {
	//GameObject caster;
	public float beat = 1;
	// rescale selon une taile donée
	/*void newScale(GameObject theGameObject, float newSize) {

		float size = theGameObject.GetComponent<Renderer> ().bounds.size.x;

		Vector3 rescale = theGameObject.transform.localScale;

		rescale.x = newSize * rescale.x / size;

		theGameObject.transform.localScale = rescale;

	}*/
	void Start () {
		//caster = GameObject.FindGameObjectWithTag ("Caster1");
		Destroy (gameObject, beat);
		//transform.localScale = new Vector3 (caster.GetComponent<Caster> ().laserLength, 1, 1);
		//newScale (gameObject, caster.GetComponent<Caster>().laserLength);

	}//oui c'est pas propre mais la seule méthode que j'ai trouvé pour choper le gameobjet c'est ça

	// Update is called once per frame
	void Update () {
		
	}
}
