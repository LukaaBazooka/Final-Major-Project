using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    // Start is called before the first frame update

    public bool scancooldown;
    public float KeyCardLevel;
    public bool NeedsCard;

    public IEnumerator coooldown(float waittime)
    {
        scancooldown = true;

        yield return new WaitForSeconds(waittime);
        scancooldown = false;
    }

    public IEnumerator Colorswitch(GameObject Object, bool accepted)
    {
        yield return new WaitForSeconds(.75f);

        GameObject object2 = Object.transform.parent.GetChild(3).gameObject;

        if (accepted)
        {
            Object.transform.GetChild(3).gameObject.SetActive(true);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            object2.transform.GetChild(3).gameObject.SetActive(true);
            object2.transform.GetChild(1).gameObject.SetActive(false);
            object2.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            object2.transform.GetChild(3).gameObject.SetActive(false);
            object2.transform.GetChild(1).gameObject.SetActive(true);
            object2.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(true);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            object2.transform.GetChild(3).gameObject.SetActive(true);
            object2.transform.GetChild(1).gameObject.SetActive(false);
            object2.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            object2.transform.GetChild(3).gameObject.SetActive(false);
            object2.transform.GetChild(1).gameObject.SetActive(true);
            object2.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(true);
            object2.transform.GetChild(3).gameObject.SetActive(false);
            object2.transform.GetChild(1).gameObject.SetActive(false);
            object2.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            object2.transform.GetChild(3).gameObject.SetActive(false);
            object2.transform.GetChild(1).gameObject.SetActive(true);
            object2.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(false);
            Object.transform.GetChild(2).gameObject.SetActive(true);
            object2.transform.GetChild(3).gameObject.SetActive(false);
            object2.transform.GetChild(1).gameObject.SetActive(false);
            object2.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Object.transform.GetChild(3).gameObject.SetActive(false);
            Object.transform.GetChild(1).gameObject.SetActive(true);
            Object.transform.GetChild(2).gameObject.SetActive(false);
            object2.transform.GetChild(3).gameObject.SetActive(false);
            object2.transform.GetChild(1).gameObject.SetActive(true);
            object2.transform.GetChild(2).gameObject.SetActive(false);
        }
        yield break;


    }
}
