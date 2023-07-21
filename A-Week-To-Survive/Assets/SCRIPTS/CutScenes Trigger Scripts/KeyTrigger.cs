using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public GameObject garageDoor;

    private void OnEnable()
    {
        garageDoor.SetActive(false);
        gameObject.SetActive(false);    
    }
}
