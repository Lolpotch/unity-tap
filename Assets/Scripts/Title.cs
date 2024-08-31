using UnityEngine;

public class Title : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    PlayerInput input;
    Spawner spawner;
    public GameObject score;
    public GameObject Lifes;
    public GameObject money;

    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rbPlayer.isKinematic = true;
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    void Update()
    {
        WhenTouched();
    }

    void WhenTouched()
    {
        //When touched, just moan erotically :v
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            float distance = Vector3.Distance(input.transform.position, touchPos);

            if(distance < input.maxDistance)
            {
                PrepareStuffs();

                input.AddClickForce();
                gameObject.SetActive(false);
            }

        }

        if(Input.touchCount < 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0f));
            float distance = Vector3.Distance(input.transform.position, touchPos);

            if(distance < input.maxDistance)
            {
                PrepareStuffs();

                input.AddTouchForce();
                gameObject.SetActive(false);
            }
            
        }
    }

    void PrepareStuffs()
    {
        Sound s = FindObjectOfType<AudioManager>().GetClip("BGM");
        s.Play();

        money.SetActive(false);
        score.SetActive(true);
        Lifes.SetActive(true);
        spawner.enabled = true;
        input.enabled = true;
        rbPlayer.isKinematic = false;
    }
}
