using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColicionConObjetoCorrecto : MonoBehaviour
{
    public GameObject basureroPrefab;
    private float cantidadPuntos = 1;
    public Puntaje puntaje;
    private GameOver gameOver;
    // Start is called before the first frame update
    void Start()
    {
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
            gameOver.PuntajeFinal(cantidadPuntos);
            Destroy(gameObject);
        }

        if (choque.CompareTag("Tilemap"))
        {
            // Verificar si el controladorBasura está asignado y llamar a su método
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
