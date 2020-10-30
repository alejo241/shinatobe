using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actualizarPuntuacion : MonoBehaviour
{
    Text txtPuntuacion;
    public static int puntuacion;


    // Start is called before the first frame update
    void Start()
    {
        txtPuntuacion = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        txtPuntuacion.text = puntuacion.ToString();
        

    }


   
}
