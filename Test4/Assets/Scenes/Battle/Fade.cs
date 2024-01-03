using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//��ģ �Ǽ���
//UI���� Panel�� Image�� ���� �ִ� �� ����, Panel�� Ŭ������ �����ϴ� �� ����
//�ڷ�ƾ �ǵ�� ������ StartCoroutine�� �ƴ϶�, CFadeIn()�̶�� �׳� ȣ����;

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
