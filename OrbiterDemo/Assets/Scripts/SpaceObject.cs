using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour {

    public int TypeID = 1;
    public int OwnerID = 0;
    public float Mass;

    protected bool isInitialized = false;
    protected Vector3 currentDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
