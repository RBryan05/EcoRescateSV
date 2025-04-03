using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correo : MonoBehaviour
{
    private string email = "bryanrauda05@gmail.com";
    private string subject = "Recomendación ECORescateSV™";
    private string body = "Escribe tu mensaje aquí, se amable.";

    public void AbrirClienteEmail()
    {
        string gmailUrl = $"https://mail.google.com/mail/?view=cm&fs=1&to={email}&su={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
        Application.OpenURL(gmailUrl);
    }
}
