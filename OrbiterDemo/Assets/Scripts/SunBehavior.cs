using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehavior : MonoBehaviour {

    public static SunBehavior Instance;
    public float Mass;


    private void Awake()
    {
        Instance = this;
    }
	
}
