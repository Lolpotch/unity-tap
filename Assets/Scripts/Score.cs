using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text score;
    Animator anim;
    public int scorePoint;
    void Awake()
    {
        anim = GetComponent<Animator>();
        score = GetComponent<Text>();
        scorePoint = 0;
    }

    void Update()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        score.text = scorePoint.ToString();
    }

    public void AddPoint(int amount)
    {
        anim.Play("Score Pop", 0, 0f);
        scorePoint += amount;
    }

}
