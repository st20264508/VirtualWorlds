using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private AudioSource fireSound;
    public GameObject firePrefab; 

    

    [SerializeField] float health = 10f;
    //[SerializeField] int damage = 4;
    //[SerializeField] float insulation = 1f;
    [SerializeField] float temperature = 1f; 
    [SerializeField] float ignitionPoint = 10f;

    //[SerializeField] bool isFlammable;
    [SerializeField] bool onFire; 

    float temptime  = 0f; //for resetting timer, could be local variable
    float temptick = 0.26f; //how often temp increases
    float damagetick = 0.17f; //how often object takes fire damage 
    float damagetime = 0f; //for resetting timer, could be local variable
    float darkenvalue;
    float starthealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        //gameObject.GetComponentInParent<Renderer>().sharedMaterial.SetFloat("_Darken", darkenValue); 
        starthealth = health;
    }

    // Update is called once per frame
    void Update()
    {
       DarkenObject();
       startFire();
       Debug.Log("temp =" + temperature);
       Debug.Log("hp =" + health);
       Debug.Log("darken =" + darkenvalue); 
    }

    private void OnTriggerStay(Collider other)    
    {
        if (other.gameObject.tag == "Fire")
        {
            Debug.Log("Fire detected");
            
            if ((Time.time - temptime) > temptick)
            {
                temptime = Time.time; 

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

    void Burning()
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
    void TakeDamage()
    {
        if ((Time.time - damagetime) > damagetick)
        {
            damagetime = Time.time; 

            health -= 0.5f; 
        }

        if (health <= 0f)
        {
            Destroy(transform.parent.gameObject); 
        }
    }

    void DarkenObject()
    {
        darkenvalue = health/starthealth;
        gameObject.GetComponentInParent<Renderer>().sharedMaterial.SetFloat("_Darken", darkenvalue);  
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
 