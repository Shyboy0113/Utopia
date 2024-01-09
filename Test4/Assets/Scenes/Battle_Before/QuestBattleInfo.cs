using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestBattleInfo : MonoBehaviour
{

    public string storyCode;

    public string enemyCode;

    public Image fadePanel;
    private Fade _fade;

    private void Awake()
    {        
        _fade = fadePanel.GetComponent<Fade>();
    }

    public void BattleStart()
    {
        
        if(_fade is not null)
        {
            _fade.FadeOut();
        }

        StartCoroutine(Wait());
        
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        
        
        GameManager.Instance.storyCode = storyCode;
        GameManager.Instance.enemyCode = enemyCode;
        
        SceneManager.LoadScene("BattleScene");

        yield return null;


    }


}
