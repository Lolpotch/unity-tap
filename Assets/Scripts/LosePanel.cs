using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour 
{
    InitializeAd adManager;
    Sound buttonClick;
    public Money money;
    public Button play;
    public Button adButton;
    public Button shop;
    public Button quit;
    public GameObject newRecord;

    public GameObject lifes;
    public Score score;

    public Text earned;
    public Text result;
    public Text best;

    int bestScore;
    int point;

    public void PlayButtonSound()
    {
        buttonClick.Play();
    }

    void Awake()
    {
        buttonClick = FindObjectOfType<AudioManager>().GetClip("Menu Select");

        int loseCount = PlayerPrefs.GetInt("Lose Count", 0);
        PlayerPrefs.SetInt("Lose Count", ++loseCount);
        if(loseCount % 5 == 0 && loseCount != 0)
        {
            adManager = FindObjectOfType<InitializeAd>();
            adManager.ShowSkippableAd();
        } 
    }

    void Start()
    {
        Sound s = FindObjectOfType<AudioManager>().GetClip("BGM");
        s.Stop();

        Time.timeScale = 1f;
        point = score.scorePoint * 3;

        money.gameObject.SetActive(true);
        play.interactable = false;
        adButton.interactable = false;
        shop.interactable = false;
        quit.interactable = false;

        ValidateBestScore();
        DisplayScore(0);
        lifes.SetActive(false);
        score.gameObject.SetActive(false);
    }

    void ValidateBestScore()
    {
        bestScore = PlayerPrefs.GetInt("Best Score", 0);

        if (score.scorePoint > bestScore)
        {
            newRecord.SetActive(true);
            PlayerPrefs.SetInt("Best Score", score.scorePoint);
            bestScore = score.scorePoint;
        }
    }

    void DisplayScore(int point)
    {
        earned.text = point.ToString();
        result.text = score.scorePoint.ToString();
        best.text = bestScore.ToString();
    }

    public void TripleAdReward()
    {
        int rewardedPoint = point * 3;
        adButton.gameObject.SetActive(false);
        money.StartIncrease(rewardedPoint - point);
        StartCoroutine(IncreasingNumber(point, rewardedPoint));
    }

    public void RemoveAdButton()
    {
        adButton.gameObject.SetActive(false);
    }

    IEnumerator IncreasingNumber(int begin, int finalPoint)
    {
        for(int i = begin; i <= finalPoint; i++)
        {
            DisplayScore(i);
            yield return null;
        }
    }


    //Animation Events
    void SetInteractable()
    {
        play.interactable = true;
        adButton.interactable = true;
        shop.interactable = true;
        quit.interactable = true;
    }

    void StartIncreaseScore()
    {
        StartCoroutine(IncreasingNumber(0, point));
        money.StartIncrease(point);
    }
}