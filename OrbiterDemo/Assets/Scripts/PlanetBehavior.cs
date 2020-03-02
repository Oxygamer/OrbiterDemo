using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : MonoBehaviour {

    public int ID;
    public Material DefaultPlanetMat;
    public Material HotPlanetMat;
    public Material GasPlanetMat;
    public Material ColdPlanetMat;
    public Material PlayerPlanetMat;

    public GameObject PlanetModel;
    public GameObject ArrowObject;
    public GameObject EnemyCircle;

    public PlanetOwnerType OwnerType;
    public PlanetType Type;

    public Color GasPlanetColor1;
    public Color GasPlanetColor2;

    float currentAngle = 0f;
    float orbitalSpeed = 1f;
    float initialForce = 2f;
    float speedMultiplier=0.1f;

    Vector3 initialSpeed = Vector3.zero;
    GameObject RocketPrefab;

    float solidPlanetScaleMin = 3f;
    float solidPlanetScaleMax = 6f;

    float gasPlanetScaleMin = 5f;
    float gasPlanetScaleMax = 8f;

    float minPlanetSpeed = 0.2f;
    float maxPlanetSpeed = 1f;


    float planetDensity = 15f;

    float MaxHealth = 100f;

    bool isInitialized = false;

    bool isDestroyed = false;

    float cameraZ = 110f;

    WeaponStats currentWeaponStats;
    int weaponTypeID = 0;

    float lastLaunchTime = 0f;

    float launchDelayAI=0f;

    bool isPause = false;

    float pathProgress = 0f;

    public float Mass { get; set; }

    public float Health { get; set; }


    // Use this for initialization
    void Start()
    {
        Messenger.AddListener<bool>(MessageKeys.ON_PAUSE, OnPause);
    }

    public void OnPause(bool value)
    {
        isPause = value;
    }

    public float GetHealthPercents()
    {
        return Health / MaxHealth;
    }

    public float GetCooldownPercents()
    {
        if (currentWeaponStats != null)
        {
            float coolDownTime = currentWeaponStats.CooldownTime;

            float timeSinceLaunch = Time.time - lastLaunchTime;

            float percents = Mathf.Clamp01(timeSinceLaunch / coolDownTime);

            return percents;
        }
        else
        {
            return 0f;
        }

    }


    public void Initialize(float initForce,PlanetType type,int weaponType)
    {
        Health = MaxHealth;
       
        initialForce = initForce;
        Type = type;
        weaponTypeID = weaponType;

        //size depends from type too
        float size = 1f;
        if(type!=PlanetType.Gas)
        {
            size = Random.Range(solidPlanetScaleMin, solidPlanetScaleMax);
            transform.localScale = new Vector3(size, size, size);
          
        }
        else
        {
            //Gas planet
            size = Random.Range(gasPlanetScaleMin, gasPlanetScaleMax);
            transform.localScale = new Vector3(size, size, size);

        }

        Mass = size*planetDensity;

        //get vector to Sun
        Vector3 sunPosition = SunBehavior.Instance.transform.position;

        Vector3 gravityDirection = sunPosition - transform.position;

        initialSpeed = Vector3.Cross(gravityDirection, Vector3.forward * -1f)*initialForce;

        orbitalSpeed = Random.Range(minPlanetSpeed, maxPlanetSpeed);

        //set current weapon stats

        //Choose from 3 types of weapons
      
        currentWeaponStats = Storage.Instance.GetWeaponData(weaponType);

        Debug.Log("Setted weapon type:" + currentWeaponStats.ID);

        RocketPrefab = Storage.Instance.GetWeaponPrefab(weaponType);
        //set planet material
        SetPlanetMaterial(Type);

        isInitialized = true;
    }

    public int GetWeaponID()
    {
        return weaponTypeID;
    }


    public float GetSize()
    {
        return transform.localScale.x;
    }

    void SetPlanetMaterial(PlanetType type)
    {
        if(type==PlanetType.Cold)
        {
            PlanetModel.GetComponent<MeshRenderer>().material = ColdPlanetMat;
        }else if(type==PlanetType.Hot)
        {
            PlanetModel.GetComponent<MeshRenderer>().material = HotPlanetMat;
        }else if(type==PlanetType.Gas)
        {
            PlanetModel.GetComponent<MeshRenderer>().material = GasPlanetMat;
            //set random color
            Color c = Color.Lerp(GasPlanetColor1, GasPlanetColor2, Random.value);

            float intensity = Random.Range(1f, 1.5f);

            PlanetModel.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", c);
            PlanetModel.GetComponent<MeshRenderer>().material.SetFloat("_Intensity", intensity);
        }
        else
        {
            PlanetModel.GetComponent<MeshRenderer>().material = DefaultPlanetMat;
        }
    }

    public void SetPlanetOwner(PlanetOwnerType type)
    {
        OwnerType = type;

        //disable or enable arrow
        if(type==PlanetOwnerType.Player)
        {
            ArrowObject.SetActive(true);
        }
        else
        {
            ArrowObject.SetActive(false);
        }

        if(type==PlanetOwnerType.AI)
        {
            EnemyCircle.SetActive(true);
        }
        else
        {
            EnemyCircle.SetActive(false);
        }

        //if(type==PlanetOwnerType.Player)
        //{
        //   PlanetModel.GetComponent<MeshRenderer>().material = PlayerPlanetMat;
        //}
    }


    void FixedUpdate () {

        if(isPause)
        {
            return;
        }
        
	}

    private void Update()
    {
        if(isPause)
        {
            return;
        }

        //move planet
        MovePlanet();

        //Only works if player control planet
        if (OwnerType==PlanetOwnerType.Player)
        {
            //check cooldown

            float cooldownTime = currentWeaponStats.CooldownTime;

            if (Time.time - lastLaunchTime > cooldownTime)
            {
                //shoot rocket
                if (Input.GetMouseButtonDown(0))
                {
                    LaunchRocket();
                }
            }

            Vector3 mousePos = Input.mousePosition;
            //update planet rotation
            Camera cam = Camera.main;
            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,cameraZ));

            transform.LookAt(worldPos,-Vector3.forward);
           
           //  Input.mousePosition;
        }else if(OwnerType==PlanetOwnerType.AI)
        {
            //Ai attack from time to time
            CheckEnemyActions();
        }
    }

    void CheckEnemyActions()
    {
        PlanetBehavior playerPlanet = PlayerStatsController.Instance.GetPlayersPlanet();

        if (playerPlanet != null)
        {
            transform.LookAt(playerPlanet.transform.position, -Vector3.forward);

            float cooldown = GetCooldownPercents();

            if (cooldown >= 1f)
            {
                //set delay before next launch to make enemy more easy
                if (Time.time > lastLaunchTime + currentWeaponStats.CooldownTime + launchDelayAI)
                {
                    //enemy launch his rocket
                    LaunchRocket();

                    launchDelayAI = Random.Range(0.4f, 2.5f);
                }
            }
        }


    }


    void LaunchRocket()
    {
        lastLaunchTime = Time.time;

        float size = transform.localScale.x;

        Vector3 posShift = (transform.forward * size) + new Vector3(0f, 0f, 0f);

        Vector3 launchPosition = transform.position + posShift;
        GameObject rocket= Instantiate(RocketPrefab, launchPosition,Quaternion.identity);
        RocketBehavior newRocket = rocket.GetComponent<RocketBehavior>();
        newRocket.OwnerID = ID;

        //same direction where mouse pointed
        newRocket.Initialize(currentWeaponStats,transform.forward);

        //add to storage
        Storage.Instance.AddToPool(rocket);
    }

    void MovePlanet()
    {
        if(isInitialized)
        {
            Vector3 pos = transform.position;

            //move planet based on its distance from Sun

            //optimize this
            Vector3 sunPos = SunBehavior.Instance.transform.position;

            float distance = Vector3.Distance(sunPos, pos);

            pathProgress += orbitalSpeed*Time.deltaTime;

            pos.x = Mathf.Sin(pathProgress) * distance;
            pos.y = Mathf.Cos(pathProgress) * distance;

            transform.position = pos;
        }
        // calculate vector using initial speed and gravity

    }

    public void MakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health,0f,MaxHealth);

       // Debug.Log("Planet Health: " + Health);

        if(Health<=0f)
        {
            if (isDestroyed == false)
            {
                isDestroyed = true;
                //destroy planet
                DestroyPlanet();

                //make explosion
            }
        }
    }

    void DestroyPlanet()
    {
        StarSystemController.Instance.RemovePlanet(this);
        gameObject.SetActive(false);

    }


    public enum PlanetOwnerType
    {
        None,
        Player,
        AI
    }

    public enum PlanetType
    {
        Cold,
        Hot,
        Gas,
        Dead,
    }
}
