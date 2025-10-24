using System;
using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

public class FireSpreader : MonoBehaviour
{

    public CreateFireObject firecreator;
    //public FireSpreader newfirespreader;

    public GameObject testcube;
    public GameObject testsphere;
    public GameObject newfirespreader;

    public Transform RaySpawn;
    private Ray testray;
    private Ray testrayforward;
    private Ray testraybackward;
    private Ray testrayleft;
    private Ray testrayright;
    Ray[] rays = new Ray[4];

    bool burnedout;

    float burncount = 0.0f;
    float burnduration = 2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false); 

        testray = new Ray(RaySpawn.position, Vector3.down);
        testrayforward = new Ray(RaySpawn.position + new Vector3(0, 0, 1), Vector3.down);
        testraybackward = new Ray(RaySpawn.position + new Vector3(0, 0, -1), Vector3.down);
        testrayleft = new Ray(RaySpawn.position + new Vector3(-1, 0, 0), Vector3.down);
        testrayright = new Ray(RaySpawn.position + new Vector3(1, 0, 0), Vector3.down);
         
        rays[0] = testrayforward; 
        rays[1] = testraybackward; 
        rays[2] = testrayleft; 
        rays[3] = testrayright;

        burnedout = false;
        //fireLogic();
        burncount = 0.0f;
        burnduration = 2.5f;

        StartCoroutine(NewFireLogic());
    }

    // Update is called once per frame
    void Update()
    {
        //drawLines();
        //fireLogic(); 
    }

    public void drawLines()
    {
        //Debug.DrawLine(RaySpawn.position + new Vector3(0, 0, 1), Vector3.down, Color.red); 
        //Debug.DrawLine(RaySpawn.position, Vector3.back, Color.green);
        // Debug.DrawLine(RaySpawn.position, Vector3.right, Color.blue);
        // Debug.DrawLine(RaySpawn.position, Vector3.left, Color.yellow);

        //RaycastHit hit; 

        

        //Debug.DrawRay(testray.origin, testray.direction, Color.blue);
        //Debug.DrawRay(testrayforward.origin, testray.direction, Color.red);
        //Debug.DrawRay(testrayforward.origin, testray.direction, Color.green);
        //Debug.DrawRay(testrayforward.origin, testray.direction, Color.yellow); 
        //Debug.DrawRay(testrayforward.origin, testray.direction, Color.pink); 

        /*
        if (Physics.Raycast(testray, out hit, 100f))  
        {
            //Debug.DrawLine(RaySpawn.position, Vector3.down, Color.red);  
            if(hit.transform.CompareTag("Flammable"))
            {
                Instantiate(testcube, hit.point, Quaternion.identity); 
            }
            
        }

        if (Physics.Raycast(testrayforward, out hit, 100f)) 
        {
            //Debug.DrawLine(RaySpawn.position, Vector3.down, Color.red);  
            if (hit.transform.CompareTag("Flammable"))
            {
                Instantiate(testcube, hit.point, Quaternion.identity);
            }

        }*/

        for (int i = 0; i < rays.Length; i++)
        {
            raytest(rays[i], Color.blue); 
        }

        /*
        raytest(testray, Color.blue);
        raytest(testrayleft, Color.yellow);
        raytest(testrayright, Color.pink);
        raytest(testrayforward, Color.red);
        raytest(testraybackward, Color.green); 
        */
    }

    public (bool, Vector3) raytest(Ray testray, Color colour) 
    {
        RaycastHit hit;

        Debug.DrawRay(testray.origin, testray.direction, colour); 

        if (Physics.Raycast(testray, out hit, 2.1f))
        {
            //Debug.DrawLine(RaySpawn.position, Vector3.down, Color.red);  
            if (hit.transform.CompareTag("Flammable"))
            {
                //Instantiate(newfire, hit.point, Quaternion.identity);
                //Instantiate(testcube, hit.point, Quaternion.identity); 
                //create();
                return (true, hit.point);
            }

        }
        return (false, hit.point); 
    }

    public void fireLogic()
    {
        //if (Physics.Raycast(rays[0], ))

        //On spawn fire burns for 2.5seconds (burntick)
        
        transform.GetChild(1).gameObject.SetActive(true); //enable fire on spawn
        if ((Time.time - burncount) > burnduration) 
        {
            transform.GetChild(1).gameObject.SetActive(false); //disable fire after 2.5 seconds
            //drawLines(); //check surroundings for flammable
            createNewFire();
            burnedout = true;
        }
    }

    IEnumerator NewFireLogic()
    {
        burnedout = false;
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        transform.GetChild(1).gameObject.SetActive(false);
        burnedout = true;
        //maybe instantiate gameobject "fireblocker" here to prevent fire from spreading to previous location of fire.
        createNewFire();
        Destroy(gameObject); //not sure if this is helping
    }
    /*public FireSpreader create()
    {
        var instance = Instantiate(this);

        instance.newfirespreader = this;

        Destroy(gameObject);

        return instance; 

    }*/

    public void createNewFire()
    {
       RaycastHit hit;

        if (burnedout)
        {
            for (int i = 0; i < rays.Length; i++)
            {
                if (Physics.Raycast(rays[i], out hit, 2.1f))
                {
                    if (hit.transform.CompareTag("Flammable"))
                    {
                        Debug.DrawRay(testray.origin, testray.direction, Color.red);
                        //Instantiate(newfire, hit.point, Quaternion.identity);
                        //Instantiate(testcube, hit.point, Quaternion.identity); 
                        //create();
                        //gameObject.GetComponent<CreateFireObject>().create(hit.point); 
                        //firecreator.create(hit.point);
                        //Instantiate(this, hit.point, Quaternion.identity); 
                        GameObject newfire = Instantiate(newfirespreader, hit.point, Quaternion.identity) as GameObject;   
                        
                    }
                    else
                    {
                        Debug.Log("Flammable surface not found");
                    }
                }
            }
        }
     
    }

}
