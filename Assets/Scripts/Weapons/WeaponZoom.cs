using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomOutFOV = 60f;
    [SerializeField] float zoomInFOV = 20f;
    [SerializeField] float zooomOutSens = 2f;
    [SerializeField] float zooomInSens = .5f;


    bool zoomedInToggle = false;

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        fpsCamera.fieldOfView = zoomInFOV;
        fpsController.mouseLook.XSensitivity = zooomInSens;
        fpsController.mouseLook.YSensitivity = zooomInSens;
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        fpsCamera.fieldOfView = zoomOutFOV;
        fpsController.mouseLook.XSensitivity = zooomOutSens;
        fpsController.mouseLook.YSensitivity = zooomOutSens;
    }
}
