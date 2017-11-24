using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
    public Transform warpTarget;
    private PlayerMovment hero;
    private PlayerMovment boat;
    public AudioClip Music;
    public bool isBoat;

    void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<PlayerMovment>();
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<PlayerMovment>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        SoundManager.instance.PlayDoor();
        if (Music != null)
            SoundManager.instance.PlayAmbient(Music);

        PlayerMovment.isTransition = true;
        hero.GetComponent<Animator>().enabled = false;
        boat.GetComponent<Animator>().enabled = false;

        yield return StartCoroutine(sf.FadeToBlack());
        
        collision.gameObject.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;
        PlayerMovment.isBoat = isBoat;
        
        yield return StartCoroutine(sf.FadeToClear());

        hero.GetComponent<Animator>().enabled = true;
        boat.GetComponent<Animator>().enabled = true;
        PlayerMovment.isTransition = false;

        //Debug.Log("Un objet est entré en colision.");
    }
}
