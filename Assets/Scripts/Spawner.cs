using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Score score;
    public Transform spawnTL;
    public Transform spawnBR;
    public GameObject orb;
    public GameObject bomb;
    public GameObject coin;
    Transform player;

    public float minDistance = 3f;
    public float minOrbDistance = .5f;
    Vector3 previousPos;
    Vector3 orbPos;
    Vector3 bombPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        previousPos = player.position + Vector3.forward * .1f;
        StartCoroutine(SpawnOrb());
    }


    public IEnumerator SpawnOrb()
    {
        bool validatedOrb = false;
        while(!validatedOrb)
        {
            RandomOrbPosition();
            validatedOrb = OrbPositionValidator();
        }

        validatedOrb = false;
        GameObject a = Instantiate(orb, null);
        a.transform.position = orbPos;

        float chance = Random.Range(0f, 1f);
        if (chance > .6f && score.scorePoint > 5)
        {
            StartCoroutine(SpawnBomb());
        }
        yield return null;
    }

    IEnumerator SpawnBomb()
    {
        bool validatedBomb = false;
        while(!validatedBomb)
        {
            RandomBombPosition();
            validatedBomb = BombPositionValidator();
        }

        if (validatedBomb)
        {
            validatedBomb = false;
            GameObject b;
            if(Random.Range(0,2) == 0)
            {
                b = Instantiate(bomb, null);
            }
            else
            {
                b = Instantiate(coin, null);
            }
            b.transform.position = bombPos;
        }
        yield return null;
    }


    void RandomOrbPosition()
    {
        float x = Random.Range(spawnTL.position.x, spawnBR.position.x);
        float y = Random.Range(spawnTL.position.y, spawnBR.position.y);
        orbPos = new Vector3(x, y, 110.1f);
    }
    bool OrbPositionValidator()
    {
        float distance = Vector3.Distance(orbPos, previousPos);
        if (distance > minDistance)
        {
            previousPos = orbPos;
            return true;
        }else
        {
            return false;
        }
    }


    void RandomBombPosition()
    {
        float x = Random.Range(spawnTL.position.x, spawnBR.position.x);
        float y = Random.Range(spawnTL.position.y, spawnBR.position.y);
        bombPos = new Vector3(x, y, 110.1f);
    }
    bool BombPositionValidator()
    {
        Vector3 playerPos = player.position + Vector3.forward * .1f;
        float distance = Vector3.Distance(bombPos, orbPos);
        float distancePlayer = Vector3.Distance(bombPos, playerPos);

        if (distance > minOrbDistance && distancePlayer > minDistance)
        {
            return true;
        }else
        {
            return false;
        }

    }

}
