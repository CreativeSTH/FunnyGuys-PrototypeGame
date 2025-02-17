using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator playerAnimation;

    private float horizontal, vertical;
    private Vector3 moveDirection;
    private bool isGrounded;
    public bool isJumping; // Indica si el personaje está saltando
    public bool isFalling; // Indica si el personaje está cayendo

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        CheckGrounded();
        CheckFalling();

        // Control de animaciones
        if (moveDirection != Vector3.zero)
        {
            RotateTowardsMovement();
            playerAnimation.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimation.SetBool("IsWalking", false);
        }
    }

    void FixedUpdate()
    {
        ApplyPhysicsMovement();
    }

    private void HandleInput()
    {
        // Entrada básica de movimiento
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Salto si oprimimos Boton Jump (input manager) y está tocando el suelo.
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                ApplyJump();
                isJumping = true;
                isFalling = false;
                playerAnimation.SetBool("IsJumping", true); // Activa la animación de salto inicial
                playerAnimation.SetBool("IsFalling", false);
                playerAnimation.SetBool("IsGrounded", false);
            }else{
                isJumping = false;
                isFalling = false;
                playerAnimation.SetBool("IsJumping", false);
                playerAnimation.SetBool("IsFalling", false);
                playerAnimation.SetBool("IsGrounded", true);
            }

        }
    }

    private void ApplyPhysicsMovement()
    {
        // Movimiento con fuerzas físicas por medio de método MovePosition
        playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void ApplyJump()
    {
        // Usamos método AddForce en Rigidbody para aplicar una fuerza vertical con modo de Impulso
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

   private void CheckGrounded()
{
    // Verificar si el personaje está en el suelo usando un Raycast
    float raycastDistance = 0.5f; // Distancia del Raycast (ajusta según el tamaño del personaje)
    RaycastHit hit;

    if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
    {
        if (hit.collider.CompareTag("Floor"))
        {
            isGrounded = true;
            playerAnimation.SetBool("IsFalling", false);
            // Activa la animación de aterrizaje
            playerAnimation.SetTrigger("JumpEnd");
        }
    }
    else
    {
        isGrounded = false;
    }

    // Debug: Dibujar el Raycast en la escena (solo para pruebas)
    Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red);
}

    private void CheckFalling()
    {
        // Verificar si el personaje está cayendo
        if (!isGrounded && playerRigidbody.linearVelocity.y < 0)
        {
            isFalling = true;
            isJumping = false;
            playerAnimation.SetBool("IsJumping", false);
            playerAnimation.SetBool("IsFalling", true); 
            playerAnimation.SetTrigger("JumpEnd");
            Debug.Log("JumpEnd activado");
        }
    }

    private void RotateTowardsMovement()
    {
        // Rotación suave hacia la dirección de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}