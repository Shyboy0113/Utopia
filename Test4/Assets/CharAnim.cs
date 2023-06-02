using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnim : MonoBehaviour
{
    private Animator charAnim;

    void Start()
    {
        charAnim = GetComponent<Animator>();
    }

    void Update()
    {
        characterMotion();
    }

    void characterMotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) charAnim.SetTrigger("Trig_1");
        else if (Input.GetKeyDown(KeyCode.Alpha2)) charAnim.SetTrigger("Trig_2");
        else if (Input.GetKeyDown(KeyCode.Alpha3)) charAnim.SetTrigger("Trig_3");
        else if (Input.GetKeyDown(KeyCode.Alpha4)) charAnim.SetTrigger("Trig_4");
        else if (Input.GetKeyDown(KeyCode.Alpha5)) charAnim.SetTrigger("Trig_5");
        else if (Input.GetKeyDown(KeyCode.Alpha6)) charAnim.SetTrigger("Trig_6");
        else if (Input.GetKeyDown(KeyCode.Alpha7)) charAnim.SetTrigger("Trig_7");
        else if (Input.GetKeyDown(KeyCode.Alpha8)) charAnim.SetTrigger("Trig_8");
        else if (Input.GetKeyDown(KeyCode.Alpha9)) charAnim.SetTrigger("Trig_9");
    }


}
