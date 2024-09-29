using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionarEscena  : MonoBehaviour
{
    public void ElegirEscena(int numeroPantallaMenu)
    {
        SceneManager.LoadScene(numeroPantallaMenu);
    }
}
