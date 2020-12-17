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
    public FirebaseUser userFU;

    //Login variables
    [Header("Login")]
    public TMP_InputField inputFieldCorreo;
    public TMP_InputField inputFieldContraseña;
    public TMP_Text errorLogin;

    //Register variables
    [Header("Register")]
    public TMP_InputField inputFieldRegistroCorreo;
    public TMP_InputField inputFieldRegistroContraseña;
    public TMP_InputField inputFieldRegistroContraseñaConfirm;
    public TMP_Text errorRegistro;

 





//Otras variables
[Header("Otras variables")]
    public ControlJuego controlJuego;
    
  

    void Awake()
    {
        errorLogin.text = "";
        errorRegistro.text = "";

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
        errorLogin.text = "";
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
            ControlJuego.userid = auth.CurrentUser.UserId;
            controlJuego.volverMenu();
         
            
        }
        else
        {
            errorLogin.text = "Usuario y/o contraseña incorrecta";
            Debug.Log("Usuario y/o contraseña incorrecta");
        }

    }

    public void registroButton()
    {

        errorRegistro.text = "";
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
                ControlJuego.userid = auth.CurrentUser.UserId;
                controlJuego.volverMenu();
            }
            else
            {
                if (registroTask.Exception.GetBaseException().Message.Equals("The email address is already in use by another account."))
                {
                    errorRegistro.text = "Este correo  ya está en uso";
                }else if (registroTask.Exception.GetBaseException().Message.Equals("The given password is invalid."))
                {
                    errorRegistro.text = "Contraseña invalida";
                }
                else
                {
                    errorRegistro.text = registroTask.Exception.GetBaseException().Message;
                }

                Debug.Log(registroTask.Exception.GetBaseException().Message);
            }

        }
        else
        {
            errorRegistro.text = "Las contraseñas no coinciden";
            Debug.Log("pass error");
        }
    }    
      

    



}
