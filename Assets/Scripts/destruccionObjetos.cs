using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class destruccionObjetos : MonoBehaviour
{

    public float resistenciaMaterial;
    public GameObject explosion;
    public static int EnemigosVivos = 0;
    public Boolean nivelTerminado = false;
    public Boolean dino; 
    public Text txtPuntuacion;
    ControlJuego controlJuego;
    public GameObject actualDino;
    public GameObject siguientedino;
    public GameObject ultimoDino;




    public void Awake()
    {
        controlJuego = GameObject.Find("ControlJuego").GetComponent(typeof(ControlJuego)) as ControlJuego;
    }

    private void Start()
    {
       
    }

    private void Update()
    {



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.relativeVelocity.magnitude > resistenciaMaterial)
        {

            if (explosion != null)
            {
                var go = Instantiate(explosion, transform.position, Quaternion.identity);

                Destroy(go, 3);

                //EnemigosVivos--;
              
            }

            Destroy(gameObject, 0.1f);

            

         
        }
        else
        {
            resistenciaMaterial = resistenciaMaterial - collision.relativeVelocity.magnitude;
            

            if(gameObject.tag == "dino" && resistenciaMaterial<99 && resistenciaMaterial >= 80)
            {
                siguientedino.GetComponent<destruccionObjetos>().resistenciaMaterial = resistenciaMaterial;
                siguientedino.SetActive(true);
                actualDino.SetActive(false);
                
            }
            if (gameObject.tag == "dino" && resistenciaMaterial < 80)
            {
                ultimoDino.GetComponent<destruccionObjetos>().resistenciaMaterial = resistenciaMaterial;
                ultimoDino.SetActive(true);
                actualDino.SetActive(false);
                

            }


        }


    }

    private void OnDestroy()
    {
        if (gameObject.tag == "dino")
        {
            actualizarPuntuacion.puntuacion += 25000;
        }
        else
        {
            actualizarPuntuacion.puntuacion += 500;
        }
    }


    public void Destruir(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > resistenciaMaterial)
        {

            if (explosion != null)
            {
                var go = Instantiate(explosion, transform.position, Quaternion.identity);
               actualizarPuntuacion.puntuacion += 500;
                Destroy(go, 3);
             
            }

            Destroy(gameObject, 0.1f);
        }
        else
        {
            resistenciaMaterial = resistenciaMaterial - collision.relativeVelocity.magnitude;
        }
    }

    /*[MethodImpl(MethodImplOptions.Synchronized)]
    public void DestruirDino(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > resistenciaMaterial)
        {

            if (explosion != null)
            {
                var go = Instantiate(explosion, transform.position, Quaternion.identity);

                Destroy(go, 3);

                EnemigosVivos--;
               actualizarPuntuacion.puntuacion += 25000;

                if (EnemigosVivos == 0)
                {
                    controlJuego.DesbloquearNivel();
                }
            }

            Destroy(gameObject, 0.1f);
        }
        else
        {
            resistenciaMaterial = resistenciaMaterial - collision.relativeVelocity.magnitude;

           
        }
    }*/
}
