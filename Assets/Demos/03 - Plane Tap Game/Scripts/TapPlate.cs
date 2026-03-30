using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TapPlate : MonoBehaviour
{
    public Material StandardMaterial;
    public Material HighlightMaterial;
    public MeshRenderer MeshRenderer;
    public XRSimpleInteractable Interactable;
    public UnityEvent TapComplete;

    bool isHighlighted = false;

    void OnEnable()
    {
        Interactable.selectEntered.AddListener(OnTap);
    }

    void OnDisable()
    {
        Interactable.selectEntered.RemoveListener(OnTap);
    }

    public void OnTap(SelectEnterEventArgs args)
    {
        if (isHighlighted)
        {
            SetHighlighted(false);
            TapComplete.Invoke();
        }
    }

    public void SetHighlighted(bool highlighted)
    {
        if (highlighted) MeshRenderer.material = HighlightMaterial;
        else MeshRenderer.material = StandardMaterial;

        isHighlighted = highlighted;
    }
}
