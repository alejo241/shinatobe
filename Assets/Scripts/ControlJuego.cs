﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using Firebase.Database;
using Firebase;

public class ControlJuego : MonoBehaviour
{


    static public int nivelesDesbloqueados;
    public int nivelActual;
    public Button[] botonesMenu;
    public static int Enemigos;
    public GameObject[] dinosVivos;
    public GameObject[] meteoritosRestantes;
    public static int Meteoritos;
    public GameObject menuPerder;
    public GameObject menuGanar;
    public bool seguir = true;
    


    static public string userid;

    //base de datos
    [Header("Database")]
    DatabaseReference reference;


    private void Awake()
    {
        
            reference = FirebaseDatabase.DefaultInstance.RootReference;

            if (SceneManager.GetActiveScene().name == "Menu")
            {
                FirebaseDatabase.DefaultInstance.GetReference("users").Child(userid).GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {

                        DataSnapshot snapshot = task.Result;


                        nivelesDesbloqueados = int.Parse(snapshot.Child("nivelesSuperados").GetRawJsonValue());
                        ActualizarBotonesMenu();
                        
                    }
                });
            }
        
       

    }

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
            Debug.Log(nivelesDesbloqueados.ToString());  
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

                        if (seguir)
                        {

                            
                            DesbloquearNivel();
                            menuGanar.SetActive(true);
                        }
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
        menuPerder.SetActive(true);
        
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
            seguir = false;
            Usuario usuario = new Usuario(nivelesDesbloqueados);
            string json = JsonUtility.ToJson(usuario);
            var prueba = reference.Child("users").Child(userid).SetRawJsonValueAsync(json); 
        }




    }

    public void volverMenu()
    {
        cambiarNivel(0);
    }

    public void mismoNivel()
    {

        SceneManager.LoadScene(nivelActual);
    }
}
