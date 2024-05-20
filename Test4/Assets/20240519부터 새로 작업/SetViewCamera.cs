using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetViewCamera : MonoBehaviour
{

    public GameObject[] vcam;

    public int vcamNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.transform.CompareTag("Player"))
        {
            vcam[vcamNumber].GetComponent<CinemachineVirtualCamera>().Priority +=1;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            vcam[vcamNumber].GetComponent<CinemachineVirtualCamera>().Priority -=1;
        }
    }


}
