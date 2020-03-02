using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

    public static Storage Instance;

    public List<WeaponStats> RocketTypes = new List<WeaponStats>();
    public List<GameObject> RocketPrefabs = new List<GameObject>();

    public GameObject SpaceTrashPrefab;

    //for faster object access
    private Dictionary<int, GameObject> RocketDictionary=new Dictionary<int, GameObject>();

    private List<GameObject> PoolList = new List<GameObject>();

    private bool isPause = false;

    void Awake()
    {
        Instance = this;
        LoadRockets();
        LoadModels();
    }

    private void Start()
    {
     
    }

    public void OnPause(bool value)
    {
        isPause = value;
    }

    public void CleanPool()
    {
        Debug.Log("Clean objects pool");

        int objCount = PoolList.Count;
        for(int i=0;i<objCount;i++)
        {
            GameObject obj= PoolList[0];
            PoolList.RemoveAt(0);
            if (obj!=null)
            {
                Destroy(obj);
            }
        }
    }

    public void AddToPool(GameObject obj)
    {
        PoolList.Add(obj);
    }

    void LoadRockets()
    {
        WeaponStats weapon = new WeaponStats();
        weapon.ID = 1;
        weapon.CooldownTime = 1f;
        weapon.InitialSpeed = 50f;
        weapon.Mass = 1f;
        weapon.Type = WeaponType.Fast;
        weapon.Damage = 15f;

        RocketTypes.Add(weapon);


        weapon = new WeaponStats();
        weapon.ID = 2;
        weapon.CooldownTime = 1.25f;
        weapon.InitialSpeed = 50f;
        weapon.Mass = 1.3f;
        weapon.Damage = 20f;
        weapon.Type = WeaponType.Heavy;

        RocketTypes.Add(weapon);

        //This type of weapon creates space trash after collision
        weapon = new WeaponStats();
        weapon.ID = 3;
        weapon.CooldownTime = 1.5f;
        weapon.InitialSpeed = 50f;
        weapon.Mass = 0.6f;
        weapon.Type = WeaponType.Double;
        weapon.Damage = 13f;

        RocketTypes.Add(weapon);

        weapon = new WeaponStats();
        weapon.ID = 4;
        weapon.CooldownTime = 0.1f;
        weapon.InitialSpeed = 40f;
        weapon.Mass = 0.1f;
        weapon.Type = WeaponType.SpaceTrash;
        weapon.Damage = 4f;

        RocketTypes.Add(weapon);

        Debug.Log("Loaded weapon types:" + RocketTypes.Count);

    }

    void LoadModels()
    {
        foreach (GameObject obj in RocketPrefabs)
        {
            int id = obj.GetComponent<SpaceObject>().TypeID;

            if (RocketDictionary.ContainsKey(id) == false)
            {
                RocketDictionary.Add(id, obj);

                Debug.Log("Added rocket type:" + id);
            }
        }
    }

    public WeaponStats GetWeaponData(int ID)
    {
        foreach(WeaponStats stats in RocketTypes)
        {
            if(stats.ID==ID)
            {
                return stats;
            }
        }

        return null;
    }

    public GameObject GetWeaponPrefab(int ID)
    {
        if(RocketDictionary.ContainsKey(ID))
        {
            return RocketDictionary[ID];
        }

        return null;
    }
 
    public GameObject GetSpaceTrashPrefab()
    {
        return SpaceTrashPrefab;
    }
}
