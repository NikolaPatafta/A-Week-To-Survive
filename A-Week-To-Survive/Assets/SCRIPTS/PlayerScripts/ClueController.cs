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
    [SerializeField] private UIManager uiManager;

    private PlayerMovement playerMovement;
    private MouseLook mouseLook;
    private float pictureAlpha1 = 0f;
    private float pictureAlpha2 = 0f;
    public int clueCounter;

    private void Start()
    {
        pictureAlpha1 = Background.color.a;
        pictureAlpha2 = textMeshPro.color.a;
    }

    private void Update()
    {
        if (pictureAlpha1 <= 0)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            mouseLook = FindObjectOfType<MouseLook>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            if(mouseLook != null)
            {
                mouseLook.enabled = false;
            }
            pictureAlpha1 -= (Time.deltaTime / 30);
            Background.color -= new Color(0, 0, 0, pictureAlpha1);
            if (pictureAlpha1 <= - 0.08f) 
            {
                pictureAlpha2 -= (Time.deltaTime / 40);
                textMeshPro.color -= new Color(0, 0, 0, pictureAlpha2);
                if(pictureAlpha2 <= -0.15f)
                {
                    uiManager.gameObject.SetActive(true);
                    SkipText();
                }
            }
        }
        
    }

    private void SkipText()
    {
        skipText.color = new Color(0, 0, 0, 1);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            playerMovement.enabled = true;
            mouseLook.enabled = true;
            CluesCanvas.gameObject.SetActive(false);
            uiManager.isPaused = false;
        }
    }


}
