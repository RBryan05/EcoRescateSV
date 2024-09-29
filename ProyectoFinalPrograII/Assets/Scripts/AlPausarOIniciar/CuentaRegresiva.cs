using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CuentaRegresiva : MonoBehaviour
{
    public GameObject uno;
    public GameObject dos;
    public GameObject tres;
    public GameObject btnPausa;
    private PausarYReanudar _pausarYReanudar;

    private void Start()
    {
        _pausarYReanudar = FindAnyObjectByType<PausarYReanudar>();

        // Asegurarse de que todo esté desactivado al inicio
        uno.SetActive(false);
        dos.SetActive(false);
        tres.SetActive(false);
    }


    // Corutina para manejar la cuenta regresiva
    public IEnumerator IniciarCuentaRegresiva()
    {
        btnPausa.SetActive(false);
        // Mostrar el número "3"
        tres.SetActive(true);
        uno.SetActive(false);
        dos.SetActive(false);
        Debug.Log("Mostrando 3");
        yield return new WaitForSecondsRealtime(1);  // Espera 1 segundo

        // Mostrar el número "2"
        tres.SetActive(false);
        dos.SetActive(true);
        uno.SetActive(false);
        Debug.Log("Mostrando 2");
        yield return new WaitForSecondsRealtime(1);  // Espera 1 segundo

        // Mostrar el número "1"
        dos.SetActive(false);
        uno.SetActive(true);
        Debug.Log("Mostrando 1");
        yield return new WaitForSecondsRealtime(1);  // Espera 1 segundo

        // Ocultar todo y reanudar el juego
        uno.SetActive(false);
        dos.SetActive(false);
        tres.SetActive(false);
        Debug.Log("Reanudar juego");

        _pausarYReanudar.Reanudar();
        btnPausa.SetActive(true);
    }
}
