using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLightMan : MonoBehaviour
{
    public Transform transform;

    public void ChangeRotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        transform.position += new Vector3(1,-0.8f,0);
    }

}
