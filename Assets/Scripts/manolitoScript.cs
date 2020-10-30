using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manolitoScript : MonoBehaviour
{
    public Transform puntoMaximo;
    public float rangoTirachinas;
    public float velocidadMaxima;
    public GameObject siguienteMeteorito;


    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

    }

    // Update is called once per frame
    void Update()
    {

    }

    bool canDrag = true;
    Vector3 dis;
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


        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = -dis.normalized * velocidadMaxima *  (dis.magnitude / rangoTirachinas);//para lanzar el meteorito

        if (siguienteMeteorito != null)
        {
            StartCoroutine(Release());
        }
        
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(2f);
        siguienteMeteorito.SetActive(true);
        
    }
}
