using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIndicator : MonoBehaviour
{
    public string key;
    List<Transform> units = new List<Transform>();

    void Awake()
    {
        StartCoroutine(GetIndicator());
    }

    void OnEnable()
    {
        StartCoroutine(Indicator(key));
    }
    public void ShowIndicator()
    {
        StartCoroutine(Indicator(key));
    }
    IEnumerator Indicator(string key)
    {
        int level = PlayerPrefs.GetInt(key);
        for(int i = 0; i < units.Count; i++)
        {
            if(i < level)
            {
                units[i].gameObject.SetActive(true);
            }
            else
            {
                units[i].gameObject.SetActive(false);
            }
        }
        yield return null;
    }

    IEnumerator GetIndicator()
    {
        units.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).tag == "Fill Indicator")
            {
                units.Add(transform.GetChild(i));
            }
        }
        yield return null;
    }
}
