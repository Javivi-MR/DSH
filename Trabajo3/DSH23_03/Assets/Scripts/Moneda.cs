using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public float velocidad;
    public GameObject particulas;
    // Start is called before the first frame update
    void Start()
    {
        velocidad = 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up,Time.deltaTime * velocidad, Space.World);
        //Rotar objeto segun el eje Y.
    }

    void OnDestroy() {
        Vector3 posactual = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Instantiate(particulas,posactual, particulas.transform.rotation);
    }
}
