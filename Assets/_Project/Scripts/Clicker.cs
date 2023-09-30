using UnityEngine;
using UnityEngine.InputSystem;
public class Clicker : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    void Awake()
    {
        _Camera = Camera.main;
    }
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = _Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out EnemyBase enemy))
                {
                    enemy.Death();
                }
            }
        }
    }
}