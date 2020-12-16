using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;
using TMPro;


public class ControlJuego : MonoBehaviour
{
    static public int nivelesDesbloqueados;
    public int nivelActual;
    public Button[] botonesMenu;
    public TMP_Text[] puntuacionesMax;
    public static int Enemigos;
    public GameObject[] dinosVivos;
    public GameObject[] meteoritosRestantes;
    public static int Meteoritos;
    public GameObject menuPerder;
    public GameObject menuGanar;
    public bool seguir = true;    
    public bool restarVida = true;
    public GameObject victoriaFinal;


    static public string userid;

    //base de datos
    [Header("Database")]
    DatabaseReference reference;
    DataSnapshot snapshot;
    FirebaseDatabase database;


    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
      
            if (SceneManager.GetActiveScene().name != "Login")
            {
                FirebaseDatabase.DefaultInstance.GetReference("users").Child(userid).GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        snapshot = task.Result;
                    }
                });
            }             

    }

    IEnumerator actualizarMenu()
    {
        yield return new WaitForSeconds(0f);
        if (snapshot.HasChild("nivelesDesbloqueados"))
        {
            nivelesDesbloqueados = int.Parse(snapshot.Child("nivelesDesbloqueados").GetRawJsonValue());
            int indiceNivel = 2;
           
            for (int i = 0; i < nivelesDesbloqueados - 1; i++)
            {
                puntuacionesMax[i].SetText(snapshot.Child(indiceNivel.ToString()).Child("puntuacionMaxima").GetRawJsonValue().ToString());
                indiceNivel++;
            }
            
        }
       
      
        
        


    }

    // Start is called before the first frame update
    void Start()
    {
        if (dinosVivos != null)
        {
            Enemigos = dinosVivos.Length;

        }

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            StartCoroutine(actualizarMenu());

        }



    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
           
            ActualizarBotonesMenu();           
        }


        if (dinosVivos.Length > 0)
        {

            if (comprobarDinosMuertos() == dinosVivos.Length)
            {

                if (seguir)
                {
                    seguir = false;
                    StartCoroutine(victoria());

                   

                }
            }
        }

       
            if (meteoritosRestantes != null)
            {

                if (meteoritosRestantes.Length > 0)
                {
                if (comprobarDinosVivos() > 0)
                {
                    int c = 0;
                    for (int i = 0; i < meteoritosRestantes.Length; i++)
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
        
      
     
    }

    private int comprobarDinosMuertos()
    {

        int c = 0;
        if (dinosVivos != null)
        {    
            for (int i = 0; i < dinosVivos.Length; i++)
            {

                if (dinosVivos[i] == null)
                {
                    c++;
                }
            }
        }

        return c;
    }

    private int comprobarDinosVivos()
    {

        int c = 0;
        if (dinosVivos != null)
        {
            if (dinosVivos.Length > 0)
            {

                for (int i = 0; i < dinosVivos.Length; i++)
                {

                    if (dinosVivos[i] != null)
                    {
                        c++;
                        
                    }

                }
            }
        }

        return c;
    }
    IEnumerator victoria()
    {
        yield return new WaitForSeconds(1.5f);
        menuGanar.SetActive(true);
        DesbloquearNivel();
        if(nivelActual == 6)
        {           
            victoriaFinal.SetActive(true);
        }
        ActualizarNivelesDesbloqueados();
        if (snapshot.HasChild(nivelActual.ToString()))
        {
            if (leerPuntuacion() < actualizarPuntuacion.puntuacion)
            {
                ActualizarPuntuacion();
            }

        }
        else
        {
            ActualizarPuntuacion();
        }








    }



    IEnumerator derrota()
    {
        yield return new WaitForSeconds(1.5f);
        menuPerder.SetActive(true);
        if (restarVida)
        {
            actualizarVida.vida--;
            restarVida = false;
        }
        if(actualizarVida.vida == 0)
        {
            volverMenu();

            actualizarVida.vida = 4;
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
        if(nivelesDesbloqueados > botonesMenu.Length)
        {
            nivelesDesbloqueados = botonesMenu.Length;
            for (int i = 0; i < nivelesDesbloqueados; i++)
            {
                if (botonesMenu[i] != null)
                {

                    botonesMenu[i].interactable = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < nivelesDesbloqueados; i++)
            {
                if (botonesMenu[i] != null)
                {

                    botonesMenu[i].interactable = true;
                }
            }
        }
       
    }

 

    public void DesbloquearNivel()
    {

        if (nivelesDesbloqueados < nivelActual)
        {
            nivelesDesbloqueados = nivelActual;
            ActualizarNivelesDesbloqueados();         
          
        }

    }

    private int leerPuntuacion()
    {
        return int.Parse(snapshot.Child(nivelActual.ToString()).Child("puntuacionMaxima").GetRawJsonValue().ToString());
    }

    public void ActualizarPuntuacion()
    {

        Dictionary<string, object> puntuacionNivel = new Dictionary<string, object>();
        puntuacionNivel.Add("puntuacionMaxima", actualizarPuntuacion.puntuacion);
        Dictionary<string, object> test = new Dictionary<string, object>();
        test.Add(nivelActual.ToString(), puntuacionNivel);
        reference.Child("users").Child(userid).Child(nivelActual.ToString()).UpdateChildrenAsync(puntuacionNivel);


    }

    public void ActualizarNivelesDesbloqueados()
    {      
        Dictionary<string, object> nivelesDesbloqueadosDi = new Dictionary<string, object>();
        nivelesDesbloqueadosDi.Add("nivelesDesbloqueados", nivelesDesbloqueados);
        reference.Child("users").Child(userid).UpdateChildrenAsync(nivelesDesbloqueadosDi);

        

    }

    public void volverMenu()
    {
        cambiarNivel(1);
    }

    public void mismoNivel()
    {

        SceneManager.LoadScene(nivelActual);
    }
    public void salirJuego()
    {
        Application.Quit();
    }

}
