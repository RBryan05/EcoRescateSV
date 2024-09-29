using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoverBasurero : MonoBehaviour
{
    private bool seMueve = false;  // Indica si el jugador está siendo arrastrado
    public float velocidad = 35f;
    private int vidas = 3;
    private ManejarVidas manejarVidas;
    private SeleccionarEscena detenerJuego;
    private GameOver gameOverCodigo;
    void Start()
    {
        gameOverCodigo = FindAnyObjectByType<GameOver>();
        manejarVidas = FindObjectOfType<ManejarVidas>();
        detenerJuego = FindObjectOfType<SeleccionarEscena>();
    }

    public void MoverseConMouse()
    {
        // Detectar si el mouse está sobre el personaje y haces clic
        // Detectar clic en el personaje
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(posicionMouse);

            if (collider != null && collider.gameObject == this.gameObject)
            {
                seMueve = true;
            }
        }

        // Si se suelta el clic
        if (Input.GetMouseButtonUp(0))
        {
            seMueve = false;
        }

        // Mover el personaje mientras se arrastra
        if (seMueve)
        {
            // Obtener la posición del mouse en el espacio del mundo
            Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Mantener la posición 'y' fija en -19.590f y 'z' actual
            Vector3 posicionNueva = new Vector3(posicionMouse.x, -19.590f, transform.position.z);

            // Limitar la posición horizontal entre -5.2 y 16.99
            posicionNueva.x = Mathf.Clamp(posicionNueva.x, -5.2f, 16.99f);

            // Asignar la nueva posición al objeto, sin depender de la posición original
            transform.position = posicionNueva;
        }

    }
    public void MoverseConTeclado()
    {
        // Codigo para poder mover el basurero horizontalmente
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float nuevaPosicionX = transform.position.x + (movimientoHorizontal * velocidad * Time.deltaTime);

        // Limitar la posición horizontal entre -5.2 y 16.99
        nuevaPosicionX = Mathf.Clamp(nuevaPosicionX, -5.2f, 16.99f);

        // Posicion constante en y
        transform.position = new Vector3(nuevaPosicionX, -19.59f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        MoverseConTeclado(); 
        MoverseConMouse();
    }

    public void PerderVida()
    {
        vidas -= 1;    
        manejarVidas.DesactivarVida(vidas);
        if (vidas == 0)
        {
            Destroy(gameObject);
            // EditorApplication.isPlaying = false; // Detiene el juego en el Editor
            gameOverCodigo.JuegoTerminado();

        }
        Debug.Log($"Vidas = {vidas}");
    }
}
