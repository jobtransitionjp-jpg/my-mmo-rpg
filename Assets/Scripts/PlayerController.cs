using UnityEngine;
using MMO.Animation;

namespace MMO.Player
{
    /// <summary>
    /// プレイヤーキャラクターの移動と入力制御を管理します
    /// 手足のアニメーションと連動して、滑らかなキャラクター操作を実現します
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float groundDrag = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float groundDamping = 0.1f;
        
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float cameraDistance = 5f;
        
        private CharacterController charController;
        private Animator animator;
        private CharacterAnimator charAnimator;
        private Vector3 velocity;
        private Vector3 moveDirection;
        private float currentSpeed;
        private bool isGrounded;
        
        // Animation parameter hashes
        private int speedHash = Animator.StringToHash("Speed");
        private int directionHash = Animator.StringToHash("Direction");
        private int isGroundedHash = Animator.StringToHash("IsGrounded");
        
        private void Start()
        {
            charController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            charAnimator = GetComponent<CharacterAnimator>();
            
            if (mainCamera == null)
            {
                mainCamera = Camera.main?.transform;
            }
        }
        
        private void Update()
        {
            HandleInput();
            UpdateMovement();
            UpdateAnimation();
            HandleGroundCheck();
        }
        
        /// <summary>
        /// キーボード入力を処理します
        /// WASD：移動、SPACE：ジャンプ
        /// </summary>
        private void HandleInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            // カメラ方向を基準に移動方向を計算
            Vector3 cameraForward = mainCamera != null ? mainCamera.forward : transform.forward;
            Vector3 cameraRight = mainCamera != null ? mainCamera.right : transform.right;
            
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();
            
            moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;
            
            // ジャンプ
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }
        
        /// <summary>
        /// キャラクターの移動処理
        /// </summary>
        private void UpdateMovement()
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // 地面にいる時の小さな負の力
            }
            
            // 目標速度の計算
            float targetSpeed = moveDirection.magnitude > 0.1f ? moveSpeed : 0f;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, groundDamping);
            
            // 移動ベクトルの計算
            Vector3 moveVelocity = moveDirection * currentSpeed;
            velocity.x = moveVelocity.x;
            velocity.z = moveVelocity.z;
            
            // 重力の適用
            velocity.y -= 9.81f * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, -20f, jumpForce);
            
            // キャラクター移動
            charController.Move(velocity * Time.deltaTime);
            
            // キャラクターの回転
            if (moveDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        
        /// <summary>
        /// アニメーションパラメータを更新
        /// </summary>
        private void UpdateAnimation()
        {
            animator.SetFloat(speedHash, currentSpeed);
            animator.SetFloat(directionHash, moveDirection.magnitude);
            animator.SetBool(isGroundedHash, isGrounded);
        }
        
        /// <summary>
        /// 地面判定
        /// </summary>
        private void HandleGroundCheck()
        {
            isGrounded = charController.isGrounded;
        }
        
        /// <summary>
        /// ジャンプ処理
        /// </summary>
        private void Jump()
        {
            velocity.y = jumpForce;
            charAnimator?.PlayJumpAnimation();
        }
        
        /// <summary>
        /// 特定のアニメーションを再生 (挨拶、ダンスなど)
        /// </summary>
        public void PlayAnimation(string animationName)
        {
            charAnimator?.PlayAnimation(animationName);
        }
    }
}
