using System.Collections;
using UnityEngine;
using System;

public class manolitoScript : MonoBehaviour
{
    public Transform puntoMaximo;
    public float rangoTirachinas;
    public float velocidadMaxima;
    public static int MeteoritosRestantes = 0;
    public Boolean meteorito;
    public GameObject siguienteMeteorito;


    bool lanzado = false;
    Vector3 dis;
    Rigidbody2D rb;
    ControlJuego controlJuego;


    private void Awake()
    {
        controlJuego = GameObject.Find("ControlJuego").GetComponent(typeof(ControlJuego)) as ControlJuego;
        lanzado = false;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
     
        if (meteorito)
        {
            MeteoritosRestantes++;     
        }
        if (siguienteMeteorito != null)
        {
            siguienteMeteorito.SetActive(false);
        }
       
     

    }

    // Update is called once per frame
    void Update()
    {

      
            if(rb.velocity.x < 0.1 && rb.velocity.y == 0 && lanzado)
            {
                Destroy(gameObject);
                
                if(MeteoritosRestantes == 0)
                {
                controlJuego.volverMenu();
                }
            }
        

       

    }

    bool canDrag = true;
   
    private void OnMouseDrag()
    {
        if (!canDrag)
            return;

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        dis = pos - puntoMaximo.position;//posicion relativa respecto al punto maximo
        dis.z = 0; // la z sobra porque el juego es en 2d

        if (dis.magnitude > rangoTirachinas)
        {
            dis = dis.normalized * rangoTirachinas;
        }

        transform.position = dis + puntoMaximo.position;
    }

    private void OnMouseUp()
    {

        if (!canDrag)
            return;
        canDrag = false;
        lanzado = true;
        MeteoritosRestantes--;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = -dis.normalized * velocidadMaxima *  (dis.magnitude / rangoTirachinas);//para lanzar el meteorito
       

      
        if(siguienteMeteorito != null)
        {
            StartCoroutine(Release());
        }
        
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(0.5f);
        siguienteMeteorito.SetActive(true);
        //Destroy(gameObject);


    }
}
