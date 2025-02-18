using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración")]
    // Variables de movimento
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float jumpForce = 5f;
    public float doubleJumpForce = 5f;
    public float runSpeed = 8f;
    private float horizontal, vertical;
    private Vector3 moveDirection;
    // Variables de Acciones de movimiento
    private bool isGrounded;
    public bool isJumping;
    public bool isFalling;
    private bool hasJumped = false;
    private bool canDoubleJump = false;
    // Variables de componentes
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator playerAnimation;

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
                hasJumped = true;
                canDoubleJump = true;
                isJumping = true;
                isFalling = false;
                playerAnimation.SetBool("IsJumping", true); // Activa la animación de salto inicial
                playerAnimation.SetBool("IsFalling", false);
                playerAnimation.SetBool("IsGrounded", false);
            }
            else if(hasJumped && canDoubleJump){
                ApplyDoubleJump();
                playerAnimation.SetBool("IsFalling", true);
                hasJumped = false;
                canDoubleJump = false;
            }
        }

        // Acelerador - Sprint 
        if (Input.GetButton("Debug Multiplier") && isGrounded)
        {
            moveSpeed = runSpeed; // Velocidad de correr
            playerAnimation.SetBool("isRunning", true); // Activa la animación de correr
        }
        else
        {
            moveSpeed = 2f; // Velocidad normal
            playerAnimation.SetBool("isRunning", false); // Desactiva la animación de correr
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
     private void ApplyDoubleJump()
    {
        // Usamos método AddForce en Rigidbody para aplicar una fuerza vertical con modo de Impulso
        playerRigidbody.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
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