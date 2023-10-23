using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartTextTween : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        Color currColor = text.color;
        Color fadeColor = currColor;
        fadeColor.a = 0;
        LeanTween.value(gameObject, (Color val)=>{ text.color=val; }, fadeColor, currColor, 0.15f);

        // Debug.Log("Lean Tween");
        transform.localScale = new Vector3(1.85f*5f, 1.85f*5f, 0f);
        LeanTween.scale(gameObject, new Vector3(1.85f, 1.85f, 0f), 0.2f).setDelay(0.1f);
        //LeanTween.alpha(gameObject, 0f, 0.2f).setOnComplete(destroyText).setDelay(1f);

        currColor = text.color;
        fadeColor = currColor;
        fadeColor.a = 0;
        LeanTween.value(gameObject, (Color val)=>{ text.color=val; }, currColor, fadeColor, 0.2f).setOnComplete(destroyText).setDelay(1f);
    }

    void destroyText()
    {
        Destroy(gameObject);
    }
}
