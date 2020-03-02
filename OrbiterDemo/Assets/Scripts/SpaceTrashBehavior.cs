using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTrashBehavior : SpaceObject {

    float rocketAccelerationForce;
    float maxForce;
    float speedMultiplier = 1f;
    float engineWorkTime = 5f;
    float rocketLaunchTime = 0f;
    Vector3 upVector;
    WeaponStats currentStats;

    bool isPause = false;

    float lifeTime = 15f;

    // Use this for initialization
    void Start () {

        Invoke("RemoveBullet", lifeTime);
		
	}

    public void Initialize(WeaponStats stats, Vector3 initDirection)
    {
        rocketLaunchTime = Time.time;
        currentStats = stats;

        rocketAccelerationForce = stats.InitialSpeed;
        maxForce = rocketAccelerationForce;
        Mass = stats.Mass;
        
        //get vector to Sun
        Vector3 sunPosition = SunBehavior.Instance.transform.position;

        Vector3 gravityDirection = sunPosition - transform.position;

        //  currentDirection = Vector3.Cross(gravityDirection, Vector3.forward * -1f) * rocketAccelerationForce;
        currentDirection = initDirection * rocketAccelerationForce;

        upVector = -Vector3.forward;
      

        isInitialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sun")
        {
            //destroy bullet
            RemoveBullet();
        }
        else if (other.tag == "Planet")
        {
            var planet = other.gameObject.GetComponent<PlanetBehavior>();
           // if (planet.ID != OwnerID)
            {
                //also damage planet
                // Debug.Log("Damage planet: "+other.gameObject.name);
                other.gameObject.GetComponent<PlanetBehavior>().MakeDamage(currentStats.Damage);
                RemoveBullet();
            }


        }
    }

    void RemoveBullet()
    {
        //Debug.Log("Bullet collided Sun");
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update () {

        if (isPause)
        {
            return;
        }

        MoveOnOrbit();
    }

    void MoveOnOrbit()
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

            Vector3 orbitalForce = Vector3.Cross(gravityDirection, upVector);


            //Engine power decreses with time
            float engineTime = Time.time - rocketLaunchTime;

            float engineProgress = (1f - Mathf.Clamp01(engineTime / engineWorkTime));

            float enginePower = Mathf.Lerp(0f, maxForce, engineProgress);

            rocketAccelerationForce = enginePower;

            //this helps to not fall on own planet when launch
            Vector3 gravityVector = StarSystemController.Instance.CalculateGravityForObject(transform.position, Mass, OwnerID);
            Vector3 clampedGravity = Mathf.Clamp(gravityVector.magnitude, 0f, 200f) * gravityVector.normalized;

            //direction affected by gravity

            //rocket try to stay on orbit and not fall too fast
            Vector3 engineForce = (orbitalForce + (currentDirection * 0.5f)).normalized * rocketAccelerationForce;

            //initial rocket force + gravity
            Vector3 resultVector = (engineForce) + clampedGravity;
            Vector3 moveStep = resultVector * Time.deltaTime * speedMultiplier;

            //rocket always looks forward
            transform.LookAt(transform.position + resultVector, -Vector3.forward);

            pos += moveStep;

            transform.position = pos;

        }

    }
}
