using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour {

    public int souls;
    public Text soulsRemaining;

	// Use this for initialization
	void Start () {
        souls = 0;
        soulsRemaining.text = souls.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        soulsRemaining.text = souls.ToString();
    }

    public void IncrementSouls(int nombre)
    {
        souls += nombre;
    }
}
