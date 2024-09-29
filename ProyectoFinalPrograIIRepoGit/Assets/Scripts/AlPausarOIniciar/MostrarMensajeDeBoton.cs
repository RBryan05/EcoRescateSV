using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarMensajeDeBoton : MonoBehaviour
{
    public GameObject Mensaje;

    public void MostrarMensaje()
    {
        Mensaje.SetActive(true);
    }

    public void OcultarMensaje()
    {
        Mensaje.SetActive(false);
    }
}
