using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [Header("Main Stuff")]
    public Money money;
    public Text desc;
    public GameObject buys;
    public Button buyButton;
    public Text priceText;
    public Text dynamic;
    public PlayerLook playerLook;
    public List<UpgradeIndicator> indicatorUnits = new List<UpgradeIndicator>();
    Sound buttonClick, buySound, moneyCounting, equipSound;

    [Space(10)]
    [Header("miscellaneous")]
    public Button[] buttons;

    //Skin button fields
    bool skin;
    bool _bought;
    int _skinIndex;

    //Upgrade button fields
    bool upgrade;
    int _indicatorIndex;
    int _defaultPrice, _multiplier;
    int level, _maxLevel;

    //Both
    int _price;
    string _key;

    void Awake()
    {
        equipSound = FindObjectOfType<AudioManager>().GetClip("Equip");
        buySound = FindObjectOfType<AudioManager>().GetClip("Click Buy");
        buttonClick = FindObjectOfType<AudioManager>().GetClip("Menu Select");
        moneyCounting = FindObjectOfType<AudioManager>().GetClip("Money Counting");
    }
    void OnEnable()
    {
        StartCoroutine(ResetSelectedButton());
        buyButton.interactable = false;
        buys.SetActive(false);
        dynamic.text = "BUY";
        desc.text = "\"Please select what you want\"";
    }

    IEnumerator ResetSelectedButton()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
        yield return null;
    }
    IEnumerator SelectedButton(int selected)
    {
        //Iterates through all buttons and decides which is clicked
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == selected)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].interactable = true;
            }
        }
        yield return null;
    }


    void SetPriceText(int price)
    {
        priceText.text = price.ToString();
    }

    public void TriggerBuy()
    {
        if (skin)
        {
            if (_bought)
            {
                //Equip skin
                equipSound.Play();
                playerLook.SetPlayerLook(_skinIndex);
                PlayerPrefs.SetInt("Skin", _skinIndex);
            }
            else
            {
                //Buy skin
                buySound.Play();
                money.DecreaseImmediately(_price);
                _bought = true;
                PlayerPrefsUtility.SetBool(_key, true);
            }
            SkinButton();
        }
        if (upgrade)
        {
            buySound.Play();
            money.DecreaseImmediately(_price);
            PlayerPrefs.SetInt(_key, ++level);
            UpgradeButton();
            indicatorUnits[_indicatorIndex].ShowIndicator();
        }
    }

    void SkinButton()
    {
        _bought = PlayerPrefsUtility.GetBool(_key);

        if (_bought)
        {
            buys.SetActive(false);

            if(_skinIndex == PlayerPrefs.GetInt("Skin", 0))
            {
                dynamic.text = "EQUIPPED";
                buyButton.interactable = false;
            }
            else
            {
                dynamic.text = "EQUIP";
                buyButton.interactable = true;
            }
        }else //if the skin wasn't bought yet
        {
            buys.SetActive(true);
            SetPriceText(_price);
            dynamic.text = "";

            if (_price > money.money)
            {
                buyButton.interactable = false;
            }else
            {
                buyButton.interactable = true;
            }
        }
    }

    void UpgradeButton()
    {
        float finalPrice = _defaultPrice * Mathf.Pow(_multiplier, level);
        _price = (int)finalPrice;
        bool maxed = level == _maxLevel;
        if(maxed)
        {
            buys.SetActive(false);
            buyButton.interactable = false;
            dynamic.text = "MAXED";
        }else
        {
            buys.SetActive(true);
            SetPriceText(_price);
            dynamic.text = "";

            if (_price > money.money)
            {
                buyButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }
        }
    }

    void SetSkinField(string key, int skinIndex, int price)
    {
        skin = true;
        upgrade = false;

        _key = key;
        _skinIndex = skinIndex;
        _price = price;

        SkinButton();
    }
    void SetUpgradeField(string key, int defaultPrice, int multiplier, int maxLevel, int indicatorIndex)
    {
        upgrade = true;
        skin = false;

        _key = key;
        _defaultPrice = defaultPrice;
        _multiplier = multiplier;
        _maxLevel = maxLevel;
        _indicatorIndex = indicatorIndex;

        level = PlayerPrefs.GetInt(_key, 0);

        UpgradeButton();
    }

    #region Button Methods
    public void StopCountingSound()
    {
        moneyCounting.Stop();
    }
    public void PlayButtonSound()
    {
        buttonClick.Play();
    }

    //Upgrade buttons
    public void MoreEnergy()
    {
        StartCoroutine(SelectedButton(0));
        SetUpgradeField("Energy Level", 1500, 3, 2, 0);
        desc.text = "More Energy - jump more!";
    }

    public void MoreLife()
    {
        StartCoroutine(SelectedButton(1));
        SetUpgradeField("Life Level", 800, 2, 4, 1);
        desc.text = "More Life - survive more bombs!";
    }

    //Skin Buttons
    public void SquareSkin()
    {
        StartCoroutine(SelectedButton(2));
        PlayerPrefsUtility.SetBool("Square", true);
        SetSkinField("Square", 0, 0);
        desc.text = "Square - just a lame shape for this boring game";
    }

    public void HexagonSkin()
    {
        StartCoroutine(SelectedButton(3));
        SetSkinField("Hexagon", 1, 1000);
        desc.text = "Hexagon - at least it has better looks than square";
    }
    public void PlusSkin()
    {
        StartCoroutine(SelectedButton(4));
        SetSkinField("Plus", 2, 1500);
        desc.text = "Plus - positive is always a good sign, right?";
    }
    public void StarSkin()
    {
        StartCoroutine(SelectedButton(5));
        SetSkinField("Star", 3, 3000);
        desc.text = "Star - we all know that \"star\" in the sky doesn't looks like this";
    }
    public void ShurikenSkin()
    {
        StartCoroutine(SelectedButton(6));
        SetSkinField("Shuriken", 4, 5000);
        desc.text = "Shuriken - the current best skin in this game";
    }
    #endregion
}
