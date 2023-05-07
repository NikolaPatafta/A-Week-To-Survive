using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    //private Camera fpCamera;
    //private Transform damage;
    private float duration = 0.15f;

    public IEnumerator Shake()
    {
        if(Time.timeScale != 0)
        {
            Quaternion originalPos = transform.localRotation;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float z = Random.Range(3f, 5f);

                transform.localRotation = Quaternion.Euler(originalPos.x, originalPos.y, z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }   
    }
}
