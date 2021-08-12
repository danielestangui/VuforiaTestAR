using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModelController : MonoBehaviour
{

    /// <summary>
    /// PENDIENTE:
    /// - Mejorar la captura de los inputs: El usuario tiene que poder cambiar el sentido o crecimiento sin dejar de pulsar la pantalla.
    /// - Separar los controles de las acciones: Hacer los controles inpendientes par aañir nuevas funcionalidades a dichos contoles.
    /// - Iluminacion: Aadir funcionalidades para poder cambiar la iluminación del objeto.
    /// - Probar a hacer el esclado y el giro con FixedUpdate
    /// </summary>


    private const float minScale = 0.5f;
    private const float maxScale = 2f;

    public UnityEngine.Transform _modelTransform;

    // Touches variables //
    private float rotationSensivity = 10f;
    private float scaleSensivity = 10f;
    private Vector2 startPosition;
    private Vector2 actualPosition;
    private float distanceBwTouches;

    private void Start()
    {

        GameObject itemObject;

        if (ItemObjectController.itemObject != null)
        {
            itemObject = Instantiate(ItemObjectController.itemObject);
            itemObject.transform.parent = _modelTransform;
        }
        else 
        {
            itemObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            itemObject.transform.parent = _modelTransform;
            itemObject.transform.localScale = Vector3.one * 0.2f;
        } 
    }

    private void Update()
    {
        getInput();
    }

    /// <summary>
    /// Manage all inputs touches from the user
    /// </summary>
    private void getInput() 
    {
        // If users touch back button goes to mainmenuscene
        if (Application.platform == RuntimePlatform.Android) 
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }

        // If the user touche the screen
        if (Input.touchCount > 0) 
        {
            // Rotation //
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase) 
                {
                    case TouchPhase.Began:

                        startPosition = touch.position;

                        break;

                    case TouchPhase.Moved:

                        actualPosition = touch.position;
                        float distance = actualPosition.x - startPosition.x;

                        float distanceRelativeToWidth = distance / Screen.width;
                        Vector3 actualRotation = _modelTransform.rotation.eulerAngles;

                        float angle = actualRotation.y + distanceRelativeToWidth * rotationSensivity;

                        _modelTransform.rotation = Quaternion.Euler(actualRotation.x, angle, actualRotation.z);

                        break;
                }
            }
        }

        // Scale //
        if (Input.touchCount == 2) 
        {

            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            if ((touchOne.phase == TouchPhase.Began) || (touchTwo.phase == TouchPhase.Began))
            {
                distanceBwTouches = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
         
            if ((touchOne.phase == TouchPhase.Moved) || (touchTwo.phase == TouchPhase.Moved)) 
            {
                float actualDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                float distDif = actualDistance - distanceBwTouches;

                float actualScale = _modelTransform.localScale.x;
                float distanceRelativeToWidth = distDif / Screen.width;
                float newScale = actualScale + distanceRelativeToWidth * scaleSensivity;
                newScale = Mathf.Clamp(newScale, minScale, maxScale);

                _modelTransform.localScale = Vector3.one * newScale;
            }
        }
    }
}
