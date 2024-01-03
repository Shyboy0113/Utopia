using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//미친 실수들
//UI에서 Panel과 Image가 따로 있는 걸 보고, Panel도 클래스로 존재하는 줄 알음
//코루틴 건드는 주제에 StartCoroutine이 아니라, CFadeIn()이라고 그냥 호출함;

public class Fade : MonoBehaviour
{
    public Image image;

    public float fadeTime = 1.0f;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(CFadeIn());
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(CFadeOut());
    }

    IEnumerator CFadeIn()
    {
        float count = 0f;
        Color color = image.color;

        while (count <= fadeTime)
        {

            float alpha = Mathf.Lerp(1f, 0f, count / fadeTime);
            image.color = new Color(color.r, color.g, color.b, alpha);

            count += Time.deltaTime;

            yield return null;
        }
    }
    IEnumerator CFadeOut()
    {

        float count = 0f;
        Color color = image.color;

        while (count <= fadeTime)
        {

            float alpha = Mathf.Lerp(0f, 1f, count / fadeTime);
            image.color = new Color(color.r, color.g, color.b, alpha);

            count += Time.deltaTime;

            yield return null;
        }

    }

}
