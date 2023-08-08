using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using PixelCrushers.DialogueSystem;

/*
   a) 저장하는 방법    
   1. 저장할 데이터가 존재
   2. 데이터를 제이슨으로 변환
   3. 제이슨을 외부에 저장
   
   b) 불러오는 방법
   1. 외부에 저장된 제이슨을 가져옴
   2. 제이슨을 데이터형태로 변환
   3. 불러온 데이터를 사용 
   
   */

[System.Serializable]
public class DialogueData
{
    public string save;
}

public class PlayerData
{
    public string name;
    public int age;
    public int level;
    public bool isDead;
    public string[] items;

}

public class SaveManager : Singleton<SaveManager>
{
    public DialogueData dialogueData = new DialogueData();

    private PlayerData _PlayerData = new PlayerData();
    
    private string path;
    private string filename = "save.json";

    private void Start()
    {
        //Start 함수에 안적으면 non-static 오류가 뜸
        path = Path.Combine(Application.dataPath, filename);
    }

    // '+'를 사용해도 되지만, 컴퓨터마다 +가 기능을 하지 않을 수 있으므로 안전하게 Combine을 사용함.
    

    [ContextMenu("Save")]
    public void SaveData() 
    {
        dialogueData.save = PersistentDataManager.GetSaveData();
        string data = JsonUtility.ToJson(dialogueData);
        File.WriteAllText(path,data);
            
        
        /*
        //true : 사람이 보기 편한 형태로 저장.
        string saveData = JsonUtility.ToJson(_PlayerData,true);
        File.WriteAllText(path, saveData);
        */
    }

    [ContextMenu("Load")]
    public void LoadData()
    {
        string data = File.ReadAllText(path);
        dialogueData = JsonUtility.FromJson<DialogueData>(data);
        PersistentDataManager.ApplySaveData(dialogueData.save);

        /*
        string loadData = File.ReadAllText(path);
        _PlayerData = JsonUtility.FromJson<PlayerData>(loadData);
        */
    }

    private void onEnable()
    {
        Lua.RegisterFunction("Save",this,SymbolExtensions.GetMethodInfo(()=>SaveData()));
        Lua.RegisterFunction("Load",this,SymbolExtensions.GetMethodInfo(()=>LoadData()));
    }
    
    private void onDisable()
    {
        Lua.UnregisterFunction("Save");
        Lua.UnregisterFunction("Load");
    }
        
}
