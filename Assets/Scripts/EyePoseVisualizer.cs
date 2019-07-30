﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#if UNITY_IOS && !UNITY_EDITOR
using UnityEngine.XR.ARKit;
#endif

/// <summary>
/// Visualizes the eye poses for an <see cref="ARFace"/>.
/// </summary>
/// <remarks>
/// Face space is the space where the origin is the transform of an <see cref="ARFace"/>.
/// </remarks>
[RequireComponent(typeof(ARFace))]
public class EyePoseVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject m_EyePrefab;

    public GameObject eyePrefab
    {
        get => m_EyePrefab;
        set => m_EyePrefab = value;
    }

    GameObject m_LeftEyeGameObject;
    GameObject m_RightEyeGameObject;

    ARFace m_Face;
    XRFaceSubsystem m_FaceSubsystem;

    void Awake()
    {
        m_Face = GetComponent<ARFace>();
    }

    void CreateEyeGameObjectsIfNecessary()
    {
        if (m_Face.leftEye != null && m_LeftEyeGameObject == null )
        {
            m_LeftEyeGameObject = Instantiate(m_EyePrefab, m_Face.leftEye);
            m_LeftEyeGameObject.SetActive(false);
        }
        if (m_Face.rightEye != null && m_RightEyeGameObject == null)
        {
            m_RightEyeGameObject = Instantiate(m_EyePrefab, m_Face.rightEye);
            m_RightEyeGameObject.SetActive(false);
        }
    }

    void SetVisible(bool visible)
    {
        if (m_LeftEyeGameObject != null && m_RightEyeGameObject != null)
        {
            m_LeftEyeGameObject.SetActive(visible);
            m_RightEyeGameObject.SetActive(visible);
        }
    }

    void UpdateVisibility()
    {
        var visible =
            enabled &&
            (m_Face.trackingState == TrackingState.Tracking) &&
            m_FaceSubsystem.SubsystemDescriptor.supportsEyeTracking &&
            (ARSession.state > ARSessionState.Ready);

        SetVisible(visible);
    }

    void OnEnable()
    {
        var faceManager = FindObjectOfType<ARFaceManager>();
        if (faceManager != null)
        {
            m_FaceSubsystem = (XRFaceSubsystem)faceManager.subsystem;
        }
        UpdateVisibility();
        m_Face.updated += OnUpdated;
    }

    void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
    {
        CreateEyeGameObjectsIfNecessary();
        UpdateVisibility();
    }
}
