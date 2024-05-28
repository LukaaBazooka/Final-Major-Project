using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using static UnityEditor.PlayerSettings;

public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject[] hotbarslots = new GameObject[4];

    [SerializeField] GameObject[] slots = new GameObject[16];

    [SerializeField] GameObject inventoryParent;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] Camera cam;
    [SerializeField] Transform HandParent;

    public AudioClip Take;
    public AudioClip Grab;

    public AudioClip Open;
    public AudioClip Close;

    public AudioClip Place;
    public AudioClip Drop;


    [SerializeField] GameObject Reticle;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject Objecthing;

    [SerializeField] Image Vignette;
    [SerializeField] Volume VOLUME;
    [SerializeField] DepthOfField Depth;
    [SerializeField] GameObject DropThing;

    GameObject draggedObject;
    GameObject lastItemSlot;

    public GameObject Player;
    bool IsInventoryOpeened;
    public bool IsPaused;


    [SerializeField] AudioSource MAIN_AUDIO_SOURCE;



    int SelectedHotbarSlot = 0;
    private bool beinglookedat = false;
    void Start()
    {
        VOLUME.profile.TryGet(out Depth);
        HotBarItemChange();
        Cursor.lockState = CursorLockMode.Locked;
    }
 

    void Update()
    {
        HotBarItemChange();
        CheckForHotbarInput();
        inventoryParent.SetActive(IsInventoryOpeened);

        if (draggedObject != null)
        {

            draggedObject.transform.position = Input.mousePosition;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Reticle.SetActive(true);
                DropThing.SetActive(true);

                Cursor.lockState = CursorLockMode.Locked;

                IsPaused = false;
                Depth.active = transform;

                LeanTween.alpha(Vignette.rectTransform, 0f, 0.5f).setEaseInOutQuad();

                LeanTween.moveLocal(Objecthing, new Vector3(0, 1000, 0), 1f).setEaseInOutCubic();

            }
            else
            {

                Reticle.SetActive(false);
                DropThing.SetActive(false);

                Cursor.lockState = CursorLockMode.None;

                IsPaused = true;
                Depth.active = false;

                LeanTween.alpha(Vignette.rectTransform, 1f, 0.5f).setEaseInOutQuad();

                LeanTween.moveLocal(Objecthing, new Vector3(0,20, 0), 1f).setEaseInOutCubic();

            }
        }


        if (Input.GetKeyDown(KeyCode.Tab) && !IsPaused)
        {
            //&& hotbarslots[SelectedHotbarSlot].GetComponent<InventorySlots>().heldItem
            if (IsInventoryOpeened)
            {
                
                Reticle.SetActive(true);
                MAIN_AUDIO_SOURCE.clip = Close;
                MAIN_AUDIO_SOURCE.Play();

                Cursor.lockState = CursorLockMode.Locked;   
                
                IsInventoryOpeened = false;
            }
            else
            {
                Reticle.SetActive(false);
                MAIN_AUDIO_SOURCE.clip = Open;
                MAIN_AUDIO_SOURCE.Play();
                Cursor.lockState = CursorLockMode.None;

                IsInventoryOpeened = true;

            }
        }
        Mouse mouse = Mouse.current;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (!IsInventoryOpeened)
            {
                for (int i = 0; i < HandParent.childCount; i++)
                {
                    if (HandParent.GetChild(i).GetComponent<ItemHand>().ItemScriptableObject == hotbarslots[SelectedHotbarSlot].GetComponent<InventorySlots>().heldItem.GetComponent<InventoryItem>().ItemScriptableObject)
                    {
                        if (HandParent.GetChild(i).gameObject.transform.Find("IsFile"))
                        {
                            if (!beinglookedat)
                            {
                                beinglookedat = true;
                                LeanTween.moveLocal(HandParent.GetChild(i).gameObject, new Vector3(-0.004998842f, 0.0390000391f, 0.716296446f),1f).setEaseInOutQuad();
                                LeanTween.rotateLocal(HandParent.GetChild(i).gameObject, new Vector3(0, 0, 0), 1f).setEaseInOutQuad();
                                //HandParent.GetChild(i).gameObject.transform.localPosition = Vector3.Lerp(HandParent.GetChild(i).gameObject.transform.localPosition, new Vector3(-0.004998842f, 0.0390000391f, 0.716296446f), 3f);
                                //HandParent.GetChild(i).gameObject.transform.localRotation = Quaternion.Lerp(HandParent.GetChild(i).gameObject.transform.rotation, new Quaternion(0, 0, 0, 1), 3f);
                            }
                            else
                            {
                                LeanTween.moveLocal(HandParent.GetChild(i).gameObject, new Vector3(0.481999993f, -0.397000015f, 0.683000028f), 1f).setEaseInOutQuad();
                                LeanTween.rotateLocal(HandParent.GetChild(i).gameObject, new Vector3(20.3688984f, 15.7115564f, 338.704407f), 1f).setEaseInOutQuad();

                               

                                beinglookedat = false;
                            }
                        }

                    }

                }                        

            }
        }

    }


    private void CheckForHotbarInput()
    {
        if (Input.GetKeyDown (KeyCode.Alpha1))
        {
            SelectedHotbarSlot = 0;
            HotBarItemChange();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedHotbarSlot = 1;
            HotBarItemChange();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedHotbarSlot = 2;
            HotBarItemChange();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectedHotbarSlot = 3;
            HotBarItemChange();

        }
    }

    private void HotBarItemChange()
    {
        for (int i = 0; i < HandParent.childCount; i++) 
        {
            HandParent.GetChild(i).gameObject.SetActive(false);
        }

        foreach(GameObject slot in hotbarslots)
        {
            Vector3 scale;

            if (slot == hotbarslots[SelectedHotbarSlot])    
            {
                scale = new Vector3(1f, 1f, 1f);
                if (slot.GetComponent<InventorySlots>().heldItem != null)
                {
                    for (int i = 0; i < HandParent.childCount; i++)
                    {
                        if (HandParent.GetChild(i).GetComponent<ItemHand>().ItemScriptableObject == hotbarslots[SelectedHotbarSlot].GetComponent<InventorySlots>().heldItem.GetComponent<InventoryItem>().ItemScriptableObject)
                        {
                            HandParent.GetChild(i).gameObject.SetActive(true);
                            //Debug.Log(HandParent.GetChild(i).gameObject);
                        }

                    }
                }

            } 
            else
            {
                scale = new Vector3(0.9f, 0.9f, 0.9f);

            }
            slot.transform.localScale = scale;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {

            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
         
            InventorySlots slot = clickedObject.transform.parent.GetComponent<InventorySlots>();
            Debug.Log(clickedObject.name);

            if (slot != null && slot.heldItem != null )
            {


                draggedObject = slot.heldItem;
                MAIN_AUDIO_SOURCE.clip = Grab;
                MAIN_AUDIO_SOURCE.Play();
                slot.heldItem = null;
                lastItemSlot = clickedObject.transform.parent.GetComponent<GameObject>();
            }
        }

     
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        if (draggedObject !=null && eventData.pointerCurrentRaycast.gameObject != null && eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            InventorySlots slot = clickedObject.transform.parent.GetComponent<InventorySlots>();

            if (slot != null && slot.heldItem == null )
            {
                slot.SetHeldItem(draggedObject);
                Debug.Log(draggedObject);
                draggedObject.transform.parent = slot.transform;
                MAIN_AUDIO_SOURCE.clip = Place;
                MAIN_AUDIO_SOURCE.Play();

            }
            else if (slot != null && slot.heldItem != null)
            {
                lastItemSlot.GetComponent<InventorySlots>().SetHeldItem(draggedObject);
            }
            //return to last slot
            else if (clickedObject.name != "DropItem")
            {
                lastItemSlot.GetComponent<InventorySlots>().SetHeldItem(draggedObject);

            }
            //drop
            else
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                Vector3 position = ray.GetPoint(3);
                GameObject Newitem = Instantiate(draggedObject.GetComponent<InventoryItem>().ItemScriptableObject.prefab, position, new Quaternion());
                Newitem.GetComponent<ItemPickable>().ItemScriptableObject = draggedObject.GetComponent<InventoryItem>().ItemScriptableObject;

                MAIN_AUDIO_SOURCE.clip = Drop;
                MAIN_AUDIO_SOURCE.Play();
                Destroy(draggedObject);

                lastItemSlot.GetComponent<InventorySlots>().heldItem = null;


            }
            HotBarItemChange();
            draggedObject = null;

        }
    }


    public void ItemPicked(GameObject PickedItem)
    {
        GameObject emptyslot = null;

        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlots slot = slots[i].GetComponent<InventorySlots>();

            if (slot.heldItem == null)
            {
                Debug.Log("Empty");
                emptyslot = slots[i];

                break;
            }
        }
           
        if (emptyslot != null)
        {
            GameObject NewItem = Instantiate(ItemPrefab);
            NewItem.GetComponent<InventoryItem>().ItemScriptableObject = PickedItem.GetComponent<ItemPickable>().ItemScriptableObject;
            NewItem.transform.SetParent(emptyslot.transform);
            NewItem.transform.localPosition= new Vector3(0, 0, 0);
            MAIN_AUDIO_SOURCE.clip = Take;
            MAIN_AUDIO_SOURCE.Play();
            emptyslot.GetComponent<InventorySlots>().heldItem= NewItem;


            Destroy(PickedItem);
        }

    }
}
