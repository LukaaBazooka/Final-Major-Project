using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
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

    GameObject draggedObject;
    GameObject lastItemSlot;

    public GameObject Player;
    bool IsInventoryOpeened;


    int SelectedHotbarSlot = 0;

    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (IsInventoryOpeened)
            {
                Reticle.SetActive(true);
                AudioSource.PlayClipAtPoint(Close, Player.transform.position, 0.3f);

                Cursor.lockState = CursorLockMode.Locked;   
                
                IsInventoryOpeened = false;
            }
            else
            {
                Reticle.SetActive(false);
                AudioSource.PlayClipAtPoint(Open, Player.transform.position, 0.3f);

                Cursor.lockState = CursorLockMode.None;

                IsInventoryOpeened = true;

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
                AudioSource.PlayClipAtPoint(Grab, Player.transform.position, 1f);

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
                draggedObject.transform.parent = slot.transform;
                AudioSource.PlayClipAtPoint(Place, Player.transform.position, 0.1f);


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

                AudioSource.PlayClipAtPoint(Drop, Player.transform.position, 0.5f);

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
            AudioSource.PlayClipAtPoint(Take, PickedItem.transform.position, 0.6f);

            emptyslot.GetComponent<InventorySlots>().heldItem= NewItem;


            Destroy(PickedItem);
        }

    }
}
