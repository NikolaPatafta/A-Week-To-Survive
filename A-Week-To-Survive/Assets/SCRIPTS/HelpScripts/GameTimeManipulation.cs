using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManipulation : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.5f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
