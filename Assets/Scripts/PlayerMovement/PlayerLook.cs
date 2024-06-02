using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] GameObject KeyCard;


    [SerializeField] AudioSource cross;


    [SerializeField] AudioClip AcceptCard;
    [SerializeField] AudioClip DeclineCard;
    [SerializeField] AudioClip Decline;
    [SerializeField] AudioClip DoorOpen;
    [SerializeField] AudioClip DoorOpen2;

    [SerializeField] AudioClip DoorClose;
    [SerializeField] AudioClip DoorClose2;

    public GameObject Reticle;
    public GameObject ReticleGrab;
    public TMP_Text Card;

    public TMP_Text Files;

    [SerializeField] RawImage Panel;


    public Camera Camera;
    private float xRotation  = 0f;

    private Animator anim;

    public float xSensitivity = 15f;
    public float ySensitivity = 15f;


    private bool scancooldown;

    private bool hasfile1 = false;
    private bool hasfile2 = false;
    private bool hasfile3 = false;
        
    private bool hasfile4 = false;
    private bool hasfile5 = false;
    private bool hasfile6 = false;
    private bool hasfile7 = false;
    private bool hasfile8 = false;
    private bool hasfile9 = false;
    private bool hasfile10 = false;
    private float filecount = 0;                                
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
                    Debug.Log(item.name);
                    if (item.name == "Keycard")
                    {
                        Card.text = "<s>Find a Keycard (1/1)</s>";
                        cross.Play();
                    }
                    else if (item.name == "Paper1")
                    {
                        if (!hasfile1)
                        {
                            hasfile1 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper2")
                    {
                        if (!hasfile2)
                        {
                            hasfile2 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper3")
                    {
                        if (!hasfile3)
                        {
                            hasfile3 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper4")
                    {
                        if (!hasfile4)
                        {
                            hasfile4 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper5")
                    {
                        if (!hasfile5)
                        {
                            hasfile5 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper6")
                    {
                        if (!hasfile6)
                        {
                            hasfile6 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper7")
                    {
                        if (!hasfile7)
                        {
                            hasfile7 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper8")
                    {
                        if (!hasfile8)
                        {
                            hasfile8 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper9")
                    {
                        if (!hasfile9)
                        {
                            hasfile9 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }
                    else if (item.name == "Paper10")
                    {
                        if (!hasfile10)
                        {
                            hasfile10 = true;
                            filecount++;
                            Files.text = "Find missing documents (" + filecount + "/10)";
                        }
                    }

                    if (filecount >= 10)
                    {

                        Files.text = "<s>Find missing documents (" + filecount + "/10) </s>";
                        cross.Play();
                        StartCoroutine(endgame());

                    }
                }
                else if (Object.tag == "CardReader")
                {

                    CardReader cardreader = Object.GetComponent<CardReader>();
                    if (cardreader.NeedsCard)
                    {
                        if (KeyCard.activeSelf == true && !cardreader.scancooldown)
                        {
                            //Debug.Log("oh nice one.");



                            cardreader.scancooldown = true;

                            StartCoroutine(cardreader.coooldown(10f));
                            AudioSource.PlayClipAtPoint(AcceptCard, Object.transform.position, 2f);


                            StartCoroutine(cardreader.Colorswitch(Object, true));


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
                    else if (!cardreader.NeedsCard)
                    {
                        if (!cardreader.scancooldown)
                        {
                            //Debug.Log("oh nice one.");



                            cardreader.scancooldown = true;

                            StartCoroutine(cardreader.coooldown(10f));
                            AudioSource.PlayClipAtPoint(AcceptCard, Object.transform.position, 2f);


                            StartCoroutine(cardreader.Colorswitch(Object, true));


                            anim = Object.transform.parent.gameObject.GetComponent<Animator>();

                            if (Object.transform.parent.gameObject.GetComponent<DoorThng>())
                            {
                                DoorThng door = Object.transform.parent.gameObject.GetComponent<DoorThng>();
                                StartCoroutine(door.Animthingy());
                            }

                        }
                        else if (!cardreader.scancooldown || !cardreader.scancooldown)
                        {
                            cardreader.scancooldown = true;

                            StartCoroutine(cardreader.coooldown(2f));

                            AudioSource.PlayClipAtPoint(DeclineCard, Object.transform.position, 2f);
                            StartCoroutine(cardreader.Colorswitch(Object, false));

                        }

                    }
                }

                else
                {
                    Reticle.SetActive(true);
                    ReticleGrab.SetActive(false);
                }
            }
            else
            {
                Reticle.SetActive(true);
                ReticleGrab.SetActive(false);
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


    public IEnumerator endgame()
    {
        Panel.enabled = true;
        LeanTween.alpha(Panel.rectTransform, 1f, 1f).setEaseInOutQuad();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");


    }

}
