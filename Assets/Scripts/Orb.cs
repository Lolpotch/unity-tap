using UnityEngine;

public class Orb : MonoBehaviour
{
    Sound sound;
    Spawner spawner;
    Score score;
    GameObject bomb;
    Life life;
    public GameObject orbSplash;
    public GameObject orbPoint;

    bool once;
    bool interactable;
    void Awake()
    {
        once = false;
        interactable = false;
        sound = FindObjectOfType<AudioManager>().GetClip("Orb Hit");
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        score = GameObject.Find("Score").GetComponent<Score>();
        life = FindObjectOfType<Life>();
    }

    void LateUpdate()
    {
        if(!once)
        {
            bomb = GameObject.FindGameObjectWithTag("Bomb");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && interactable)
        {
            sound.Play();
            Time.timeScale += 0.005f;
            Instantiate(orbPoint, transform.position, Quaternion.identity);
            life.EnergyRefill();
            StartCoroutine(life.SetEnergyFill(life.energy));
            StartCoroutine(spawner.SpawnOrb());
            score.AddPoint(1);
            Instantiate(orbSplash, transform.position, Quaternion.identity);
            DestroyBombOnScreen();
            Destroy(gameObject);
        }
    }

    void DestroyBombOnScreen()
    {
        if(bomb != null)
        {
            Destroy(bomb);
        }
    }

    //Animation Event
    public void SetInteractBool()
    {
        interactable = !interactable;
    }
}
