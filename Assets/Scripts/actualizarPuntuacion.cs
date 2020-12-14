using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class actualizarPuntuacion : MonoBehaviour
{
    public TMP_Text txtPuntuacion;
    public static int puntuacion;


    // Start is called before the first frame update
    void Start()
    {
        //txtPuntuacion = GetComponent<Text> ();
        puntuacion = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        txtPuntuacion.text = "Score: " + puntuacion.ToString();
        //prueba de github

    }


   
}
