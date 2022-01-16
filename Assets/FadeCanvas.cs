using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn() {
        LeanTween.cancel(gameObject);
        LeanTween.alpha(gameObject, 1f, 0.3f);
    }

    public void FadeOut() {
        LeanTween.cancel(gameObject);
        LeanTween.alpha(gameObject, 0f, 0.3f).setOnComplete(FadeIn);
    }
}
