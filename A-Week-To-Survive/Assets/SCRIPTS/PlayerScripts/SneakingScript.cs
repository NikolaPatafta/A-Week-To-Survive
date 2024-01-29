using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SneakingScript : MonoBehaviour
{
    private Image image;
    private float targetAlpha;
    private float fadeDuration = 2f; // You can adjust the duration

    private void Start()
    {
        image = GetComponent<Image>();
        targetAlpha = image.color.a; // Set the target alpha to the initial alpha
    }

    private void Update()
    {
        float currentAlpha = image.color.a;
        float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime / fadeDuration);

        // Update the image color with the new alpha
        image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
    }

    public void OpenPicture()
    {
        targetAlpha = 1f; // Set the target alpha to fully visible
    }

    public void ClosePicture()
    {
        targetAlpha = 0f; // Set the target alpha to fully transparent
    }
}
