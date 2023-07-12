using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public int Melancholy_value;
    public int Good_value;
    public int Evil_value;

    public int Hp_value;
    public int Mp_value;
    
    void Awake()
    {
        Melancholy_value = Good_value = Evil_value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
