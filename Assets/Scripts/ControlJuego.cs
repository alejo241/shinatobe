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
    public static int Enemigos;
    public GameObject[] dinosVivos;
    public GameObject[] meteoritosRestantes;
    public static int Meteoritos;

    // Start is called before the first frame update
    void Start()
    {
        if(dinosVivos != null)
        {
            Enemigos = dinosVivos.Length;
        }
       
      
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            ActualizarBotonesMenu();
        }

        if(dinosVivos != null)
        {
            if (dinosVivos.Length > 0)
            {
                int c = 0;
                for (int i = 0; i < dinosVivos.Length; i++)
                {

                    if (dinosVivos[i] == null)
                    {
                        c++;
                    }

                    if (c == dinosVivos.Length)
                    {
                        DesbloquearNivel();
                    }
                }
            }
        }
 
        if(meteoritosRestantes != null)
        {
            if (meteoritosRestantes.Length > 0)
            {
                int c = 0;
                for (int i = 0; i < dinosVivos.Length; i++)
                {

                    if (meteoritosRestantes[i] == null)
                    {
                        c++;
                    }

                    if (c == meteoritosRestantes.Length)
                    {

                        StartCoroutine(derrota());

                    }
                }
            }
        }
     
    }

    IEnumerator derrota()
    {
        yield return new WaitForSeconds(2.0f);

        volverMenu();
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
