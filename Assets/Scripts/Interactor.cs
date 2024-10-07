using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Interactable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteracterSource;
    public float InteractRange;

    private GameObject interactText;

    private void Start()
    {
        interactText = GameObject.Find("InteractText");
    }

    void Update()
    {
        Ray r = new(InteracterSource.position, InteracterSource.forward);  // forward interaction

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Interactable interactObj))
            {
                interactText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                interactText.SetActive(false);
            }
        }
    }
}
