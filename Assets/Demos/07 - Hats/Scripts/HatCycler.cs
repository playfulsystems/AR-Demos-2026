using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARFaceManager))]
public class HatCycler : MonoBehaviour
{
    [SerializeField] List<GameObject> hatPrefabs;
    [SerializeField] InputActionReference tapAction;

    ARFaceManager faceManager;
    Dictionary<TrackableId, GameObject> activeHats = new();
    int currentIndex = 0;
    [SerializeField] Vector3 hatOffset;
    [SerializeField] Vector3 hatRotationOffset;

    void Awake()
    {
        faceManager = GetComponent<ARFaceManager>();
    }

    void OnEnable()
    {
        faceManager.trackablesChanged.AddListener(OnFacesChanged);
        tapAction.action.performed += OnTap;
        tapAction.action.Enable();
    }

    void OnDisable()
    {
        faceManager.trackablesChanged.RemoveListener(OnFacesChanged);
        tapAction.action.performed -= OnTap;
    }

    void OnFacesChanged(ARTrackablesChangedEventArgs<ARFace> args)
    {
        foreach (var face in args.added)
            SpawnHat(face);

        foreach (var face in args.removed)
            DestroyHat(face.Key);
    }


    void OnTap(InputAction.CallbackContext ctx)
    {
        if (hatPrefabs.Count == 0) return;

        Debug.Log("TAP TAP TAP");

        currentIndex = (currentIndex + 1) % hatPrefabs.Count;

        // Replace Hats on all currently tracked faces
        foreach (var face in faceManager.trackables)
        {
            DestroyHat(face.trackableId);
            SpawnHat(face);
        }
    }

    void SpawnHat(ARFace face)
    {
        if (hatPrefabs.Count == 0) return;
        var hat = Instantiate(hatPrefabs[currentIndex], face.transform);

        hat.transform.position += hatOffset;
        hat.transform.localEulerAngles += hatRotationOffset;

        activeHats[face.trackableId] = hat;
    }

    void DestroyHat(TrackableId id)
    {
        if (activeHats.TryGetValue(id, out var hat))
        {
            Destroy(hat);
            activeHats.Remove(id);
        }
    }
}
