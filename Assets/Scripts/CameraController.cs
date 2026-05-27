using UnityEngine;

namespace MMO.Camera
{
    /// <summary>
    /// プレイヤーをフォローするカメラシステム
    /// 第三者視点でプレイヤーを追跡します
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float distance = 5f;
        [SerializeField] private float height = 2f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float followSpeed = 5f;
        
        [SerializeField] private float minVerticalAngle = 10f;
        [SerializeField] private float maxVerticalAngle = 80f;
        
        private float horizontalAngle = 0f;
        private float verticalAngle = 45f;
        private UnityEngine.Camera mainCamera;
        
        private void Start()
        {
            mainCamera = GetComponent<UnityEngine.Camera>();
            if (mainCamera == null)
            {
                mainCamera = UnityEngine.Camera.main;
            }
        }
        
        private void LateUpdate()
        {
            if (playerTransform == null) return;
            
            HandleCameraInput();
            UpdateCameraPosition();
        }
        
        /// <summary>
        /// マウス入力でカメラ回転
        /// </summary>
        private void HandleCameraInput()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            horizontalAngle += mouseX * rotationSpeed;
            verticalAngle -= mouseY * rotationSpeed;
            verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
        }
        
        /// <summary>
        /// カメラの位置を更新
        /// </summary>
        private void UpdateCameraPosition()
        {
            // 球面座標でカメラ位置を計算
            float radianHorizontal = Mathf.Deg2Rad * horizontalAngle;
            float radianVertical = Mathf.Deg2Rad * verticalAngle;
            
            Vector3 offset = new Vector3(
                Mathf.Sin(radianHorizontal) * Mathf.Cos(radianVertical),
                Mathf.Sin(radianVertical),
                Mathf.Cos(radianHorizontal) * Mathf.Cos(radianVertical)
            ) * distance;
            
            Vector3 targetPosition = playerTransform.position + Vector3.up * height + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            
            // プレイヤーの頭部付近を見つめる
            Vector3 lookAtPosition = playerTransform.position + Vector3.up * 1.5f;
            transform.LookAt(lookAtPosition);
        }
        
        /// <summary>
        /// カメラリセット（プレイヤーの背後）
        /// </summary>
        public void ResetCamera()
        {
            horizontalAngle = 0f;
            verticalAngle = 45f;
        }
    }
}
