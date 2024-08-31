using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Theft : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f; 
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Mover();
    }

    private void Mover()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        _rigidbody2D.velocity = movement * _speed;
    }
}