using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject myobject;
    public bool activate;
    void Update()
    {
        myobject.SetActive(activate);
    }
}
