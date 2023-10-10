using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorRayCast : MonoBehaviour
{
    [SerializeField] private float doorActionRange = 1.8f;
    [SerializeField] private LayerMask doorMask;
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private PlayPlayerSound playSound;
    [SerializeField] private Canvas canNotOpenDoor;
    
    [HideInInspector] public bool canOpenDoor = true;
    private float picturealpha;
    private string interactButton = "E";


    private void Start()
    {
        picturealpha = textMeshPro.color.a;
    }

    private void Update()
    {
        RayCastOnDoor();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, doorActionRange, doorMask))
            {
                if (hit.transform.tag != "Key")
                {
                    CallAnimation(hit.transform);
                }
                else if (hit.collider.tag == "Key")
                {
                    hit.transform.gameObject.GetComponent<KeyTrigger>().enabled = true;
                    playSound.PlayThinkingSound();
                }

            }
        }
    }

    private void RayCastOnDoor()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, doorActionRange, doorMask))
        {
            textMeshPro.text = "Interact '" + interactButton + "'";
            if (picturealpha <= 0)
            {
                picturealpha -= (Time.deltaTime / 10);
                textMeshPro.color -= new Color(0, 0, 0, picturealpha);
            }
        }
        else
        {

            textMeshPro.color = new Color(255, 255, 255, 0);
            picturealpha = 0;
        }

    }

    private void CallAnimation(Transform doorTrans)
    {
        DoorController door = doorTrans.transform.GetComponentInParent<DoorController>();
        door.ToggleDoor();
    }


}
