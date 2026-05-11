using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    [SerializeField] private LevelConfig levelConfig;
    public LevelConfig LevelConfig => levelConfig; 
}
