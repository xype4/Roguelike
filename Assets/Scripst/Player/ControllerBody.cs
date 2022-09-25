using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBody : MonoBehaviour
{
    public float Run;
    public float Speed = 4f;
    public float SpeedRun = 8f;
    //[HideInInspector]
    private float Gravity = 30f;
    private bool run;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController ControlleR;
    private GameObject Camera;

    [HideInInspector]
    public float timeFactor = 1;

    
    void Start()
    {
        ControlleR = GetComponent<CharacterController>();
        Camera = gameObject.transform.GetChild(0).gameObject;
        Run = 0.2f;
    }

    void FixedUpdate()
    {
        run = false;

        if(ControlleR.isGrounded && (Input.GetKey("left shift")))
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            if (moveDir.z < 0) moveDir.z *= 0.6f; 
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= SpeedRun;
            moveDir.y -= 4f;
        
            run = true;
        }

        if(ControlleR.isGrounded && run == false)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= Speed;
        }

        moveDir.y -= Gravity*0.06f;

        run = false;
        ControlleR.Move (moveDir* 0.02f*timeFactor);

        transform.rotation = Quaternion.Euler(0f,Camera.GetComponent<ControllerHad>().MoveX,0f);         
    } 
}
