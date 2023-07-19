using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    private PlayerData _PlayerData = new PlayerData();
    private string filename = "save.json";

    [ContextMenu("Save")]
    public void SaveData()
        {
            // '+'를 사용해도 되지만, 컴퓨터마다 +가 기능을 하지 않을 수 있으므로 안전하게 Combine을 사용함.
            string path = Path.Combine(Application.dataPath, filename);
            
            //true : 사람이 보기 편한 형태로 저장.
            string saveData = JsonUtility.ToJson(_PlayerData,true);
            File.WriteAllText(path, saveData);

        }

    [ContextMenu("Load")]
    public void LoadData()
       {
           // '+'를 사용해도 되지만, 컴퓨터마다 +가 기능을 하지 않을 수 있으므로 안전하게 Combine을 사용함.
           string path = Path.Combine(Application.dataPath, filename);
           
           string loadData = File.ReadAllText(path);
           _PlayerData = JsonUtility.FromJson<PlayerData>(loadData);

       }

        
}
