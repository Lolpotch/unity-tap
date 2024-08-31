using System.Collections;
using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject[] energies;

    public GameObject[] energyBlanks;

    public GameObject[] lifes;
    public GameObject[] lifeBlanks;

    public Transform[] energyParticlePlots;

    public Transform[] lifeParticlePlots;

    public GameObject energyParticle;
    public GameObject lifeParticle;

    [HideInInspector]
    public  int energy, life;
    int energyMax, lifeMax;

    void Awake()
    {
        energyMax = 3 + PlayerPrefs.GetInt("Energy Level", 0);
        energy = energyMax;
        lifeMax = 1 + PlayerPrefs.GetInt("Life Level", 0);
        life = lifeMax;
    }

    void Start()
    {
        StartCoroutine(SetEnergyBlank());
        StartCoroutine(SetEnergyFill(energy));
        StartCoroutine(SetLifeBlank());
        StartCoroutine(SetLifeFill(life));
    }

    IEnumerator SetEnergyBlank()
    {
        for (int i = 0; i < energyBlanks.Length; i++)
        {
            if(i < energyMax)
            {
                energyBlanks[i].SetActive(true);
            }
            else
            {
                energyBlanks[i].SetActive(false);
            }
        }
        yield return null;
    }

    IEnumerator SetLifeBlank()
    {
        for (int i = 0; i < lifeBlanks.Length; i++)
        {
            if (i < lifeMax)
            {
                lifeBlanks[i].SetActive(true);
            }
            else
            {
                lifeBlanks[i].SetActive(false);
            }
        }
        yield return null;
    }

    public IEnumerator SetEnergyFill(int energy)
    {
        //Called everytime needed
        for(int i = 0; i < energies.Length; i++)
        {
            if(i < energy)
            {
                energies[i].SetActive(true);
            }
            else
            {
                energies[i].SetActive(false); 
            }
        }
        yield return null;
    }

    public IEnumerator SetLifeFill(int life)
    {
        //Called everytime needed
        for (int i = 0; i < lifes.Length; i++)
        {
            if (i < life)
            {
                lifes[i].SetActive(true);
            }
            else
            {
                lifes[i].SetActive(false);
            }
        }
        yield return null;
    }


    public void EnergyRefill()
    {
        energy = energyMax;
    }
    public void LifeRefill()
    {
        life = lifeMax;
    }

    //Handle Particle
    public void LifeParticle(int currentLife)
    {
        Vector3 particlePos = lifeParticlePlots[currentLife - 1].position;

        Instantiate(lifeParticle, particlePos, Quaternion.identity);
    }

    public void EnergyParticle(int currentEnergy)
    {
        Vector3 particlePos = energyParticlePlots[currentEnergy -1].position;

        Instantiate(energyParticle, particlePos, Quaternion.identity);
    }
}
