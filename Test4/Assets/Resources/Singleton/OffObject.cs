using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class OffObject : MonoBehaviour
{
    private string check;
    private int offCnt = 0;
    
    void Update()
    {
        if(check == "success" || offCnt >= 2 ) return;
        
        check = DialogueLua.GetQuestField("왕 옆에 촛불 끄기", "State").AsString;
        offCnt = DialogueLua.GetVariable("off").asInt;
        
        Debug.Log("check : " + check);
        Debug.Log("offCnt : " + offCnt);
        
        if (check == "success" || offCnt >= 2)
        {
            gameObject.SetActive(false);
        }
    }
}
