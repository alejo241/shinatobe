﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEditor.PackageManager;
using Firebase.Storage;

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

    //base de datos
    [Header("Database")]
    private FirebaseStorage firebase;



    //Otras variables
    [Header("Otras variables")]
    public ControlJuego controlJuego;
    Usuario usuario;
    
  

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
            errorLogin.text = "Usuario y/o contraseña incorrecta";
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
            errorRegistro.text = "Las contraseñas no coinciden";
            Debug.Log("pass error");
        }
    }    
      

    



}
