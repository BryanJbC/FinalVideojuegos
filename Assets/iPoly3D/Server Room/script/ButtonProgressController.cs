using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonProgressController : MonoBehaviour
{
    public GameObject canvas; // El Canvas que contiene el progreso
    public TMP_Text progressText; // El texto que muestra el progreso del jugador
    private int buttonsPressed = 0;
    private int totalButtons = 3;

    void Start()
    {
        // Inicializamos el texto del Canvas
        UpdateProgressText();
    }

    // Llamar este método cuando se presiona un botón
    public void OnButtonPressed()
    {
        buttonsPressed++;

        // Actualizamos el texto del Canvas
        UpdateProgressText();

        // Si todos los botones han sido presionados, ocultamos el Canvas
        if (buttonsPressed == totalButtons)
        {
            canvas.SetActive(false);
        }
    }

    // Actualizamos el texto del progreso en el Canvas
    private void UpdateProgressText()
    {
        progressText.text = "Botones presionados: " + buttonsPressed + "/" + totalButtons;
    }
}

