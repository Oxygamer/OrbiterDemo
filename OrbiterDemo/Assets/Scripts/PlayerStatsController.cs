using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour {

    public static PlayerStatsController Instance;

    WeaponStats currentWeaponStats;
    PlanetBehavior currentPlanet;

    private void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start () {

        Messenger.AddListener<PlanetBehavior, int>(MessageKeys.ON_PLANET_SETTED, OnPlanetSelected);
		
	}


    public PlanetBehavior GetPlayersPlanet()
    {
        return currentPlanet;
    }

    public float GetCooldownPercents()
    {
        if (currentPlanet != null)
        {
            return currentPlanet.GetCooldownPercents();
        }
        else
        {
            return 0f;
        }
    }

    public void OnPlanetSelected(PlanetBehavior planet,int weaponID)
    {
        currentWeaponStats = Storage.Instance.GetWeaponData(weaponID);
        currentPlanet = planet;
    }
	
	
}
