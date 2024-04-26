using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioSource SecondSource;
    [SerializeField] RawImage Panel;


    void Start()
    {
        StartCoroutine(Load());
    }

    // Update is called once per frame
    public IEnumerator Load()
    {

        

        m_AudioSource.Play();
        yield return new WaitForSeconds(1f);
        LeanTween.alpha(Panel.rectTransform, 0f, 2f).setEaseInOutQuad();
        yield return new WaitForSeconds(7.5f);
        SecondSource.Play();

        yield return new WaitForSeconds(17f);
        LeanTween.alpha(Panel.rectTransform, 1f, 0f).setEaseInOutQuad();

        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("MainScene");

    }
}
