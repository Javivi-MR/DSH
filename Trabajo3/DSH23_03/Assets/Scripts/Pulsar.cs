using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pulsar : MonoBehaviour
{
    private Button btn;
    public Image img;

    public Sprite[] spNumeros;
    public Text t;


    private bool contar;
    private int numero;

    // Start is called before the first frame update
    void Start()
    {
        //btn = GameObject.FindAnyObjectByType<Button>()
        btn = GameObject.FindWithTag("btnstart").GetComponent<Button>();
        btn.onClick.AddListener(Pulsado);
        contar = false;
        numero = 3;
    }

    void Pulsado()
    {
        Debug.Log("Pulsado Start");
        img.gameObject.SetActive(true);
        t.gameObject.SetActive(true);
        btn.gameObject.SetActive(false);
        contar = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(contar)
        {
            switch(numero)
            {
                case 0: 
                    Debug.Log("Terminado - Salte de escena"); 
                break;
                case 1: 
                    img.sprite = spNumeros[0];
                    t.text = "1";
                break;
                case 2: 
                    img.sprite = spNumeros[1];
                    t.text = "2";
                break;
                case 3: 
                    img.sprite = spNumeros[2];
                break;
            }
            StartCoroutine(Esperar());
            contar = false;
            numero--;
        }
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1);
        contar = true;
    }

}
