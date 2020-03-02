using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPressContinue()
    {
        Debug.Log("Continue");
        Messenger.Broadcast(MessageKeys.ON_CONTINUE);
    }

    public void OnPressRestart()
    {
        Debug.Log("Restart pressed");
        Messenger.Broadcast(MessageKeys.ON_RESTART);
    }
}
