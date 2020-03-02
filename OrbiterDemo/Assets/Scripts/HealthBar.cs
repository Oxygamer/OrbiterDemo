using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    SpriteRenderer render;
    PlanetBehavior currentPlanet;
    Vector3 indicatorOffset = new Vector3(0f, 10f, -10f);

	// Use this for initialization
	void Start () {

        render = GetComponent<SpriteRenderer>();
		
	}

    public void AttachPlanet(PlanetBehavior planet)
    {
        currentPlanet = planet;
    }

    public float UpdateFill()
    {
        if(currentPlanet!=null)
        {
            float fillProgress= currentPlanet.GetHealthPercents();
            SetFill(fillProgress);
            return fillProgress;
        }
        else
        {
            return 0f;
        }
    }

    void Update()
    {
        if (currentPlanet != null)
        {
            transform.position = currentPlanet.transform.position + indicatorOffset;
        }

    }

    public void SetFill(float value)
    {
        if(render!=null)
        {
            render.material.SetFloat("_Fill", value);
            Color c = Color.Lerp(Color.red, Color.green, value);
            render.material.SetColor("_Color", c);
        }

    }
}
