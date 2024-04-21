using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LoadIn : MonoBehaviour
{

    public RawImage Panel;
    public RawImage Icon;
    public RawImage Glow;
    public RawImage Text;


    public GameObject MyCanvas;

    public GameObject Play;
    public GameObject Settings;

    public GameObject Scene;
    public AudioSource audiothing;

    void Start()
    {
        StartCoroutine(Loadthing());
    }

    public IEnumerator Loadthing()
    {
        yield return new WaitForSeconds(1f);
        audiothing.Play();
        yield return new WaitForSeconds(1f);

        LeanTween.alpha(Glow.rectTransform, 1f, 2f).setEaseInOutQuad();
        LeanTween.alpha(Icon.rectTransform, 1f, 2f).setEaseInOutQuad();

        yield return new WaitForSeconds(2.5f);
        LeanTween.alpha(Text.rectTransform, 1f, 2f).setEaseInOutQuad();
        yield return new WaitForSeconds(2f);

        LeanTween.alpha(Text.rectTransform, 0f, 2f).setEaseInOutQuad();
        LeanTween.alpha(Glow.rectTransform, 0f, 2f).setEaseInOutQuad();
        LeanTween.alpha(Icon.rectTransform, 0f, 2f).setEaseInOutQuad();
        yield return new WaitForSeconds(3f);
        Scene.SetActive(true);

        LeanTween.alpha(Panel.rectTransform, 0f, 1f).setEaseInOutQuad();



    

        yield return new WaitForSeconds(1.5f);

        LeanTween.moveLocal(Play, new Vector3(-244.4f,-10, 0),2f).setEaseInOutQuad();
        yield return new WaitForSeconds(.2f);
        LeanTween.moveLocal(Settings, new Vector3(-244.4f, -96, 0), 2f).setEaseInOutQuad();


        MyCanvas.SetActive(false);




    }
}
