using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.PackageManager;
using UnityEngine;
using static Unity.VisualScripting.Member;


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

    

    public AudioSource MetalSteps;
    public AudioSource ConcreteSteps;


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

    private string Material;

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

        Vector3 rayOrigin = player.transform.position;
        Vector3 rayDirection = Vector3.down;


        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, 10f))
        {
            if (hitInfo.collider.tag == "Metal")
            {
                Material = "Metal";
            }
            else
            {
                Material = "other";

            }
        }

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
            if (Material == "Metal")
            {
                MetalSteps.volume = Mathf.Lerp(MetalSteps.volume, 0.2f, 0.5f);
                if (Motor.sprinting)
                {
                    MetalSteps.pitch = Mathf.Lerp(MetalSteps.pitch, 1.7f, 0.5f);
                    ConcreteSteps.volume = Mathf.Lerp(ConcreteSteps.volume, 0f, 0.5f);

                }
                else
                {
                    MetalSteps.pitch = Mathf.Lerp(MetalSteps.pitch, 1.2f, 0.5f);
                    ConcreteSteps.volume = Mathf.Lerp(ConcreteSteps.volume, 0f, 0.5f);

                }
            }
            else
            {
                ConcreteSteps.volume = Mathf.Lerp(ConcreteSteps.volume, 0.2f, 0.5f);
                if (Motor.sprinting)
                {
                    ConcreteSteps.pitch = Mathf.Lerp(ConcreteSteps.pitch, 1.25f, 0.5f);
                    MetalSteps.volume = Mathf.Lerp(MetalSteps.volume, 0f, 0.5f);

                }
                else
                {
                    ConcreteSteps.pitch = Mathf.Lerp(ConcreteSteps.pitch, 0.75f, 0.5f);
                    MetalSteps.volume = Mathf.Lerp(MetalSteps.volume, 0f, 0.5f);

                }
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
            if (Material == "Metal")
            {
                MetalSteps.volume = Mathf.Lerp(MetalSteps.volume, 0f, 0.5f);
                ConcreteSteps.volume = Mathf.Lerp(ConcreteSteps.volume, 0f, 0.5f);

            }
            else
            {
                ConcreteSteps.volume = Mathf.Lerp(ConcreteSteps.volume, 0f, 0.5f);
                MetalSteps.volume = Mathf.Lerp(MetalSteps.volume, 0f, 0.5f);

            }

            lerper();
        }

        
    }
}
