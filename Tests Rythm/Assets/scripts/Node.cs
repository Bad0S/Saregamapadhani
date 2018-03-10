using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {

	public bool walkable;
	public Vector3 Worldposition;

	public Node(bool _walkable, Vector3 _worldPos){
		walkable = _walkable;
		Worldposition = _worldPos;
	}
}
