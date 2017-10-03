using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour {

    public int souls;

	// Use this for initialization
	void Start () {
        souls = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementSouls(int nombre)
    {
        souls += nombre;
    }
}
