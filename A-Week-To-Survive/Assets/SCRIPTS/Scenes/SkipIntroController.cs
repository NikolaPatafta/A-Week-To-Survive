using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntroController : MonoBehaviour
{
    [SerializeField] GameObject SkipScene;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SkipScene.SetActive(true);
        }
    }

}
