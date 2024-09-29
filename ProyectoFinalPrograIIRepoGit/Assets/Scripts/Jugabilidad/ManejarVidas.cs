using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejarVidas : MonoBehaviour
{
    public GameObject[] vidas;
    private AudioSource audioVidaMenos;
    // Start is called before the first frame update
    void Start()
    {
        audioVidaMenos = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DesactivarVida(int indice)
    {
        audioVidaMenos.Play();
        vidas[indice].SetActive(false);
    }
}
