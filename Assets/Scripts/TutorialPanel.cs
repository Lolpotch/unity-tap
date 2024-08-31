using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    bool firstStart;
    public GameObject opening;

    void Awake()
    {
        firstStart = PlayerPrefsUtility.GetBool("Tutorial", false);
    }
    private void Start()
    {
        OnPlayerTouch();
    }
    void Update()
    {
        OnPlayerTouch();
    }

    // I hope I won't ever need look at this script anymore
    void OnPlayerTouch()
    {
        if(!firstStart)
        {
            if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                firstStart = true;
                PlayerPrefsUtility.SetBool("Tutorial", firstStart);
                opening.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        else 
        {
            opening.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
