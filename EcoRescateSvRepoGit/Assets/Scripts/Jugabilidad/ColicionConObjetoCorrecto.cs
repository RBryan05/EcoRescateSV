using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColicionConObjetoCorrecto : MonoBehaviour
{
    public GameObject basureroPrefab;
    private float cantidadPuntos = 1;
    public Puntaje puntaje;
    private GameOver gameOver;
    private MostrarMenufelicidades mostrarMenuFelicidades;

    // Start is called before the first frame update
    void Start()
    {
        mostrarMenuFelicidades = FindObjectOfType<MostrarMenufelicidades>();
        gameOver = FindObjectOfType<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D choque)
    {
        if (choque.CompareTag("Player"))
        {
            puntaje.SumarPuntos(cantidadPuntos);           
            Destroy(gameObject);
            gameOver.PuntajeFinal(cantidadPuntos);
            mostrarMenuFelicidades.PuntajeFinalAGuardar(cantidadPuntos);
        }

        if (choque.CompareTag("Tilemap"))
        {
            // Verificar si el controladorBasura est� asignado y llamar a su m�todo
            if (basureroPrefab != null)
            {
                if (basureroPrefab.TryGetComponent<MoverBasurero>(out var basurero))
                {
                    basurero.PerderVida();
                }
            }

            Destroy(gameObject);
        }
    }
}
