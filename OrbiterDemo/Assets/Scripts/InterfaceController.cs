using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour {

    public CooldownBar Bar;
    float lastInterfaceUpdate = 0f;
    bool isPaused = false;

    public GameObject PausePanel;

	// Use this for initialization
	void Start () {

        Messenger.AddListener(MessageKeys.ON_CONTINUE, OnContinue);
	}

    public void OnContinue()
    {
        isPaused = false;
        PressPause(isPaused);
    }
	
	// Update is called once per frame
	void Update () {

   
        if (Time.time - lastInterfaceUpdate > 0.03f)
        {
            lastInterfaceUpdate = Time.time;
            UpdateInterface();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //make pause
            if(isPaused==false)
            {
                isPaused = true;
                PressPause(isPaused);
              
            }
            else
            {
                isPaused = false;
                PressPause(isPaused);
            }
        }
    }

    void PressPause(bool value)
    {
        Messenger.Broadcast(MessageKeys.ON_PAUSE, value);
        PausePanel.SetActive(value);

        if(value==false)
        {
            PausePanel.SetActive(false);
        }
        else
        {
            PausePanel.SetActive(true);
        }
    }


    void UpdateInterface()
    {
        //get cooldown state
        float percents =  PlayerStatsController.Instance.GetCooldownPercents();
        Bar.SetFill(percents);

    }
}
