using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] CollectableTypesEnum _collectableType;

    public CollectableTypesEnum CollectableType => _collectableType;

    private Animator _animator;
    private static readonly int PlayPulseAnimationHash
        = Animator.StringToHash("Pulse");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        if(_animator == null)
        {
            Debug.Log($"Could not find an animator component for " +
                $"UI item {this.gameObject.name}");
        }
    }
    public void PlayPulseAnimation() => _animator.SetTrigger(PlayPulseAnimationHash);
}
