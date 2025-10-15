using System.Collections.Generic;
using UnityEngine;

public class FaceVisibilityManager : MonoBehaviour
{
    public static FaceVisibilityManager Instance { get; private set; }

    private List<AlarmDead> activeFaces = new List<AlarmDead>();
    private List<AlarmDead> checkingFaces = new List<AlarmDead>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("FaceVisibilityManager Instance created");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateDangerIndicator();
    }

    public void RegisterFace(AlarmDead face)
    {
        if (!activeFaces.Contains(face))
        {
            activeFaces.Add(face);
            Debug.Log($"Registered face. Total active faces: {activeFaces.Count}");
        }
    }

    public void UnregisterFace(AlarmDead face)
    {
        activeFaces.Remove(face);
        checkingFaces.Remove(face);
        Debug.Log($"Unregistered face. Total active faces: {activeFaces.Count}");
    }

    public void SetFaceChecking(AlarmDead face, bool isChecking)
    {
        if (isChecking)
        {
            if (!checkingFaces.Contains(face))
            {
                checkingFaces.Add(face);
            }
        }
        else
        {
            checkingFaces.Remove(face);
        }
    }

    private void UpdateDangerIndicator()
    {
        activeFaces.RemoveAll(face => face == null);
        checkingFaces.RemoveAll(face => face == null);

        bool anyVisibleFaces = false;
        bool anyVisibleChecking = false;
        int visibleCount = 0;
        int checkingVisibleCount = 0;

        foreach (AlarmDead face in activeFaces)
        {
            if (face != null && face.IsVisibleOnScreen())
            {
                anyVisibleFaces = true;
                visibleCount++;

                if (checkingFaces.Contains(face))
                {
                    anyVisibleChecking = true;
                    checkingVisibleCount++;
                }
            }
        }

        Debug.Log($"[DangerIndicator] Total: {activeFaces.Count}, Visible: {visibleCount}, Checking Visible: {checkingVisibleCount}");

        if (DangerIndicator.Instance != null)
        {
            DangerIndicator.Instance.SetFacesActive(anyVisibleFaces);
            DangerIndicator.Instance.SetFacesChecking(anyVisibleChecking);
        }
    }

    public bool AnyFaceVisible()
    {
        activeFaces.RemoveAll(face => face == null);

        int visibleCount = 0;
        foreach (AlarmDead face in activeFaces)
        {
            if (face != null && face.IsVisibleOnScreen())
            {
                visibleCount++;
            }
        }

        Debug.Log($"Checking visibility: {visibleCount}/{activeFaces.Count} faces visible");
        return visibleCount > 0;
    }
}
