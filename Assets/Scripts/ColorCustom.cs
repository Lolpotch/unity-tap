using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;

public class ColorCustom : MonoBehaviour
{
    Image image;
    SpriteRenderer sprite;
    ParticleSystem particle;
    ParticleSystem.MainModule settings;

    public Color[] colors;

    void Awake()
    {
        GetRefs();
    }
    void Start()
    {
        SetColor(PlayerPrefs.GetInt("Color Index", 0));
    }

    void GetRefs()
    {
        settings = GetComponent<ParticleSystem>().main;
        particle = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    public void SetColor(int i)
    {
        if(sprite != null)
        {
            sprite.color = colors[i];
        }
        if(image != null)
        {
            image.color = colors[i];
        }
        if(particle != null)
        {
            var main = particle.main;
            main.startColor = colors[i];
        }
    }
}
