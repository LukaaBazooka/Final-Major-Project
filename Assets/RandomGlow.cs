using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomGlow : MonoBehaviour
{

    [SerializeField] GameObject Object;
    void Start()
    {
        StartCoroutine(Glowfunction());
    }

    public IEnumerator Glowfunction()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            LeanTween.scale(Object, new Vector3(Random.Range(.5f, 2.3f), Random.Range(.5f, 2.4f), 1f), 3f);


        }



    }
}
