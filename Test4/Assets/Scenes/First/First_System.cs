using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_System : MonoBehaviour
{
    public GameObject hero;

    public Transform _heroTransform;

    public GameObject dummy;

    public Vector3[] cameraPos;

    private void Awake()
    {
        hero.SetActive(false);

    }

    private void Start()
    {
        StartCoroutine(AppearCharacter());
    }

    private void Update()
    {
        Debug.Log(_heroTransform.position);
        Camera.main.transform.position = cameraPos[(int)(_heroTransform.position.x +10) /20];
    }

    private IEnumerator AppearCharacter()
    {

        yield return new WaitForSeconds(4.95f);

        hero.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        SpriteRenderer renderer = hero.GetComponent<SpriteRenderer>();
        Color newColor = renderer.color;
        newColor.a = 1f;
        renderer.color = newColor;

        dummy.SetActive(false);

        yield return null;
    }


}
