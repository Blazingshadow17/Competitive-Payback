using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;
    //[Header("Canvas Used")]
    private Canvas billboard;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        billboard = GetComponent<Canvas>();
        billboard.sortingLayerName = "Foreground"; //makes everything on billboard appear on foreground layer
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up); //makes it face camera all times
    }
}
