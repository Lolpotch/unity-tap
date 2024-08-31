using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    //Setting up the position of the borders.
    //So they could adapt with different screen ratio
    //Pretty retarded script btw


    //In the end, I forced the game to use typical ratio instead;
    //So I won't use this apperently
    //No shit, I'm having a bad time with this stupid game
    Camera mainCam;
    Vector3 screenBorder;
    public Transform topBorder;
    public Transform leftBorder;
    public Transform rightBorder;
    public Transform bottomBorder;
    void Start()
    {
        mainCam = Camera.main;
        screenBorder = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    }

    void Update()
    {
        topBorder.position = Vector3.up * (screenBorder.y + .1f);
        leftBorder.position = Vector3.left * (screenBorder.x + .1f);
        rightBorder.position = Vector3.right * (screenBorder.x + .1f);
        bottomBorder.position = Vector3.down * (screenBorder.y + 1f);
    }
}
