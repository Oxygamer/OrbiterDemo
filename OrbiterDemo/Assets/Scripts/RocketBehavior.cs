using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : SpaceObject {

 
    float rocketAccelerationForce;
    float maxForce;
    float speedMultiplier = 1f;
    float slowdownFactor = 0f;  //0.5
    float engineWorkTime = 30f;
    float rocketLaunchTime = 0f;
    Vector3 upVector;
    WeaponStats currentStats;

    bool isPause = false;

    // Use this for initialization
    void Start () {

        //test it
        // Initialize(40f);
        Messenger.AddListener<bool>(MessageKeys.ON_PAUSE, OnPause);
	}

    public void Initialize(WeaponStats stats,Vector3 initDirection)
    {
        rocketLaunchTime = Time.time;
        currentStats = stats;

        rocketAccelerationForce = stats.InitialSpeed;
        maxForce = rocketAccelerationForce;
        Mass = stats.Mass;

        Debug.Log("launched weapon:" + stats.Type);


        //get vector to Sun
        Vector3 sunPosition = SunBehavior.Instance.transform.position;

        Vector3 gravityDirection = sunPosition - transform.position;

      //  currentDirection = Vector3.Cross(gravityDirection, Vector3.forward * -1f) * rocketAccelerationForce;
        currentDirection = initDirection * rocketAccelerationForce;

        //detect orbital force direction
        //if sun on left or right side
        if(AngleDir(initDirection,gravityDirection,-Vector3.forward)>0)
        {
            upVector = -Vector3.forward;
          //  Debug.Log("Up Vector");
        }
        else
        {
            upVector = Vector3.forward;
           // Debug.Log("Down Vector");
        }
      
        isInitialized = true;
    }

    public void OnPause(bool value)
    {
        isPause = value;
    }

    //detects if target on left or right side from rocket
    public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Sun"||other.tag=="Trash")
        {
            //destroy bullet
            RemoveBullet();
        }else if(other.tag=="Planet")
        {
            var planet =  other.gameObject.GetComponent<PlanetBehavior>();
            if (planet.ID != OwnerID)
            {

                //create space trash if its double rocket
                if(currentStats.Type==WeaponType.Double)
                {
                    CreateSpaceTrash();
                }

                //also damage planet
                // Debug.Log("Damage planet: "+other.gameObject.name);
                other.gameObject.GetComponent<PlanetBehavior>().MakeDamage(currentStats.Damage);
                RemoveBullet();
            }
           

        }
    }

    void CreateSpaceTrash()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3 posShift = Vector3.zero;

            posShift.x = Random.Range(2f, 5f);
            posShift.y= Random.Range(2f, 5f);

            Vector3 launchPosition = transform.position + posShift;

            GameObject particlePrefab = Storage.Instance.GetSpaceTrashPrefab();
            GameObject particleObject = Instantiate(particlePrefab, launchPosition, Quaternion.identity);
            SpaceTrashBehavior newTrashParticle = particleObject.GetComponent<SpaceTrashBehavior>();
            newTrashParticle.OwnerID = -1;

            WeaponStats stats = Storage.Instance.GetWeaponData(4);
            //same direction where mouse pointed

            //random direaction
            Vector3 launchDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);

            newTrashParticle.Initialize(stats, launchDirection);

            //add to storage
            Storage.Instance.AddToPool(particleObject);
        }

    }


    void RemoveBullet()
    {
        //Debug.Log("Bullet collided Sun");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

        if(isPause)
        {
            return;
        }

        MoveRocketRealistic();

    }

    void MoveRocketRealistic()
    {
        if (isInitialized)
        {
            Vector3 pos = transform.position;
           
            //optimize this
            Vector3 sunPos = SunBehavior.Instance.transform.position;
            float sunMass = SunBehavior.Instance.Mass;

            Vector3 gravityDirection = sunPos - transform.position;

            //depends from rocket direction

            Vector3 orbitalForce1 = Vector3.Cross(gravityDirection, Vector3.forward * -1f);

            Vector3 orbitalForce2 = Vector3.Cross(gravityDirection, Vector3.forward);

            Vector3 orbitalForce= Vector3.Cross(gravityDirection, upVector);


            //Engine power decreses with time
            float engineTime = Time.time - rocketLaunchTime;
         
            float engineProgress = (1f - Mathf.Clamp01(engineTime / engineWorkTime));

            float enginePower = Mathf.Lerp(0f, maxForce,engineProgress);

            rocketAccelerationForce = enginePower;

            //this helps to not fall on own planet when launch
            Vector3 gravityVector = StarSystemController.Instance.CalculateGravityForObject(transform.position, Mass, OwnerID);
            Vector3 clampedGravity = Mathf.Clamp(gravityVector.magnitude,0f, 200f)*gravityVector.normalized;

            //direction affected by gravity

            //rocket try to stay on orbit and not fall too fast
            Vector3 engineForce = (orbitalForce+(currentDirection*0.5f)).normalized*rocketAccelerationForce;

            //initial rocket force + gravity
            Vector3 resultVector = (engineForce) + clampedGravity;
            Vector3 moveStep = resultVector * Time.deltaTime * speedMultiplier;
           
            //rocket always looks forward
            transform.LookAt(transform.position + resultVector,-Vector3.forward);

            pos += moveStep;

            transform.position = pos;
           
        }

    }
}
