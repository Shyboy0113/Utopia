using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearPanel : MonoBehaviour
{
    public Fade fade;

    private int _gold;
    private int _exp;

    public TMP_Text tmp_Text;
    public TMP_Text ZKey_Text;

    private GameObject _enemy;

    [SerializeField]
    private bool _canSkip = false;

    private void Update()
    {
        if (_canSkip is true && Input.GetKeyDown(KeyCode.Z))
        {
            fade.FadeOut();
            ResetGameManager();
           
        }
    }

    public void ResetGameManager() {

        GameManager.Instance.enemyCode = null;
        GameManager.Instance.isBattle = false;

    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);

        ZKey_Text.gameObject.SetActive(true);
        _canSkip = true;
        
    }

    private void OnEnable()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (_enemy is not null)
        {
            EnemyState enemyState = _enemy.GetComponent<EnemyState>();

            if (enemyState is not null)
            {
                _gold = enemyState.gold;
                _exp = enemyState.exp;

                tmp_Text.text = $"골드 : {_gold} 원\n\n경험치 : {_exp} exp";

            }

        }

        StartCoroutine(Wait());

    }
}
