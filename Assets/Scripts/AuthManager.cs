using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;



public class AuthManager : MonoBehaviour
{

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;

    //Login variables
    [Header("Login")]
    public TMP_InputField inputFieldCorreo;
    public TMP_InputField inputFieldContraseña;
   
    //Register variables
    [Header("Register")]
    public TMP_InputField inputFieldRegistroCorreo;
    public TMP_InputField inputFieldRegistroContraseña;
    public TMP_InputField inputFieldRegistroContraseñaConfirm;


    //Otras variables
    [Header("Otras variables")]
    public ControlJuego controlJuego;
    public TMP_Text error;

    void Awake()
    {
        //comprobar que existen todas las dependencias de firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //si existen
                auth = FirebaseAuth.DefaultInstance;
            }
            else
            {
                Debug.LogError("No estan disponible las dependencias: " + dependencyStatus);
            }
        });
    }

    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login());
    }

    private IEnumerator Login()
    {
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(inputFieldCorreo.text, inputFieldContraseña.text);
        
        //esperar a que la tarea esté completa
        yield return new WaitUntil(() => LoginTask.IsCompleted);


        //si no hay errores
        if(LoginTask.Exception == null)
        {
            controlJuego.volverMenu();
        }
        else
        {
            Debug.Log("Usuario y/o contraseña incorrecta");
        }

    }

    public void registroButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Registro());
    }

    private IEnumerator Registro()
    {
        var registroTask = auth.CreateUserWithEmailAndPasswordAsync(inputFieldRegistroCorreo.text, inputFieldRegistroContraseña.text);

        //esperar a que la tarea esté completa
        yield return new WaitUntil(() => registroTask.IsCompleted);

        //si las contraseñas son iguales
        if (inputFieldRegistroContraseña.text.Equals(inputFieldRegistroContraseñaConfirm.text))
        {
            //si no hay errores
            if (registroTask.Exception == null)
            {
                controlJuego.volverMenu();
            }
            else
            {
                Debug.Log("task error");
            }

        }
        else
        {
            Debug.Log("pass error");
        }
    }    
      

    



}
