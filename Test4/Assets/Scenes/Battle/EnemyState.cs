using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyData enemyData;

    private SpriteRenderer _spriteRenderer;
    private float fadeDuration = 1f;

    private GameObject clearPanel;

    private IEnumerator Fade(float targetAlpha, float duration, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        float startAlpha = _spriteRenderer.color.a;
        float time = 0;

        while (time < duration)
        {
            // 시간에 따라 투명도를 선형적으로 변경
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        // 최종 투명도 설정
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, targetAlpha);

    }
    private IEnumerator ShowClearPanel()
    {
        yield return new WaitForSeconds(1f);

        if (clearPanel is not null)
        {
            clearPanel.SetActive(true);
        }
    }

    public string enemyName;
    public int currentPower;
    public int currentdefence;
    public int currentHp;

    public int gold;
    public int exp;

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

        gold = enemyData.Gold;
        exp = enemyData.Exp;

        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0f);

        StartCoroutine(Fade(1f, fadeDuration, 1f));
              
    }

    private void Update()
    {
        if(currentHp <= 0 && GameManager.Instance.isBattle is true)
        {
            currentHp = 0;
            isDead = true;

            SoundEffectManager.Instance.PlayEffect(1);

            GameManager.Instance.isBattle = false;

            StartCoroutine(Death());
        }
    }

    public void ChangeHp()
    {
        int damage = Mathf.Max(0,GameManager.Instance.Atk - currentdefence);
        currentHp -= damage;

    }

    public IEnumerator Death()
    {

        yield return StartCoroutine(Fade(0f, fadeDuration, 0f));

        yield return StartCoroutine(ShowClearPanel());

}



}
