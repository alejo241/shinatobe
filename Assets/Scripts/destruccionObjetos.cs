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
   
    

    public void Awake()
    {
        controlJuego = GameObject.Find("ControlJuego").GetComponent(typeof(ControlJuego)) as ControlJuego;
    }

    private void Start()
    {
       
       
        /*if (dino)
        {
            EnemigosVivos = dinosVivos.Length;
        }*/

        

    }

    private void Update()
    {



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //collision.gameObject.tag == "dino"

        /*if (dino)
        {
            DestruirDino(collision);
        }
        else
        {
            Destruir(collision);
        }*/


        if (collision.relativeVelocity.magnitude > resistenciaMaterial)
        {

            if (explosion != null)
            {
                var go = Instantiate(explosion, transform.position, Quaternion.identity);

                Destroy(go, 3);

                //EnemigosVivos--;
              
            }

            Destroy(gameObject, 0.1f);

        

            /*if (ControlJuego.Enemigos == 0)
            {
                controlJuego.DesbloquearNivel();
            }*/
        }
        else
        {
            resistenciaMaterial = resistenciaMaterial - collision.relativeVelocity.magnitude;
        }


    }


    private void OnDestroy()
    {
        if (gameObject.tag.Equals("dino"))
        {
            actualizarPuntuacion.puntuacion += 1000;
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DestruirDino(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > resistenciaMaterial)
        {

            if (explosion != null)
            {
                var go = Instantiate(explosion, transform.position, Quaternion.identity);

                Destroy(go, 3);

                EnemigosVivos--;
               actualizarPuntuacion.puntuacion += 1000;

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
    }
}
