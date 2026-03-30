using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TapPlateGame : MonoBehaviour
{
    public TapPlate[] TapPlates;
    private int currentHighlighted = -1;

    private void Start()
    {
        SetRandomPlaneHighlight();
    }

    public void SetRandomPlaneHighlight()
    {
        int randomIndex = Random.Range(0, TapPlates.Length);

        // try again if same
        if (currentHighlighted == randomIndex)
        {
            SetRandomPlaneHighlight();
            return;
        }

        TapPlates[randomIndex].SetHighlighted(true);
        currentHighlighted = randomIndex;
    }
}
