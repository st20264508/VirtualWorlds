using UnityEngine;

public class RayCast : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             
             
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(prefab1, hit.point, Quaternion.identity); 

                /*Flammable flammable = hit.transform.GetComponent<Flammable>();
                if (flammable != null)
                {
                    //flammable.Damage(2); 
                }*/
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(prefab2, hit.point, Quaternion.identity);

                /*Flammable flammable = hit.transform.GetComponent<Flammable>();
                if (flammable != null)
                {
                    //flammable.Damage(2); 
                }*/
            }
        }
    }
}
