using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    Sound sound;
    public Text text;
    [HideInInspector]
    public int money;

    void Awake()
    {
        sound = FindObjectOfType<AudioManager>().GetClip("Money Counting");
    }
    void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        DisplayMoney(money);
    }

    void DisplayMoney(int amount)
    {
        text.text = amount.ToString();
    }

    IEnumerator Increase(int changedAmount)
    {
        bool moneySet = false;
        int end = money + changedAmount;
        sound.Play();
        for (int begin = money; begin <= end; begin++)
        {
            if(!moneySet)
            {
                moneySet = true;
                money = end;
                PlayerPrefs.SetInt("Money", money);
            }
            DisplayMoney(begin);
            yield return null;
        }
        sound.Stop();
        DisplayMoney(end);

    }

    IEnumerator Decrease(int changedAmount)
    {
        bool moneySet = false;
        int end = money - changedAmount;

        for (int begin = money; begin >= end; begin--)
        {
            if (!moneySet)
            {
                moneySet = true;
                money = end;
                PlayerPrefs.SetInt("Money", money);
            }
            DisplayMoney(begin);
            yield return null;
        }

        DisplayMoney(end);
    }

    //Animation Events
    public void StartIncrease(int amount)
    {
        StartCoroutine(Increase(amount));
    }

    public void StartDecrease(int amount)
    {
        StartCoroutine(Decrease(amount));
    }
    public void IncreaseImmediately(int amount)
    {
        money += amount;
        DisplayMoney(money);
        PlayerPrefs.SetInt("Money", money);
    }

    public void DecreaseImmediately(int amount) 
    {
        money -= amount;
        DisplayMoney(money);
        PlayerPrefs.SetInt("Money", money);
    }

}
