﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    GameObject m_BackButton;
    public GameObject backButton
    {
        get { return m_BackButton; }
        set { m_BackButton = value; }
    }
    
    void Start()
    {
         if (Application.CanStreamedLevelBeLoaded("Menu"))
         {
            m_BackButton.SetActive(true);
         }
    }

    public void BackButtonPressed()
    {   
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
