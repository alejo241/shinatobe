using System.Collections;
using UnityEngine;
using System;

public class manolitoScript : MonoBehaviour
{
    public Transform puntoMedio;
    public float rangoTirachinas;
    public float velocidadMaxima;  
    public Boolean meteorito;
    public GameObject siguienteMeteorito;
    GameObject meteoritoActual;


    bool lanzado = false;
    Vector3 dis;
    Rigidbody2D rb;
 

    private void Awake()
    {        
        lanzado = false;
    }

    void Start()
    {
        meteoritoActual = this.gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;     
       
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

            StartCoroutine(destruirMeteorito());
           
                
            }
        

       

    }

    bool canDrag = true;
   
    private void OnMouseDrag()
    {
        if (!canDrag)
            return;

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        dis = pos - puntoMedio.position;//posicion relativa respecto al punto maximo
        dis.z = 0; // la z sobra porque el juego es en 2d

        if (dis.magnitude > rangoTirachinas)
        {
            dis = dis.normalized * rangoTirachinas;
        }

        transform.position = dis + puntoMedio.position;
    }

    private void OnMouseUp()
    {

        if (!canDrag)
            return;
        canDrag = false;
        lanzado = true;
      

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


    }

    IEnumerator destruirMeteorito()
    {
        yield return new WaitForSeconds(2.0f);
        
        Destroy(meteoritoActual);
        


    }
}
