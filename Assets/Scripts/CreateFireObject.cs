using UnityEngine;

public class CreateFireObject : MonoBehaviour
{
    public GameObject floorfire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void create(Vector3 location)
    {
        Instantiate(floorfire, location, Quaternion.identity); 
    }

}
