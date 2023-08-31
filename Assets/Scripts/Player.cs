using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player: MonoBehaviour, IKitchenObjectParent {

    public static Player Shared { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform holdPoint;

    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;
    private float radius = .7f;
    private float height = 2f;
    private float interactDistance = 1.5f;
    private Vector3 lastInteractionDir;
    float moveDistance {
        get {
            return Time.deltaTime * moveSpeed;
        }
    }
    private bool isWalking; 

    // Public methods
    public bool IsWalking() {
        return isWalking;
    }

    // Core Methods
    private void Awake() {
        Shared = this;
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    // Interaction
    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero) {
            lastInteractionDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, counterLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    // Movement
    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        bool hasObstacle = ObstacleDetector(moveDir);

        if (hasObstacle) {
            // X axis
            Vector3 moveX = new Vector3(moveDir.x, 0, 0);
            if (!ObstacleDetector(moveX)) {
                moveTo(moveX);
            }

            // Z axis
            Vector3 moveZ = new Vector3(0, 0, moveDir.z);
            if (!ObstacleDetector(moveZ)) {
                moveTo(moveZ);
            }
        }
        else {
            moveTo(moveDir);    
        }
        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, moveDistance);
    }

    private bool ObstacleDetector(Vector3 direction) {
        return Physics.CapsuleCast(transform.position, transform.position + Vector3.up * height, radius, direction, Time.deltaTime * moveSpeed);
    }

    // Move with default move distance
    private void moveTo(Vector3 direction) {
        moveTo(direction, moveDistance);
    }

    // Move with custom move distance
    private void moveTo(Vector3 direction, float moveDistance) {
        transform.position += direction * moveDistance;
    }

    // Counter
    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter
        });     
    }

    public KitchenObject GetKitchenObject() {
       return kitchenObject;
    }

    public Transform GetKitchenObjectFollowtransform() {
        return holdPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }
}