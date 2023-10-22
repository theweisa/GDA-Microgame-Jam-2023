using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Lean Tween");
        transform.localScale = new Vector3(1.85f*5f, 1.85f*5f, 0f);
        LeanTween.scale(gameObject, new Vector3(1.85f, 1.85f, 0f), 0.2f).setDelay(0.1f);
        LeanTween.alpha(gameObject, 0f, 0.2f).setOnComplete(destroyText).setDelay(1f);
    }

    void destroyText()
    {
        Destroy(gameObject);
    }
}
