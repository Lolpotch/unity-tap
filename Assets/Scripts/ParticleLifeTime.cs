using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeTime : MonoBehaviour
{
    float duration = 1f;
    void Start()
    {
        Destroy(gameObject, duration);
    }
}
