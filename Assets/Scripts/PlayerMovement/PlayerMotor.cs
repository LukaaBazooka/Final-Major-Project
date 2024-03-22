using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 Playervelocity;
    private bool IsGrounded;
    public float speed = 3.5f;
    public float gravity = -68.8f;
    public float JumpHeight = 1f;
    private bool lerpcrouch = false;
    public bool crouching = false;
    private float crouchTimer;
    public bool sprinting = false;

    public AudioSource OutOfBreath;


    public Image StamBar;

    public float energy = 100f;
    private bool lerping;

    private bool cantsee;

    private bool outbreathe;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }



    IEnumerator breathe()
    {
        if (!outbreathe)
        {
            outbreathe = true;
            OutOfBreath.Play();
            yield return new WaitForSeconds(3f);
            outbreathe = false;
        }
    }


    public void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !crouching && !lerpcrouch)
        {
            if (energy > 90)
            {
                sprinting = true;
                speed = 4.5f;
            }
            if (energy <= 0)
            {
                sprinting = false;
                speed = 3.5f;
            }


        }
        //iof they lift off the  keyt, remove speed and such.
        if (Input.GetKeyUp(KeyCode.LeftShift) && !crouching && !lerpcrouch)
        {
            sprinting = false;
            speed = 3.5f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !crouching && !lerpcrouch)
        {
            if (energy > 0 && sprinting)
                energy -= 0.1f;
          

            if (energy <= 0 && sprinting)
            {
                sprinting = false;
                speed = 3.5f;
                energy = 0;
            }

        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (energy < 100)
            {
                if (crouching)
                    energy += 0.2f;
                else
                    energy += 0.1f;

            }

            if (energy > 100)
                energy = 100;



        }
    }


    IEnumerator FadeImage(float from, float to)
    {
        if (!lerping )
        {
            lerping= true;

            if (to == 1 && cantsee)
            {
                cantsee = false;
                // Use LeanTween to tween the alpha value
                LeanTween.value(gameObject, from, to, 0.1f)
                    .setEase(LeanTweenType.easeInOutQuad) // Specify easing type
                    .setOnUpdate((float alpha) =>
                    {
                        // Update the UI Image color with the new alpha value
                        Color newColor = StamBar.color;
                        newColor.a = alpha;
                        StamBar.color = newColor;
                    });
                yield return new WaitForSeconds(.1f);

            }
            else if(to == 0 && !cantsee)
            {
                cantsee = true;
                LeanTween.value(gameObject, from, to, 2f)
                 .setEase(LeanTweenType.easeInOutQuad) // Specify easing type
                 .setOnUpdate((float alpha) =>
                 {
                     // Update the UI Image color with the new alpha value
                     Color newColor = StamBar.color;
                     newColor.a = alpha;
                     StamBar.color = newColor;
                 });
                yield return new WaitForSeconds(2f);

            }

            lerping = false;

        }

    }

    void Update()
    {
        if (energy >= 100)
        {
            StartCoroutine(FadeImage(1, 0));
        }
        else
        {
            StartCoroutine(FadeImage(0, 1));

        }

        if (energy <10)
        {
            StartCoroutine(breathe());
        }
        Debug.Log(cantsee);

        StamBar.fillAmount = Mathf.Lerp(StamBar.fillAmount, energy / 100, 0.2f);

        IsGrounded = controller.isGrounded;
        if (lerpcrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpcrouch = false;
                crouchTimer = 0f;
            }
        }




        //sprint handler
        Sprint();


    }

    //recieve the inputs from our input manager script and apply them to our controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        Playervelocity.y += gravity * Time.deltaTime;
        if (IsGrounded && Playervelocity.y < 0)
            Playervelocity.y = -2f;

        controller.Move(Playervelocity * Time.deltaTime);

    }
    
    public void Jump()
    {
        Debug.Log("jumped");

        if (IsGrounded)
        {
            Debug.Log("jump");
            Playervelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        if (!sprinting)
        {
            crouching = !crouching;
            crouchTimer = 0;
            lerpcrouch = true;
        }
       
    }

 

}
