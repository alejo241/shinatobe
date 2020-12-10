using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usuario
{
    int nivelesSuperados;

    public Usuario(int nivelesSuperados)
    {
        this.nivelesSuperados = nivelesSuperados;
    }

    public int NivelesSuperados
    {
        get
        {
            return nivelesSuperados;
        }
        set
        {
            nivelesSuperados = value;
        }
        
    }




}
