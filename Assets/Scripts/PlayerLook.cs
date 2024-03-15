using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] InventoryManager inventoryManager;


    public GameObject Reticle;


    public Camera Camera;
    private float xRotation  = 0f;

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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))

        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 3))
            {
                ItemPickable item = hitinfo.collider.gameObject.GetComponent<ItemPickable>();

                if (item != null)
                {
                    Reticle.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    Debug.Log("Looking at Item");
                    inventoryManager.ItemPicked(hitinfo.collider.gameObject);
                }
                else
                {
                    Reticle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                }
            }
        }


        Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo2;

        if (Physics.Raycast(ray2, out hitinfo2, 3))
        {
            ItemPickable item = hitinfo2.collider.gameObject.GetComponent<ItemPickable>();

            if (item != null)
            {
                Reticle.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else
            {
                Reticle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            }
        }
    }


}
