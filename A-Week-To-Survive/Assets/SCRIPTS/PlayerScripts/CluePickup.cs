using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LayerMask clueMask;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject ClueGameObject;
    private string ClueTag_1 = "Clue_1";
    private string ClueTag_2 = "Clue_2";
    private string ClueTag_3 = "Clue_3";

    private float pickupRange = 10f;
    private float picturealpha;

    private void Start()
    {
        StartCoroutine(CheckForClues());
        picturealpha = textMeshPro.color.a;
        
    }

    private IEnumerator CheckForClues()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange, clueMask))
        {
            if (hit.transform.CompareTag(ClueTag_1))
            {
                if (picturealpha <= 0)
                {
                    picturealpha -= (Time.deltaTime / 10);
                    textMeshPro.color -= new Color(0, 0, 0, picturealpha);
                }
            }           
        }
        else
        {
            textMeshPro.color = new Color(255, 255, 255, 0);
            picturealpha = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            uiManager.isPaused = true; 
            ClueGameObject.gameObject.SetActive(true);
            uiManager.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CheckForClues());
    }


}
