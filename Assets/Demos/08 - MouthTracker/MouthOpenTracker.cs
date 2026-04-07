using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARFaceManager))]
public class MouthOpenTracker : MonoBehaviour
{
    [SerializeField] ARFaceManager faceManager;
    [SerializeField] Slider slider;

    public float JawOpen { get; private set; }

    private void Awake()
    {
        if (faceManager == null)
            faceManager = GetComponent<ARFaceManager>();
    }

    private void OnEnable()
    {
        faceManager.trackablesChanged.AddListener(OnFacesChanged);
    }

    private void OnDisable()
    {
        faceManager.trackablesChanged.RemoveListener(OnFacesChanged);
    }

    private void OnFacesChanged(ARTrackablesChangedEventArgs<ARFace> args)
    {
        // Handle both updated and added faces
        foreach (var face in args.updated)
            ExtractJawOpen(face);

        foreach (var face in args.added)
            ExtractJawOpen(face);
    }

    private void ExtractJawOpen(ARFace face)
    {
        JawOpen = 0f;

        if (faceManager.subsystem == null)
            return;

        var result = faceManager.TryGetBlendShapes(face, Allocator.Temp);

        if (result.status.IsSuccess())
        {
            using var blendShapes = result.value;

            foreach (var shape in blendShapes)
            {
                if (shape.blendShapeId == (int)ARKitBlendShapeLocation.TongueOut)
                {
                    JawOpen = shape.weight;
                    break;
                }
            }
        }

        slider.value = JawOpen;
    }
}