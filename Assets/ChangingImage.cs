using UnityEngine;
using UnityEngine.UI;

public class ChangingImage : MonoBehaviour
{
    Image image;
    public Sprite[] images;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    //Animation event
    public void Changing()
    {
        if (image.sprite == images[0])
        {
            image.sprite = images[1];
        }
        else
        {
            image.sprite = images[0];
        }
    }
}
