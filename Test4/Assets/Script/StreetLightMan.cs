using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PixelCrushers.DialogueSystem;

public class StreetLightMan : MonoBehaviour
{
    public Transform transform;

    public TMP_Text _text;

    private void Update()
    {
        _text.text = DialogueLua.GetVariable("Evil Gage").asString;
    }

    public void ChangeRotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        transform.position += new Vector3(1,-0.8f,0);
    }
    
    

}
