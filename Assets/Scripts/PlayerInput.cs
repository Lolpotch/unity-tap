using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Sound tapSound;
    Camera mainCam;
    public Rigidbody2D rb;
    public Life life;
    public GameObject tapSplash;

    public float maxDistance = 10f;
    public float rotateForce = 500f;
    public float tapForce = 5f;
    public float limitFallSpeed = 5f;

    [HideInInspector]
    bool touched;
    float rotation;
    bool once;
    void Awake()
    {
        tapSound = FindObjectOfType<AudioManager>().GetClip("Player Tap");
        mainCam = Camera.main;
        rotation = 0f;
        once = false;
    }

    void Update()
    {
        if (!once)
        {
            once = true;
            touched = true;
        }
        else touched = false;
        MyInput();
        HandleRotation(transform.rotation);
        LimitFallSpeed();
    }

    void MyInput()
    {
        
        if(Input.touchCount < 0 && life.energy > 0)
        {
            AddTouchForce();
        }

        if (Input.GetMouseButtonDown(0) && life.energy > 0)
        {
            AddClickForce();
        }
    }

    public void AddTouchForce()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = mainCam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0f));

        float distance = Vector3.Distance(transform.position, touchPos);

        if (distance < maxDistance)
        {
            tapSound.Play();
            touched = true;
            life.EnergyParticle(life.energy);
            StartCoroutine(life.SetEnergyFill(--life.energy));

            PlayParticle(tapSplash, touchPos);

            //Look at touch position
            Vector3 difference = touchPos - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);


            rb.velocity = transform.up * tapForce;
        }
    }

    public void AddClickForce()
    {
        Vector3 touchPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

        float distance = Vector3.Distance(transform.position, touchPos);

        if (distance < maxDistance)
        {
            tapSound.Play();
            touched = true;
            life.EnergyParticle(life.energy);
            life.energy--;
            StartCoroutine(life.SetEnergyFill(life.energy));

            PlayParticle(tapSplash, touchPos);

            //Look at mouse position
            Vector3 difference = touchPos - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);


            rb.velocity = transform.up * tapForce;
        }
    }

    
    void HandleRotation(Quaternion playerRotation)
    {
        if(touched)
        {
            if (playerRotation.z <= 0f)
            {
                rotation = -rotateForce;
            }
            else
            {
                rotation = rotateForce;
            }
            touched = false;
        }
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
    }

    void LimitFallSpeed()
    {
        Vector3 speed = rb.velocity;
        speed.y = Mathf.Clamp(speed.y, -limitFallSpeed, 20f);
        rb.velocity = speed;
    }


    void PlayParticle(GameObject particle, Vector3 position)
    {
        GameObject a = Instantiate(particle, null);
        a.transform.position = new Vector3(position.x, position.y, 0f);
    }
}
