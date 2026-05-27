using UnityEngine;

namespace MMO.Animation
{
    /// <summary>
    /// キャラクターの手足アニメーションを制御します
    /// IK (逆運動学) を使用して、より自然なポーズを作成します
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform leftHandTarget;
        [SerializeField] private Transform rightHandTarget;
        [SerializeField] private Transform leftFootTarget;
        [SerializeField] private Transform rightFootTarget;
        
        [SerializeField] private bool useIK = true;
        [SerializeField] private float ikWeight = 1f;
        [SerializeField] private float handIKWeight = 0.5f;
        [SerializeField] private float footIKWeight = 0.8f;
        
        // アニメーション状態
        private bool isJumping;
        private bool isDancing;
        private string currentEmote = "";
        
        // Animation parameter hashes
        private int jumpHash = Animator.StringToHash("Jump");
        private int waveHash = Animator.StringToHash("Wave");
        private int danceHash = Animator.StringToHash("Dance");
        private int sitHash = Animator.StringToHash("Sit");
        
        private void Start()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
        }
        
        private void OnAnimatorIK(int layerIndex)
        {
            if (!useIK || animator == null) return;
            
            // 左手のIK
            if (leftHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handIKWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }
            
            // 右手のIK
            if (rightHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, handIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, handIKWeight);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            }
            
            // 左足のIK
            if (leftFootTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, footIKWeight);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootTarget.rotation);
            }
            
            // 右足のIK
            if (rightFootTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, footIKWeight);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, footIKWeight);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTarget.position);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootTarget.rotation);
            }
            
            // 腰（Hips）のIK
            animator.SetLookAtWeight(0.5f);
            animator.SetLookAtPosition(transform.position + transform.forward * 3f + Vector3.up * 1.5f);
        }
        
        /// <summary>
        /// ジャンプアニメーション
        /// </summary>
        public void PlayJumpAnimation()
        {
            animator.SetTrigger(jumpHash);
            isJumping = true;
        }
        
        /// <summary>
        /// 挨拶アニメーション
        /// </summary>
        public void PlayWaveAnimation()
        {
            animator.SetTrigger(waveHash);
            currentEmote = "Wave";
        }
        
        /// <summary>
        /// ダンスアニメーション
        /// </summary>
        public void PlayDanceAnimation()
        {
            animator.SetTrigger(danceHash);
            isDancing = true;
            currentEmote = "Dance";
        }
        
        /// <summary>
        /// 座るアニメーション
        /// </summary>
        public void PlaySitAnimation()
        {
            animator.SetTrigger(sitHash);
            currentEmote = "Sit";
        }
        
        /// <summary>
        /// カスタムアニメーション再生
        /// </summary>
        public void PlayAnimation(string animationName)
        {
            animator.SetTrigger(Animator.StringToHash(animationName));
            currentEmote = animationName;
        }
        
        /// <summary>
        /// IKの有効/無効を切り替え
        /// </summary>
        public void SetIKEnabled(bool enabled)
        {
            useIK = enabled;
        }
        
        /// <summary>
        /// 手のIK重みを設定
        /// </summary>
        public void SetHandIKWeight(float weight)
        {
            handIKWeight = Mathf.Clamp01(weight);
        }
        
        /// <summary>
        /// 足のIK重みを設定
        /// </summary>
        public void SetFootIKWeight(float weight)
        {
            footIKWeight = Mathf.Clamp01(weight);
        }
        
        /// <summary>
        /// 現在のエモート状態を取得
        /// </summary>
        public string GetCurrentEmote()
        {
            return currentEmote;
        }
        
        /// <summary>
        /// ジャンプ状態を取得
        /// </summary>
        public bool IsJumping()
        {
            return isJumping;
        }
        
        /// <summary>
        /// ダンス状態を取得
        /// </summary>
        public bool IsDancing()
        {
            return isDancing;
        }
    }
}
