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

    public GameObject cog;

    public GameObject MyCanvas;


    public GameObject Scene;
    public AudioSource audiothing;
    public AudioSource CogSpin;

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


        yield return new WaitForSeconds(.5f);

        CogSpin.Play();
    

        LeanTween.rotate(cog, new Vector3(0, 0, 180), 2f).setEaseInOutBack();

        yield return new WaitForSeconds(.5f);

        MyCanvas.SetActive(false);

    }
}
