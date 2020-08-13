﻿using Assets.Scripts;
using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public static Galaxy Galaxy { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Debug.Log("Starting Galaxy Generation!");
        Galaxy = Save.SavedGameFound() ? Save.LoadGame() : new Galaxy();
        Debug.Log("Finished Galaxy Generation!");
        Galaxy.RenderStartingSystem();
    }
}
