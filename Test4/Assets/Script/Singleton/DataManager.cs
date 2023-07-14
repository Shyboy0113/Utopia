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
    //이름, 레벨, 코인, 착용중인 무기
    public string name;
    public int level;
    public int coin;
    public int item;

}

public class DataManager : Singleton<DataManager>
{
    private PlayerData _PlayerData = new PlayerData();
    
    string path;
    private string filename = "save";

    void Awake()
    {
        path =Application.persistentDataPath + "/";
        
    }
        void Start()
    {
        
    }

        public void SaveData()
        {
            string data = JsonUtility.ToJson(_PlayerData);
            File.WriteAllText(path + filename, data);

        }

        public void LoadData()
        {
            string data = File.ReadAllText(path + filename);
            _PlayerData = JsonUtility.FromJson<PlayerData>(data);


        }

        
}
