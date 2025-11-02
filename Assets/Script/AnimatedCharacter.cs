using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedCharacter : MonoBehaviour
{
    [Header("Animation Settings")]
    public string idleAnimationName = "Idle_A";
    public string walkAnimationName = "Walk";
    public float rotationSpeed = 720f; // Degrees per second (fast rotation)
    public float idleThreshold = 0.15f; // Speed below which character is considered idle
    public float animationTransitionTime = 0.15f; // Crossfade duration
    
    [Header("Debug")]
    public bool showDebugInfo = false;
    
    private Animator animator;
    private Transform parentTransform;
    private Vector3 lastPosition;
    private Vector3 currentVelocity;
    private bool isCurrentlyWalking = false;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        parentTransform = transform.parent;
        
        if (parentTransform != null)
        {
            lastPosition = parentTransform.position;
        }
        else
        {
            lastPosition = transform.position;
        }
    }
    
    void Start()
    {
        // Start with idle animation
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            animator.Play(idleAnimationName, 0, 0f);
            isCurrentlyWalking = false;
            
            if (showDebugInfo)
            {
                Debug.Log($"{gameObject.name} - Starting with {idleAnimationName}");
            }
        }
    }
    
    void LateUpdate()
    {
        UpdateMovementAnimation();
        UpdateRotation();
    }
    
    void UpdateMovementAnimation()
    {
        if (animator == null || animator.runtimeAnimatorController == null)
            return;
            
        // Calculate current velocity based on parent's position
        Vector3 currentPosition = parentTransform != null ? parentTransform.position : transform.position;
        currentVelocity = (currentPosition - lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;
        
        float speed = new Vector3(currentVelocity.x, 0, currentVelocity.z).magnitude;
        
        // Determine if we should be walking
        bool shouldWalk = speed > idleThreshold;
        
        // Only change animation if state changed
        if (shouldWalk != isCurrentlyWalking)
        {
            isCurrentlyWalking = shouldWalk;
            
            string targetAnimation = shouldWalk ? walkAnimationName : idleAnimationName;
            
            if (showDebugInfo)
            {
                Debug.Log($"{gameObject.name} - Speed: {speed:F2}, Switching to: {targetAnimation}");
            }
            
            animator.CrossFade(targetAnimation, animationTransitionTime);
        }
    }
    
    void UpdateRotation()
    {
        // Only rotate if moving
        Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        
        if (horizontalVelocity.magnitude > idleThreshold)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
