using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{

    public Camera camara;
    public int velocidad;
    public GameObject prefabSuelo;
    public GameObject prefabMoneda;
    public GameObject prefabBarra;
    public Text textoestrellas;

    private Vector3 offset;
    private float valx;
    private float valz;
    private int estrellas;
    
    //Movimiento del jugador
    private Rigidbody rb; 
    private Vector3 direccionActual;
    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        valx = 0.0f;
        valz = 0.0f;
        rb = GetComponent<Rigidbody>();
        direccionActual = Vector3.forward;
        SueloInicial();
    }

    void SueloInicial()
    {
        for(int n = 0 ; n < 3 ; n++)
        {
            valz += 6.0f;
            GameObject elsuelo = Instantiate(prefabSuelo,new Vector3(valx,0.0f,valz), Quaternion.identity) as GameObject;
            if(n==2)
            {
                float ran = Random.Range(-2f,2f);
                GameObject moneda = Instantiate(prefabMoneda,new Vector3(valx + ran,0.5f,valz + ran), Quaternion.identity) as GameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        camara.transform.position = this.transform.position + offset;

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(direccionActual == Vector3.forward)
            {
                direccionActual = Vector3.right;
            }
            else
            {
                direccionActual = Vector3.forward;
            }
        }

        if(rb.transform.position.y < 0)
        {
            SceneManager.LoadScene("Scene3",LoadSceneMode.Single);
        }

        float tiempo = velocidad*Time.deltaTime;
        
        rb.transform.Translate(direccionActual*tiempo);
    }

    void OnCollisionExit(Collision other) 
    {
        if(other.transform.tag == "suelo")
        {
            StartCoroutine(CrearSuelo(other));
        }    
    }

    IEnumerator CrearSuelo(Collision col)
    {
        Debug.Log("Cae");
        yield return new WaitForSeconds(0.5f);
        col.rigidbody.isKinematic = false;
        col.rigidbody.useGravity = true; 
        yield return new WaitForSeconds(0.5f);
        Destroy(col.gameObject);
        float ran = Random.Range(0f,1f);
        if(ran < 0.5f)
        {
            valx += 6.0f;
        }
        else
        {
            valz += 6.0f;
        }
        
        GameObject elsuelo = Instantiate(prefabSuelo,new Vector3(valx,0.0f,valz), Quaternion.identity) as GameObject;
        
        ran = Random.Range(0f,1f);
        if(ran < 0.5f) //Cada suelo generado tiene un 50% de posibilidad de poseer una moneda
        {
            ran = Random.Range(-2f,2f);
            float ran2 =  Random.Range(-3f,3f);
            GameObject moneda = Instantiate(prefabMoneda,new Vector3(valx + ran,0.5f,valz + ran), Quaternion.identity) as GameObject;
            GameObject barra = Instantiate(prefabBarra,new Vector3(valx + ran2,0.5f,valz + ran2), Quaternion.identity) as GameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
            
        if(other.gameObject.CompareTag("moneda"))
        {
            Destroy(other.gameObject);
            estrellas++;
            textoestrellas.text = "Monedas: " + estrellas;
        }
        if(other.gameObject.CompareTag("barratag"))
        {
            Debug.Log("He chocado con un muro");
            SceneManager.LoadScene("Scene3",LoadSceneMode.Single);
        }
    }
}

