using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private AudioSource fireSound;
    public GameObject firePrefab; 

    

    [SerializeField] int health = 10;
    //[SerializeField] int damage = 4;
    //[SerializeField] float insulation = 1f;
    [SerializeField] int temperature = 1; 
    [SerializeField] int ignitionPoint = 10;

    //[SerializeField] bool isFlammable;
    [SerializeField] bool onFire; 

    float ignitiontime  = 0f;
    float ignitiontick = 0.26f;
    float damagetick = 1f;
    float damagetime = 0f;
    float darkenValue = 0f;  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponentInParent<Renderer>().sharedMaterial.SetFloat("_Darken", darkenValue); //PUT WORK IN GITHUB REPO
    }

    // Update is called once per frame
    void Update()
    {
       startFire();
       Debug.Log("temp =" + temperature);
       Debug.Log("hp =" + health); 
    }

    private void OnTriggerStay(Collider other)    
    {
        if (other.gameObject.tag == "Fire")
        {
            Debug.Log("Fire detected");
            
            if ((Time.time - ignitiontime) > ignitiontick)
            {
                ignitiontime = Time.time;

                temperature++;
            }
        }
    }

    public void startFire()
    {
        if (temperature > ignitionPoint)
        {
            //Instantiate(firePrefab, this.transform.position, this.transform.rotation);   
            onFire = true;
        }
        Burning();
    }

    public void Burning()
    {
        if (onFire)
        {
            transform.GetChild(0).gameObject.SetActive(true); 
            fireParticles = GetComponentInChildren<ParticleSystem>();
            fireSound = GetComponentInChildren<AudioSource>();
            if (!fireParticles.isPlaying && !fireSound.isPlaying)
            {
                fireParticles.Play();
                fireSound.Play();
            }
            TakeDamage();
        }

         
    }
    public void TakeDamage()
    {
        if ((Time.time - damagetime) > damagetick)
        {
            damagetime = Time.time; 

            health -= 3; 
        }

        if (health <= 0)
        {
            Destroy(transform.parent.gameObject); 
        }
    }


    /*public void burning()
    {
        if (fireDetected)
        {
            TempIncrease(2, insulation);
        }
        else
        {
            fireDetected = false; 
        }
    }
    

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0 )
        {
            Destroy(transform.parent.gameObject); 
        } 
    }

    IEnumerator IncreaseTemperature(int tempincrease, float insulationlevel)
    {
        temperature += tempincrease; 
        yield return new WaitForSeconds(insulationlevel); 
    }

    public void TempIncrease(int tempincrease, float insulationlevel)
    {
        StartCoroutine(IncreaseTemperature(tempincrease, insulationlevel));  
    }*/
}
 