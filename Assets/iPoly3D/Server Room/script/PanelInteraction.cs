using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PanelInteraction : MonoBehaviour
{
    public GameObject canvas; // Canvas para mostrar el código
    public TMP_Dropdown dropdownGreen;  // Campo para el TMP_Dropdown verde
    public TMP_Dropdown dropdownOrange; // Campo para el TMP_Dropdown naranja
    public TMP_Dropdown dropdownYellow; // Cambiar a Dropdown
    public Animator doorAnimator; // La puerta con la animación de abrir
    private bool isCodeCorrect = false;
    public TMP_InputField inputField;
    private bool doorOpened = false; // Añadimos esta variable para controlar el estado de la puerta

    void Start()
    {
        // Al inicio, desactivar el canvas
        canvas.SetActive(false);
    }

    // Llamar este método cuando el panel sea presionado
    public void OnPanelClicked()
    {
        // Solo mostrar el canvas si la puerta no ha sido abierta aún
        if (!doorOpened)
        {
            canvas.SetActive(true); // Mostrar el canvas
            PositionCanvasInFrontOfPlayer(); // Opcional, ver más abajo
        }
    }

    // Verificar el código ingresado por el jugador
    public void CheckCode()
    {
        // Variables para almacenar los valores seleccionados del dropdown
        string greenText = dropdownGreen.options[dropdownGreen.value].text;
        string orangeText = dropdownOrange.options[dropdownOrange.value].text;
        string yellowText = dropdownYellow.options[dropdownYellow.value].text;
        int greenValue = int.Parse(greenText);
        int orangeValue = int.Parse(orangeText);
        int yellowValue = int.Parse(yellowText);

        // Verificar si el código es correcto
        if (greenValue == 3 && orangeValue == 5 && yellowValue == 8)
        {
            isCodeCorrect = true;
            canvas.SetActive(false); // Esconder el canvas al ingresar el código correcto
            OpenDoor(); // Llamar a la función para abrir la puerta
        }
        else
        {
            Debug.Log("Código incorrecto");
            // Código incorrecto, esconder el canvas pero permitir que se pueda volver a abrir
            canvas.SetActive(false); // Esconder el canvas
        }
    }

    public void OnButtonNumberClick(string number)
    {
        inputField.text += number;  // Agrega el número al campo de entrada
    }

    // Método para abrir la puerta solo si no ha sido abierta
    private void OpenDoor()
    {
        if (!doorOpened && isCodeCorrect) // Solo abrir si no ha sido abierta y el código es correcto
        {
            doorAnimator.SetBool("isOpen", true); // Activar la animación de apertura de la puerta
            doorOpened = true; // Marcar la puerta como abierta
        }
    }

    // Opcional: Colocar el canvas frente al jugador
    private void PositionCanvasInFrontOfPlayer()
    {
        Camera playerCamera = Camera.main; // O la cámara del jugador
        canvas.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 2f; // Ajustar distancia frente al jugador
        canvas.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
    }
}



