using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial_FirstSystem : MonoBehaviour
{
    public GameObject hero;

    public Transform _heroTransform;

    //dummy's variable
    public GameObject dummy;
    public Animator dummyAnimator;

    public Vector3[] cameraPos;
    public float waitingTime;
    
    //UI's variable
    public Fade fade;
    public TMP_Text directionText;
    
    private void Awake()
    {
        hero.SetActive(false);
        directionText.gameObject.SetActive(false);
    }

    private void Start()
    {
        fade.fadeTime = waitingTime;

        StartCoroutine(AppearCharacter());
    }

    private void Update()
    {
        Camera.main.transform.position = cameraPos[(int)(_heroTransform.position.x +10) /20];
    }

    private IEnumerator AppearCharacter()
    {
        yield return new WaitForSeconds(waitingTime);

        dummyAnimator.SetTrigger("WakeUp");

        yield return new WaitForSeconds(3.0f);

        hero.SetActive(true);

        SpriteRenderer renderer = hero.GetComponent<SpriteRenderer>();
        Color newColor = renderer.color;
        newColor.a = 1f;
        renderer.color = newColor;

        dummy.SetActive(false);
        directionText.gameObject.SetActive(true);

        yield return null;
    }


}
