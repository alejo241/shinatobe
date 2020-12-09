using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject loginPantalla;
    public GameObject registroPantalla;

    public void mostrarPantallaLogin()
    {
        registroPantalla.SetActive(false);
        loginPantalla.SetActive(true);
    }

    public void mostrarPantallaRegistro()
    {
        loginPantalla.SetActive(false);
        registroPantalla.SetActive(true);       
    }

}
