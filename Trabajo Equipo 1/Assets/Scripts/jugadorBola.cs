using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jugadorBola : MonoBehaviour
{
    public float velocidad;
    private Rigidbody rb;

    public Camera camara;
    private Vector3 offset;
    private int estrellas;

    public Text texto;

    // Start is called before the first frame update
    void Start()
    {
        velocidad = 3;
        rb = GetComponent<Rigidbody>();
        estrellas = 0;
        offset = camara.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movHorizontal, 0.0f, movVertical);
        rb.AddForce(movimiento * velocidad);

        camara.transform.position = this.transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("diamanteTag"))
        {
            Destroy(other.gameObject);
            estrellas++;
            texto.text = "Estrellas:" + estrellas;
        }
        if(estrellas == 6)
        {
            SceneManager.LoadScene("Mapa2",LoadSceneMode.Single);
        }
    }
}
