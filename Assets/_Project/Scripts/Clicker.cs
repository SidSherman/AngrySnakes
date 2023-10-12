using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Clicker : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    private GameObject clickedObject;
    void Awake()
    {
        _Camera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(clickedObject)
            Gizmos.DrawSphere(clickedObject.transform.position, 1);
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (Time.timeScale != 0)
            {

                Vector3 mousePosition = mouse.position.ReadValue();
                Ray ray = _Camera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log(hit.collider.gameObject);

                    clickedObject = hit.collider.gameObject;
                    
                    if(Time.timeScale == 0)
                        return;
                    if (hit.collider.gameObject.TryGetComponent(out EnemyBase enemy))
                    {
                        if(!enemy.IsDead) enemy.Death();
                    }
                }
            }
        }
    }

 
}