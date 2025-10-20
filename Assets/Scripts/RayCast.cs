using UnityEngine;

public class RayCast : MonoBehaviour
{
    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             
             
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(prefab, hit.point, Quaternion.identity); 

                Flammable flammable = hit.transform.GetComponent<Flammable>();
                if (flammable != null)
                {
                    //flammable.Damage(2); 
                }
            }
        }
    }
}
