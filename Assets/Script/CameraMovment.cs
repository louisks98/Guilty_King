using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour {

    public static bool inCombat;
    public static Transform target_Combat;
    public GameObject hero;
    public GameObject boat;
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        CameraMovment.inCombat = false;
	}
	
	// Update is called once per frame
	void Update () {
        cam.orthographicSize = (Screen.height / 100f) / 2f;

        if (inCombat)
        {
            transform.position = target_Combat.position + new Vector3(0,0,-10);
            Debug.Log("Encombat");
        }
        else
        {
            Debug.Log("Pas en combat");
            if (hero.activeInHierarchy == true)
            {
                if (hero.GetComponent<Transform>())
                {
                    transform.position = Vector3.Lerp(transform.position, hero.GetComponent<Transform>().position, 0.1f) + new Vector3(0, 0, -10);
                }
            }

            if (boat.activeInHierarchy == true)
            {
                if (boat.GetComponent<Transform>())
                {
                    transform.position = Vector3.Lerp(transform.position, boat.GetComponent<Transform>().position, 0.1f) + new Vector3(0, 0, -10);
                }
            }
        }
    }
}
