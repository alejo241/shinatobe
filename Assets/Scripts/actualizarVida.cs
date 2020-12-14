using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actualizarVida : MonoBehaviour
{

    public static int vida = 2;
    public Text mostrarVida;
    // Start is called before the first frame update
    void Start()
    {

        mostrarVida = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        mostrarVida.text = vida.ToString();
    }
}
