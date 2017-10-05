using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
    public Transform warpTarget;
    public bool isBoat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovment.isBoat = isBoat;
        collision.gameObject.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;
        Debug.Log("Un objet est untré en colision.");
    }
}
