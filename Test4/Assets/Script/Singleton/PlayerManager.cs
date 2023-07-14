using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public float Melancholy_value;
    public float Good_value;
    public float Evil_value;

    public float Hp_value;
    public float Mp_value;
    
    void Awake()
    {
        Melancholy_value = Good_value = Evil_value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
