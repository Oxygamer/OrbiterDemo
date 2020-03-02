using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetType = PlanetBehavior.PlanetType;

public class StarSystemController : MonoBehaviour {

    public static StarSystemController Instance;

    public static float G = 9.81f;

    public float MinPlanetDistance = 5f;
    public float MaxPlanetDistance = 30f;

    public float PlanetInitSpeedMin = 30f;
    public float PlanetInitSpeedMax = 40f;

    public float PlanetInitMassMin = 1f;
    public float PlanetInitMassMax = 2f;

    public int PlanetCount = 2;

    //prefab for default planet
    public GameObject PlanetPrefab;
    public GameObject SunObject;
    public GameObject HpBarPrefab;

    private float lastInterfaceUpdateTime = 0f;
    List<PlanetBehavior> PlanetList = new List<PlanetBehavior>();
    List<HealthBar> PlanetBarsList = new List<HealthBar>();

    bool isPause = false;

    private void Awake()
    {
        Instance = this;    
    }

    // Use this for initialization
    void Start () {

        InitializeSystem();

        Messenger.AddListener(MessageKeys.ON_RESTART, ResetSystem);
        Messenger.AddListener<bool>(MessageKeys.ON_PAUSE, OnPause);

    }

    public void OnPause(bool value)
    {
        isPause = value;
    }

    public void ResetSystem()
    {
        Debug.Log("Clean previous system");

        int planetCount = PlanetList.Count;
        for(int i=0;i<planetCount;i++)
        {
            PlanetBehavior planet = PlanetList[0];
            PlanetList.RemoveAt(0);
            Destroy(planet.gameObject);
        }

        Storage.Instance.CleanPool();

        PlanetList.Clear();
        PlanetBarsList.Clear();

        //spawn new objects
        InitializeSystem();

        //unpause
        Messenger.Broadcast(MessageKeys.ON_CONTINUE);

    }

    void InitializeSystem()
    {
        //Create planets here

        float currentDistance = MinPlanetDistance;

        List<int> FreePlanets = new List<int>();

        for (int i = 0; i < PlanetCount; i++)
        {
            //Generate random position
            Vector3 newPos = new Vector3(0f, 0f, 0f);

            //try to avoid overlays of planets

            currentDistance += Random.Range(1f, 7f);
            float dist = currentDistance;

            //check that planet on proper distance from each other
            float resultSpeed = Random.Range(PlanetInitSpeedMin, PlanetInitSpeedMax);

            //ability to set any start angle on orbit
            newPos.x = Mathf.Sin(0f)* dist;
            newPos.y = Mathf.Cos(0f) * dist;

            //better if depends from size
          //  float newPlanetMass = Random.Range(PlanetInitMassMin, PlanetInitMassMax);
         
            GameObject newPlanet = Instantiate(PlanetPrefab, newPos, PlanetPrefab.transform.rotation);
            PlanetBehavior planet = newPlanet.GetComponent<PlanetBehavior>();
            planet.ID = i;

            //randomize planet type
            int randomType = Random.Range(0, 3);
            PlanetType t = (PlanetType)randomType;

            int weaponType = Random.Range(1, 4);
            //choose weapon here

            planet.Initialize(resultSpeed,t,weaponType);

            PlanetList.Add(planet);

            GameObject barObject = Instantiate(HpBarPrefab, newPos + new Vector3(0f, 10f, -10f), Quaternion.identity);
            HealthBar bar = barObject.GetComponent<HealthBar>();
            bar.AttachPlanet(planet);
            PlanetBarsList.Add(bar);
            Storage.Instance.AddToPool(barObject);

            //add offset from planet
            currentDistance += planet.GetSize() + 3f;
            FreePlanets.Add(planet.ID);
        }

        int playerPlanetIndex = Random.Range(0,FreePlanets.Count);
        int playerPlanetID = FreePlanets[playerPlanetIndex];
        FreePlanets.Remove(playerPlanetID);

        //also select planet for enemy
        int enemyIndex = Random.Range(0, FreePlanets.Count);
        int enemyPlanetID= FreePlanets[enemyIndex];
        FreePlanets.Remove(enemyPlanetID);


        foreach (PlanetBehavior planet in PlanetList)
        {
            if(planet.ID== playerPlanetID)
            {
                planet.SetPlanetOwner(PlanetBehavior.PlanetOwnerType.Player);
                Messenger.Broadcast(MessageKeys.ON_PLANET_SETTED, planet, planet.GetWeaponID());
                Debug.Log("Player planet ID:" + playerPlanetID);

            }else if(planet.ID==enemyPlanetID)
            {
                planet.SetPlanetOwner(PlanetBehavior.PlanetOwnerType.AI);
                Debug.Log("AI planet ID:" + playerPlanetID);
            }
            else
            {
                planet.SetPlanetOwner(PlanetBehavior.PlanetOwnerType.None);
            }
        }
    }

    void UpdatePlanetsUI()
    {
        
        for(int i=0;i<PlanetBarsList.Count;i++)
        {

            HealthBar bar = PlanetBarsList[i];

            if (bar != null)
            {
                float hp = bar.UpdateFill();

                if (hp <= 0f)
                {
                    //remove it
                    bar.gameObject.SetActive(false);
                    //destroy it after game finished
                }
            }
        }
    }

    public Vector3 CalculateGravityForObject(Vector3 objectPos, float objectMass,int ignoreObjectLayer)
    {
        Vector3 resultGravity = Vector3.zero;
        Vector3 sunPos = SunBehavior.Instance.transform.position;
        float sunMass = SunBehavior.Instance.Mass;

        Vector3 gravityDirection = sunPos - objectPos;
        float distance = Vector3.Distance(sunPos, objectPos);
        float gravityForce = CalculateGravityBetween(sunPos, sunMass, objectPos, objectMass);

        Vector3 sunGravityVector = (sunPos - objectPos).normalized;

        resultGravity += (sunGravityVector*gravityForce);

        foreach(PlanetBehavior planet in PlanetList)
        {
            if (planet.ID != ignoreObjectLayer)
            {
                float planetGravity = CalculateGravityBetween(objectPos, objectMass, planet.transform.position, planet.Mass);
                Vector3 planetGravityVector = (planet.transform.position - objectPos).normalized;

                resultGravity += (planetGravityVector * planetGravity);
            }
        }


        return resultGravity;

    }

    public float CalculateGravityBetween(Vector3 point1,float mass1,Vector3 point2,float mass2)
    {
        float distance = Vector3.Distance(point1, point2);
        float gravityForce = (mass1 * mass2 * G) / Mathf.Pow(distance, 2f);

        return gravityForce;

    }

    public void RemovePlanet(PlanetBehavior planet)
    {
        PlanetList.Remove(planet);
    }

	// Update is called once per frame
	void Update () {

        if(isPause)
        {
            return;
        }

        //maybe call this on events
        if(Time.time-lastInterfaceUpdateTime>0.1f)
        {
            lastInterfaceUpdateTime = Time.time;
            UpdatePlanetsUI();
        }
		
	}
}
