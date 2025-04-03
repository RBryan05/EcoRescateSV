using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MostrarMenufelicidades : MonoBehaviour
{
    private float puntos;
    public GameObject btnConfirmarEnviarPuntaje;
    public TMP_InputField NombreJugador;

    private ApiService apiService;
    public GameObject Felicitaciones;

    public Text PuntajeAGuardar;
    public Text GameMode;
    public string ModoDeJuego;

    public GameObject Exito;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener la instancia de ApiService
        apiService = ApiService.Instance;
        if (apiService == null)
        {
            Debug.LogError("ApiService no encontrado. Asegúrate de que ApiService está en la escena.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Lista de palabras prohibidas con variaciones en caracteres
        List<string> palabrasProhibidas = new()
    {
        "tonto", "idiota", "estupido", "maldito", "puto", "mierda", "cabron", "hdp",
        "pene", "culo", "caca", "sexo", "violacion", "asesino", "droga", "cocaina",
        "heroina", "crack", "maricon", "zorra", "perra", "chingar", "chingada",
        "pija", "forro", "mierdoso", "imbecil", "mongolico", "cerdo", "basura",
        "imbécil", "subnormal", "degenerado", "puta", "coño", "joder", "gilipollas",
        "pajero", "tarado", "gil", "pelotudo", "cabrón", "pendejo", "boludo", "huevon",
        "hijueputa", "malparido", "mamon", "baboso", "cornudo", "idiotazo", "tarugo",
        "ladron", "delincuente", "traficante", "prostituta", "proxeneta", "violador",
        "pedofilo", "zoofilico", "sodomita", "homicida", "terrorista", "suicida",
        "diabolico", "satanico", "demonio", "racista", "nazi", "fascista", "homofobo",
        "machista", "xenofobo", "dictador", "mafioso", "extorsionador", "secuestrador",
        "asesino serial", "maton", "pandillero", "narco", "camello", "acoso", "acosador",
        "abusador", "maltratador", "necrofilico", "pornografico", "sadico", "masoquista",
        "traficante", "adicto", "alcoholico", "borracho", "fumon", "drogadicto", "pastillero",
        "vicioso", "corrupto", "estafador", "mentiroso", "ladrón", "roba", "robo", "robar",
        "pijudo", "putito", "putita", "puton", "putona", "putazo", "putazo", "putazo",
        "penudo", "vulva", "vagina", "concha", "vaginuda", "marero", "ms", "salvatrucha", "vergon",
        "pipian", "marimacho", "marica", "mariquita"
    };

        // Diccionario con caracteres especiales usados para evadir censura
        Dictionary<char, string> reemplazos = new()
    {
        { 'a', "@áàâäãå4q" }, { 'e', "3éèêë€" }, { 'i', "1!íìîï|" }, { 'o', "0óòôöõø" },
        { 'u', "üúùûv" }, { 's', "$5ß" }, { 'c', "ç¢©" }, { 'g', "9" }, { 't', "7+" }
    };

        // Genera patrones regex para detectar variaciones de cada palabra prohibida
        List<Regex> patrones = palabrasProhibidas.Select(palabra =>
        {
            string patron = palabra.ToLower();
            foreach (var kvp in reemplazos)
            {
                patron = patron.Replace(kvp.Key.ToString(), $"[{kvp.Key}{kvp.Value}]+");
            }
            return new Regex(patron, RegexOptions.IgnoreCase);
        }).ToList();

        // Verifica si el nombre es válido
        bool nombreValido = !string.IsNullOrEmpty(NombreJugador.text) &&
                            !patrones.Any(regex => regex.IsMatch(NombreJugador.text));

        // Activa o desactiva el botón según el nombre ingresado
        btnConfirmarEnviarPuntaje.SetActive(nombreValido);

        PuntajeAGuardar.text = puntos.ToString("0");
        GameMode.text = ModoDeJuego;
    }

    public void MostrarMenuSubirPuntaje()
    {
        List<Jugador> jugadores = apiService.ObtenerJugadores();

        var topJugadores = jugadores
            .OrderByDescending(j => int.Parse(j.Puntaje))
            .Take(3)
            .ToList();

        int puntajeFinalInt = int.Parse(PuntajeAGuardar.text);
        int puntajeTop1 = int.Parse(topJugadores[0].Puntaje);
        int puntajeTop2 = int.Parse(topJugadores[1].Puntaje);
        int puntajeTop3 = int.Parse(topJugadores[2].Puntaje);

        Debug.Log(topJugadores);

        // Comparar si el puntaje final es mayor que alguno de los 3 primeros
        if (puntajeFinalInt > puntajeTop1 || puntajeFinalInt > puntajeTop2 || puntajeFinalInt > puntajeTop3)
        {
            Felicitaciones.SetActive(true);
        }
    }

    public void OcultarMenusubirPuntaje()
    {
        Felicitaciones.SetActive(false);
    }

    public void GuardarPuntaje()
    {
        Debug.Log("¡Guardando puntaje!");
        string nombre = NombreJugador.text;
        string puntaje = PuntajeAGuardar.text;
        string modo = GameMode.text;

        Debug.Log("Guardando jugador: " + nombre + " - " + puntaje + " - " + modo);

        apiService.GuardarJugador(nombre, puntaje, modo, (exito) =>
        {
            if (exito)
            {
                Debug.Log("Jugador guardado exitosamente.");
                OcultarMenusubirPuntaje();
                Exito.SetActive(true);
            }
            else
                Debug.LogError("Error al guardar el jugador.");
        });
    }

    public void PuntajeFinalAGuardar(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }

    // Llamar a esta función cuando termine el juego
    public void JuegoTerminado()
    {
        MostrarMenuSubirPuntaje();
    }
}
