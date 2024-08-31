using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Sound sound;
    Score score;
    public GameObject coinSplash;
    public GameObject coinPoint;

    bool interactable;
    void Awake()
    {
        sound = FindObjectOfType<AudioManager>().GetClip("Coin Hit");
        interactable = false;
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && interactable)
        {
            sound.Play();
            Instantiate(coinPoint, transform.position, Quaternion.identity);
            //Life.energy = Life.energyMax;
            score.AddPoint(3);
            PlayParticle(coinSplash);
            Destroy(gameObject);
        }
    }

    void PlayParticle(GameObject particle)
    {
        GameObject p = Instantiate(particle, null);
        p.transform.position = transform.position;
    }

    //Animation Event
    public void SetInteractBool()
    {
        interactable = !interactable;
    }
}
