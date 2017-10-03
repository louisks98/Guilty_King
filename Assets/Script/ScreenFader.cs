using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {

    Animator Anim;
    bool isFading = false;

	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
	}

    public IEnumerator FadeToClear()
    {
        isFading = true;
        Anim.SetTrigger("FadeIn");

        while (isFading)
            yield return null;
    }

    public IEnumerator FadeToBlack()
    {
        isFading = true;
        Anim.SetTrigger("FadeOut");

        while (isFading)
            yield return null;
    }

    public void AnimationComplete()
    {
        isFading = false;
    }
}
