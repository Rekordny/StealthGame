using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject restartPane;
    private bool _gameOver = false;
    [SerializeField]
    public bool gameOver
    {
        get
        {
            return _gameOver;
        }
        set
        {
            _gameOver = value;
            if (_gameOver)
            {
                Debug.Log("Game Over!");
                restartPane.SetActive(true);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("SRTGame Over!");
            gameOver = true;
        }
    }

    void Update()
    {
        // Get input from keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get mouse pos
        Ray mouseRayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePosition = Vector3.zero;
        if (Physics.Raycast(mouseRayFromCamera, out RaycastHit hitInfo))
        {
            mousePosition = hitInfo.point;
        }
        mousePosition.y = transform.position.y; // ensure height

        // calculate direction
        Vector3 direction = (mousePosition - transform.position).normalized;

        if (!gameOver)
        {
            // rotate
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }

            // move player
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
