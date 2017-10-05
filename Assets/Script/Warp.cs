using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
    public Transform warpTarget;
    private PlayerMovment thePlayer;
    public bool isBoat;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovment>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        thePlayer.canMove = false;

        yield return StartCoroutine(sf.FadeToBlack());

        Debug.Log("Un objet est untré en colision.");
        PlayerMovment.isBoat = isBoat;
        collision.gameObject.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;
        Debug.Log("Un objet est untré en colision.");

        yield return StartCoroutine(sf.FadeToClear());
        thePlayer.canMove = true;
    }
}
