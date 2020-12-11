using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject loginPantalla;
    public GameObject registroPantalla;
    public TMP_Text errorLogin;
    public TMP_Text errorRegistro;

    public void mostrarPantallaLogin()
    {
        errorLogin.text = "";
        registroPantalla.SetActive(false);
        loginPantalla.SetActive(true);
      
    }

    public void mostrarPantallaRegistro()
    {
        errorRegistro.text = "";
        loginPantalla.SetActive(false);
        registroPantalla.SetActive(true);       
    }

}
