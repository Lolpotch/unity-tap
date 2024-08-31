using UnityEngine;

public class Bomb : MonoBehaviour
{
    Sound sound;
    Life life;
    CheckBelowLimit lose;
    GameObject player;
    CameraShake shake;
    public GameObject bombSplash;
    public GameObject playerSplash;
    bool interactable;

    void Awake()
    {
        life = FindObjectOfType<Life>();
        sound = FindObjectOfType<AudioManager>().GetClip("Bomb Hit");
        transform.rotation = Quaternion.Euler(RandomRotation());
        interactable = false;
        shake = Camera.main.GetComponent<CameraShake>();
        player = GameObject.FindGameObjectWithTag("Player");
        lose = player.GetComponent<CheckBelowLimit>();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && interactable)
        {
            life.LifeParticle(life.life);
            StartCoroutine(life.SetLifeFill(--life.life));
            if(life.life <= 0)
            {
                Time.timeScale = 1f;
                Instantiate(bombSplash, transform.position, Quaternion.identity);
                Instantiate(playerSplash, transform.position, Quaternion.identity);
                lose.losePanel.SetActive(true);
                player.SetActive(false);
            }
            Instantiate(bombSplash, transform.position, Quaternion.identity);
            sound.Play();
            shake.OnPlayerBoom();
            Destroy(gameObject);
        }
    }

    Vector3 RandomRotation()
    {
        float rot = Random.Range(0f, 360f);
        return new Vector3(0f, 0f, rot);
    }
    void BombParticle(GameObject bomb, GameObject playerParticle)
    {
        GameObject pa = Instantiate(bomb, null);
        pa.transform.position = transform.position;

        GameObject pi = Instantiate(playerParticle, null);
        pi.transform.position = player.transform.position;
    }

    public void SetInteractBool()
    {
        interactable = !interactable;
    }
}
