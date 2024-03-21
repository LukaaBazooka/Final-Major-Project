using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorThng : MonoBehaviour
{
    // Start is called before the first frame update




    [SerializeField] AudioClip DoorOpen;
    [SerializeField] AudioClip DoorOpen2;

    [SerializeField] AudioClip DoorClose;
    [SerializeField] AudioClip DoorClose2;
    public  Animator anim;






    [SerializeField]  GameObject Door;


    public IEnumerator Animthingy()
    {

        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(DoorOpen, Door.transform.position, 2f);

        anim.SetBool("IsOpen", true);
        anim.SetBool("Activated", true);
        AudioSource.PlayClipAtPoint(DoorClose, Door.transform.position, 2f);

        anim.Play("DoorOpen");
        anim.SetBool("Activated", false);
        yield return new WaitForSeconds(3f);

        AudioSource.PlayClipAtPoint(DoorOpen2, Door.transform.position, 2f);

        yield return new WaitForSeconds(3f);
        AudioSource.PlayClipAtPoint(DoorClose, Door.transform.position, 2f);

        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(DoorClose2, Door.transform.position, 2f);

        yield return new WaitForSeconds(1f);
        Debug.Log("closing");
        anim.Play("DoorClose");
        AudioSource.PlayClipAtPoint(DoorOpen, Door.transform.position, 2f);
        yield break;


    }

}
