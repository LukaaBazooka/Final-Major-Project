using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(PositionFollower))]
public class ViewBobbing : MonoBehaviour
{
    private CharacterController controller;
    private PlayerMotor Motor;
    private bool IsGrounded;

    public float EffectIntensity;
    public float EffectIntesnityX;
    public float EffectSpeed;
    private float EffectSpeed2;

    public GameObject player;

    public AudioSource Footstep;

    private PositionFollower FollowerInstance;
    private Vector3 OriginsOffset;
    private float sintime;


    bool lerping;

    IEnumerator lerper()
    {
        if (!lerping)
        {
            lerping = true;
            sintime = Mathf.Lerp(sintime, 0f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            lerping = false;

        }
    }


    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        Motor = player.GetComponent<PlayerMotor>();

        FollowerInstance = GetComponent<PositionFollower>();
        OriginsOffset = FollowerInstance.Offset;
    }

    void Update()
    {
        IsGrounded = controller.isGrounded;


    }
    void FixedUpdate()
    {
        Vector3 InputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
        if (Motor.sprinting)
        {
            EffectSpeed2 = EffectSpeed * 1.5f;
        }
        else
        {
            EffectSpeed2 = EffectSpeed;
        }
    

        if (InputVector.magnitude > 0f && IsGrounded)
        {
            Footstep.volume = Mathf.Lerp(Footstep.volume, 1f, 0.5f);
            if (Motor.sprinting)
            {
                Footstep.pitch = Mathf.Lerp(Footstep.pitch, 1.5f, 0.5f);
            }
            else
            {
                Footstep.pitch = Mathf.Lerp(Footstep.pitch, 1f, 0.5f);
            }


            sintime += Time.deltaTime + EffectSpeed2;

            float SinAmountY = -Mathf.Abs(EffectIntensity * Mathf.Sin(sintime));
            Vector3 sinAmountX = FollowerInstance.transform.right * EffectIntensity * Mathf.Cos(sintime) * EffectIntesnityX;

            FollowerInstance.Offset = new Vector3
            {
                x = OriginsOffset.x,
                y = OriginsOffset.y + SinAmountY,
                z = OriginsOffset.z,
            };

            FollowerInstance.Offset += sinAmountX;


        }
        else
        {
            Footstep.volume = Mathf.Lerp(Footstep.volume, 0f, 0.5f);
            lerper();
        }

        
    }
}
