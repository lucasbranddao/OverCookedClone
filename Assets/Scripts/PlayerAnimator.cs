using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator: MonoBehaviour {

    [SerializeField] private Player player;
    private const string isWalking = "isWalking";
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(isWalking, player.IsWalking());
    }
}
