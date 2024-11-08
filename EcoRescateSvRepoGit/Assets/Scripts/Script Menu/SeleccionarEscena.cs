using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionarEscena  : MonoBehaviour
{
    public GameObject MenuConfirmarPrefab;
    public void ElegirEscena(int numeroPantallaMenu)
    {
        SceneManager.LoadScene(numeroPantallaMenu);
    }
    public void CerrarJuego()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void AbrirMenuConfirmar()
    {
        if (MenuConfirmarPrefab != null)
        {
            MenuConfirmarPrefab.SetActive(true);
        }
    }

    public void CancelarCierre()
    {
        if (MenuConfirmarPrefab != null)
        {
            MenuConfirmarPrefab.SetActive(false);
        }
    }
}
