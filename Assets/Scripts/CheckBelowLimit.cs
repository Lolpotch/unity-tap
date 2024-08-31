using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBelowLimit : MonoBehaviour
{
    public Transform bottomLimit;
    public GameObject losePanel;

    void LateUpdate()
    {
        CheckLimit();
    }

    void CheckLimit()
    {
        if (transform.position.y < bottomLimit.position.y)
        {
            losePanel.SetActive(true);
            print("YOU SUCK");
            gameObject.SetActive(false);
        }
    }
}
