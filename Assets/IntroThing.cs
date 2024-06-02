using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class IntroThing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RawImage Panel;


    public InputManager InputManager;
    public PlayerMotor PlayerMotor;
    public PlayerLook playerLook;
    public Animator animator;
    public GameObject Cam;
    [SerializeField] Volume VOLUME;

    [SerializeField] DepthOfField Depth;

    void Start()
    {
        VOLUME.profile.TryGet(out Depth);
        LeanTween.alpha(Panel.rectTransform, 0f, 1f).setEaseInOutQuad();

        StartCoroutine(Waiter());
    }

    // Update is called once per frame
    public IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5.25f);
        animator.enabled = false;
        yield return new WaitForSeconds(1f);

        InputManager.enabled = true;
        PlayerMotor.enabled = true;
        playerLook.enabled = true;

        for (int i = 0; i < 20; i++)
        {
            Depth.aperture.value++;
            yield return new WaitForSeconds(.2f);

        }

        Panel.enabled = false;

        Cam.transform.localPosition = new Vector3(0, 0.6000000004f, 0);

    }
}
