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
    //Vidljiv ili nevidljiv cursor / crosshair
    private bool can_Unlock = true;
    
    [SerializeField]
    private float sensitivity = 5f; //mouse sens
    
    [SerializeField]
    private int smooth_Steps = 10; //smooth korak

    [SerializeField]
    private float roll_Speed = 3f;

    [SerializeField]
    private float smooth_Weight = 0.4f;

    [SerializeField]
    private float roll_Angle = 10f;

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
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            LookAround();
        }
    }

    //unlocking and locking Cursor (vidljivost ili nevidljivost kursora)
    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible= false;
            }
        }

    }
    void LookAround()
    {
        //varijable spremljene u tag holder skripti za lakse snalazenje
        //postavljamo varijablu Mouse_Y u Vector2 na mjesto X jer se rotira suprotno od smjera kretanja, isto tako i za Mouse_X
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
