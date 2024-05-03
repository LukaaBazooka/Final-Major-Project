using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioSource SecondSource;
    [SerializeField] RawImage Panel;
    [SerializeField] AudioSource Radio;

    public TMP_Text _tmpProText;
    string writer;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;
    void Start()
    {


        writer = _tmpProText.text;
        StartCoroutine(Load());
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    public IEnumerator Load()
    {

        

        m_AudioSource.Play();
        yield return new WaitForSeconds(1f);
        LeanTween.alpha(Panel.rectTransform, 0f, 2f).setEaseInOutQuad();
        yield return new WaitForSeconds(2f);

        Radio.Play();
        StartCoroutine(WRITE());


        yield return new WaitForSeconds(23f);
        SecondSource.Play();

        yield return new WaitForSeconds(17f);
        LeanTween.alpha(Panel.rectTransform, 1f, 0f).setEaseInOutQuad();

        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("MainScene");

    }

    IEnumerator WRITE()
    {
        yield return new WaitForSeconds(1f);

        writer = " Dr. Reed,";
        StartCoroutine("TypeWriterTMP");
        yield return new WaitForSeconds(1.02f);
        
        writer = " This is a priority-level Alpha retrieval mission.";
        StartCoroutine("TypeWriterTMP");

        yield return new WaitForSeconds(2.8f);
        writer = " Your objective is to secure all the relevant research data of Haven Research facility.";
        StartCoroutine("TypeWriterTMP");

        yield return new WaitForSeconds(4.9f);
        writer = " However exercise extreme caution,";
        StartCoroutine("TypeWriterTMP");
        yield return new WaitForSeconds(2.5f);
        writer = " Intel indicates a possible containment breach may have led to its abandonment.";
        StartCoroutine("TypeWriterTMP");
        yield return new WaitForSeconds(4.5f);
        writer = " Assume containment breach protocols are in effect at all times.";
        StartCoroutine("TypeWriterTMP");
        yield return new WaitForSeconds(4.1f);
        writer = " Your success is paramount doctor. recover the data no matter the cost.";
        StartCoroutine("TypeWriterTMP");
        yield return new WaitForSeconds(6f);
        _tmpProText.enabled= false;

    }

    IEnumerator TypeWriterTMP()
    {
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }
    }

}
