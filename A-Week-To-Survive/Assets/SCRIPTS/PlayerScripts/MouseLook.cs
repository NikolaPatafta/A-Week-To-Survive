using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MouseLook : MonoBehaviour
{
    [SerializeField]    
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;
    
    [SerializeField]
    private float sensitivity = 5f; 
    

    [SerializeField]
    private Vector2 default_look_Limits = new Vector2(-70f, 80f);
    private Vector2 look_Angles;
    private Vector2 current_Mouse_Look;
    private Vector2 smooth_Move;

    private float current_Roll_Angle;
    private int last_look_Frame;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            LookAround();
        }
    }

    void LookAround()
    {
        current_Mouse_Look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X)); 

        look_Angles.x += current_Mouse_Look.x * sensitivity * (invert ? 1f : -1f);   //if invert normalni look na mouse, if false onda obrnuto (if,else)
        look_Angles.y += current_Mouse_Look.y * sensitivity;

        //Clamp ne dopusta varijabli da ide ispod ili iznad zadanog limita
        look_Angles.x = Mathf.Clamp(look_Angles.x, default_look_Limits.x, default_look_Limits.y);

        //Drunk stats - WIP (X OR Z AXIS) 
        //current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxis(MouseAxis.MOUSE_X) * roll_Angle, Time.deltaTime * roll_Speed);
        //**** u lookRoot Euler umjesto zadnjeg 0f ide current_roll_angle

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

    }
}
