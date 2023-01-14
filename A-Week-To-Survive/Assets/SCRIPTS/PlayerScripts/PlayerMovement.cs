using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController character_Controller;

    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jumpForce = 10f;
    private float verticalVelocity;

    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveThePlayer();    
    }

    void MoveThePlayer()
    {
        //Skripta TagHolder -> Klasa Axis
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        //Transform iz lokalne var u world space
        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime; //deltaTime - vremenski razmak izmedju framova

        ApplyGravity();
        character_Controller.Move(move_Direction);

    }

    void ApplyGravity()
    {
        //vertical velocity = brzina vertikalne kretnje
        verticalVelocity -= gravity * Time.deltaTime;
        PlayerJump();

        move_Direction.y = verticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if(character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }

    }
}
