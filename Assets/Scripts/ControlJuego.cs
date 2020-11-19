using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class ControlJuego : MonoBehaviour
{


    static public int nivelesDesbloqueados;
    public int nivelActual;
    public Button[] botonesMenu;
    public int MeteoritosRestantes;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            ActualizarBotonesMenu();
        }

    }

    public void cambiarNivel(int nivel)
    {
        if(nivel == 0)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene(nivel);
        }
        
    }

    public void ActualizarBotonesMenu() 
    {
        for (int i = 0; i < nivelesDesbloqueados+1; i++)
        {
            botonesMenu[i].interactable = true;
        }
    }

    public void DesbloquearNivel()
    {
        if (nivelesDesbloqueados < nivelActual)
        {
            nivelesDesbloqueados = nivelActual;
            nivelActual++;
        }
        volverMenu();
    }

    public void volverMenu()
    {
        cambiarNivel(0);
    }
}
