using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotAnimation : MonoBehaviour
{
    // [SerializeField] CollectableTypesEnum collectableType;

    // public CollectableTypesEnum CollectableType => collectableType;

    private Animator animator;

    private static readonly int PlayPulseAnimationHash = Animator.StringToHash("Pulse");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.Log($"Could not find an animator component for UI item {this.gameObject.name}");
        }
    }

    public void PlayPulseAnimation() => animator.SetTrigger(PlayPulseAnimationHash);
}
