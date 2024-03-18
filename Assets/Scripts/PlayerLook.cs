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

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

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

    public IEnumerator Colorswitch(GameObject Object, bool accepted)
    {
        

        if (accepted)
        {
            Object.transform.GetChild(3).gameObject.SetActive(true);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(true);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
        }
        yield break;


    }
    public IEnumerator Animthingy(GameObject Object)
    {

        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(DoorOpen, Object.transform.parent.gameObject.transform.position, 2f);

        anim.SetBool("IsOpen", true);
        anim.SetBool("Activated", true);
        AudioSource.PlayClipAtPoint(DoorClose, Object.transform.parent.gameObject.transform.position, 2f);

        anim.Play("DoorOpen");
        anim.SetBool("Activated", false);
        yield return new WaitForSeconds(3f);

        AudioSource.PlayClipAtPoint(DoorOpen2, Object.transform.parent.gameObject.transform.position, 2f);

        yield return new WaitForSeconds(3f);
        AudioSource.PlayClipAtPoint(DoorClose, Object.transform.parent.gameObject.transform.position, 2f);

        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(DoorClose2, Object.transform.parent.gameObject.transform.position, 2f);
        
        yield return new WaitForSeconds(1f);
        Debug.Log("closing");
        anim.Play("DoorClose");
        AudioSource.PlayClipAtPoint(DoorOpen, Object.transform.parent.gameObject.transform.position, 2f);
        yield break;


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
                
                    if (KeyCard.activeSelf == true)
                    {
                        Debug.Log("oh nice one.");
                        AudioSource.PlayClipAtPoint(AcceptCard, Object.transform.position, 2f);


                        StartCoroutine(Colorswitch(Object,true));


                        anim = Object.transform.parent.gameObject.GetComponent<Animator>();
                        StartCoroutine(Animthingy(Object));
                      
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(DeclineCard, Object.transform.position, 2f);
                        StartCoroutine(Colorswitch(Object, false));

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

        if (Physics.Raycast(ray2, out hitinfo2, 2f))
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
