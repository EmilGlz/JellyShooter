using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    [SerializeField] float destroyTime; // 4 sec destroy time
    [SerializeField] float fadeTime; // 1 sec fade time
    [SerializeField] SpriteRenderer spriteRenderer;
    Color tempColor;
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(destroyTime-fadeTime); // wait for 4-1=3 sec
        LeanTween.value(1f,0f, fadeTime ).setOnComplete(()=> 
        {
            Destroy(gameObject);
        }).setOnUpdate(OnUpdate);
    }

    void OnUpdate(float alfa)
    {
        if (spriteRenderer != null)
        {
            tempColor = spriteRenderer.color;
            tempColor.a = alfa;
            spriteRenderer.color = tempColor;
        }
    }

}
