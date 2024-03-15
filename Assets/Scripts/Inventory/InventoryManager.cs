using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;

public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] GameObject[] slots = new GameObject[16];
    [SerializeField] GameObject inventoryParent;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] Camera cam;
    [SerializeField] AudioClip Take;
    [SerializeField] GameObject Reticle;

    GameObject draggedObject;
    GameObject lastItemSlot;

    bool IsInventoryOpeened;
    void Update()
    {
        inventoryParent.SetActive(IsInventoryOpeened);
        Reticle.SetActive(!IsInventoryOpeened);

        if (draggedObject != null)
        {

            draggedObject.transform.position = Input.mousePosition;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsInventoryOpeened)
            {
                Cursor.lockState = CursorLockMode.Locked;   
                
                IsInventoryOpeened = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;

                IsInventoryOpeened = true;

            }
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
                Destroy(draggedObject);

                lastItemSlot.GetComponent<InventorySlots>().heldItem = null;

            }

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
            AudioSource.PlayClipAtPoint(Take, PickedItem.transform.position, 2f);

            emptyslot.GetComponent<InventorySlots>().heldItem= NewItem;


            Destroy(PickedItem);
        }

    }
}
