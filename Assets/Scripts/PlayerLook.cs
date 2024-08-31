using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    SpriteRenderer shape;

    public Sprite[] skins;
    void Awake()
    {
        shape = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetPlayerLook(PlayerPrefs.GetInt("Skin", 0));
    }

    public void SetPlayerLook(int i)
    {
        shape.sprite = skins[i];
    }
}
