using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
    public Transform warpTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Un objet est untré en colision.");
        collision.gameObject.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;
    }
}
