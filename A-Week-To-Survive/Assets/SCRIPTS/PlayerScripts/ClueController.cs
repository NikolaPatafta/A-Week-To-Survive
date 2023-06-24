using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClueController : MonoBehaviour
{
    [SerializeField] Image Background;
    [SerializeField] GameObject CluesCanvas;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] TextMeshProUGUI skipText;

    private UIManager uiManager;
    private float pictureAlpha1 = 0f;
    private float pictureAlpha2 = 0f;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<UIManager>();
        pictureAlpha1 = Background.color.a;
        pictureAlpha2 = textMeshPro.color.a;
    }

    private void Update()
    {
        Debug.Log("PictureAl " + pictureAlpha1);
        if (pictureAlpha1 <= 0)
        {
            pictureAlpha1 -= (Time.deltaTime / 30);
            Background.color -= new Color(0, 0, 0, pictureAlpha1);
            if (pictureAlpha1 <= - 0.08f) 
            {
                pictureAlpha2 -= (Time.deltaTime / 40);
                textMeshPro.color -= new Color(0, 0, 0, pictureAlpha2);
                if(pictureAlpha2 <= -0.15f)
                {
                    SkipText();
                }
            }
        }
    }

    private void SkipText()
    {
        skipText.color = new Color(0, 0, 0, 1);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CluesCanvas.gameObject.SetActive(false);
            uiManager.isPaused = false;
        }
    }


}
