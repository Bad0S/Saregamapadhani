using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflect : MonoBehaviour {

	public float PosX;
	public float PosY;
	public float MaxPosX;
	public float MaxPosY;
	public float MinPosX;
	public float MinPosY;

	public bool on_rail_H;
	public bool on_rail_V;
	// Use this for initialization
	void Start () {
		PosX = transform.position.x;
		PosY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (on_rail_H == true) {

			if (Input.GetKeyUp (KeyCode.LeftArrow) && PosX > MinPosX) {
				PosX -= 1;
				transform.position = new Vector3 (PosX, transform.position.y, 0);

			} else if (Input.GetKeyUp (KeyCode.RightArrow) && PosX < MaxPosX) {
				PosX += 1;
				transform.position = new Vector3 (PosX, transform.position.y, 0);

			}
		}

		if (on_rail_V == true) {
			

			if (Input.GetKeyUp (KeyCode.UpArrow) && PosY < MaxPosY) {
				
				PosY += 1;
				transform.position = new Vector3 (transform.position.x, PosY, 0);

			} else if (Input.GetKeyUp(KeyCode.DownArrow) && PosY > MinPosY) {
				
				PosY -= 1;
				transform.position = new Vector3 (transform.position.x, PosY, 0);

			}
	}
}




}
