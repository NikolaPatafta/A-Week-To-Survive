using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CluePickup : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LayerMask clueMask;
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameObject[] ClueGameObject;
    [SerializeField] private string[] clueList;

    private ClueController clueController;
    private float pickupRange = 5f;
    private float picturealpha;
    private int clueListCounter = 0;

    private void Start()
    {
        StartCoroutine(CheckForClues());
        picturealpha = interactText.color.a;
    }

    private IEnumerator CheckForClues()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange, clueMask))
        {
            if (hit.transform.CompareTag(clueList[clueListCounter]))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {   
                    uiManager.isPaused = true;
                    ClueGameObject[clueListCounter].gameObject.SetActive(true);
                    clueController = FindObjectOfType<ClueController>();
                    clueController.clueCounter = clueListCounter;
                    clueListCounter++;
                }
                
                if (picturealpha <= 0)
                {
                    picturealpha -= (Time.deltaTime / 10);
                    interactText.color -= new Color(0, 0, 0, picturealpha);
                }
            }           
        }
        else
        {
            interactText.color = new Color(255, 255, 255, 0);
            picturealpha = 0;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(CheckForClues());
    }


}
