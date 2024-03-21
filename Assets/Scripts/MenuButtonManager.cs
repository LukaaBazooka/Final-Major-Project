using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage Panel;
    public GameObject Canvas;
    public AudioSource Sound;
    public GameObject Glow;

    public void Start()
    {
        Debug.Log("Loaded??");

    }

    public IEnumerator LOAD2()
    {
        Canvas.SetActive(true);
        Sound.Play();

        LeanTween.scale(Glow, new Vector3(1.98549998f, 0.504339993f, 1f), 1f).setEaseInOutQuad();


        LeanTween.alpha(Panel.rectTransform, 1f, 2f).setEaseInOutQuad();

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }
    public void LoadMainScene()
    {
        StartCoroutine(LOAD2());
    }
}
