using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fader;

    private bool busy;

    public void FadeToBlack(float duration)
    {
        if (busy)
        {
            return;
        }
        StartCoroutine(FadeToBlackCount(duration));
    }
    
    public void FadeFromBlack(float duration)
    {
        if (busy)
        {
            return;
        }
        StartCoroutine(FadeFromBlackCount(duration));
    }
    private IEnumerator FadeToBlackCount(float duration)
    {
        busy = true;
        while(fader.color.a < 1)
        {
            fader.color = new Color(0,0,0, fader.color.a + (Time.deltaTime/duration));
            yield return null;
        }
        
        fader.color = new Color(0,0,0,1);
        busy = false;
        yield return null;
    }
    private IEnumerator FadeFromBlackCount(float duration)
    {
        busy = true;
        while(fader.color.a > 0)
        {
            fader.color = new Color(0,0,0, fader.color.a - (Time.deltaTime/duration));
            yield return null;
        }
        fader.color = new Color(0,0,0,0);
        busy = false;
        yield return null;
    }
}
