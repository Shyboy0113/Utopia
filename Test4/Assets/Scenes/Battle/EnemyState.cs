using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyData enemyData;

    private SpriteRenderer _spriteRenderer;
    private float fadeDuration = 1f;

    private GameObject clearPanel;

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = _spriteRenderer.color.a;
        float time = 0;

        while (time < duration)
        {
            // �ð��� ���� ������ ���������� ����
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        // ���� ���� ����
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, targetAlpha);

        yield return new WaitForSeconds(1f);

        if(clearPanel is not null)
        {
            clearPanel.SetActive(true);
        }

    }

    public string enemyName;
    public int currentPower;
    public int currentdefence;
    public int currentHp;

    public bool isDead = false;

    private void Awake()
    {
        clearPanel = GameObject.Find("ClearPanel");

        if (clearPanel is not null)
        {
            clearPanel.SetActive(false);
        }

        enemyName = enemyData.Name;
        currentPower = enemyData.Power;
        currentdefence = enemyData.Defence;
        currentHp = enemyData.MaxHp;

        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if(currentHp <= 0 && GameManager.Instance.isBattle is true)
        {
            currentHp = 0;
            isDead = true;
            Death();
        }
    }

    public void ChangeHp()
    {
        int damage = Mathf.Max(0,GameManager.Instance.Atk - currentdefence);
        currentHp -= damage;

    }

    public void Death()
    {
        SoundEffectManager.Instance.PlayEffect(1);

        StartCoroutine(Fade(0f, fadeDuration));

        GameManager.Instance.isBattle = false;

}



}
