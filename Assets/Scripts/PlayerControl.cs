using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 moveVector;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveVector = Vector2.zero;
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVector.x * speed, 0, moveVector.y * speed, ForceMode.VelocityChange);
    }

    //Gets player input
    private void OnMove(InputValue val)
    {
        moveVector = val.Get<Vector2>().normalized;
    }
}
