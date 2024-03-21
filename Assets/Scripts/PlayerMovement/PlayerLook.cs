using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.XR;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] GameObject KeyCard;



    [SerializeField] AudioClip AcceptCard;
    [SerializeField] AudioClip DeclineCard;
    [SerializeField] AudioClip Decline;
    [SerializeField] AudioClip DoorOpen;
    [SerializeField] AudioClip DoorOpen2;

    [SerializeField] AudioClip DoorClose;
    [SerializeField] AudioClip DoorClose2;

    public GameObject Reticle;
    public GameObject ReticleGrab;


    public Camera Camera;
    private float xRotation  = 0f;

    private Animator anim;

    public float xSensitivity = 15f;
    public float ySensitivity = 15f;


    private bool scancooldown;

                                         
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calculate cam rotastion looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //apply this to our cam
        Camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //rotate player to left and right when looking.
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))

        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 1.5f))
            {
                ItemPickable item = hitinfo.collider.gameObject.GetComponent<ItemPickable>();
                GameObject Object = hitinfo.collider.gameObject;

                if (item != null)
                {
                    Reticle.SetActive(false);
                    ReticleGrab.SetActive(true);
                    Debug.Log("Looking at Item");
                    inventoryManager.ItemPicked(hitinfo.collider.gameObject);
                }
                else if (Object.tag == "CardReader")
                {

                    CardReader cardreader = Object.GetComponent<CardReader>();

                    if (KeyCard.activeSelf == true && !cardreader.scancooldown)
                    {
                        //Debug.Log("oh nice one.");
                        cardreader.scancooldown = true;

                        StartCoroutine(cardreader.coooldown(10f));
                        AudioSource.PlayClipAtPoint(AcceptCard, Object.transform.position, 2f);


                        StartCoroutine(cardreader.Colorswitch(Object,true));


                        anim = Object.transform.parent.gameObject.GetComponent<Animator>();

                        if (Object.transform.parent.gameObject.GetComponent<DoorThng>())
                        {
                            DoorThng door = Object.transform.parent.gameObject.GetComponent<DoorThng>();
                            StartCoroutine(door.Animthingy());
                        }
                      
                    }
                    else if (!KeyCard.activeSelf && !cardreader.scancooldown || !cardreader.scancooldown) 
                    {
                        cardreader.scancooldown = true;

                        StartCoroutine(cardreader.coooldown(2f));

                        AudioSource.PlayClipAtPoint(DeclineCard, Object.transform.position, 2f);
                        StartCoroutine(cardreader.Colorswitch(Object, false));

                    }
                }

                else
                {
                    Reticle.SetActive(true);
                    ReticleGrab.SetActive(false);
                }
            }
        }


        Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo2;

        if (Physics.Raycast(ray2, out hitinfo2, 1.5f))
        {
            ItemPickable item = hitinfo2.collider.gameObject.GetComponent<ItemPickable>();



            if (item != null)
            {
                Reticle.SetActive(false);
                ReticleGrab.SetActive(true);
                Reticle.SetActive(false);

            }
         
            else
            {
                Reticle.SetActive(true);
                ReticleGrab.SetActive(false);
            }
        }
    }


}
