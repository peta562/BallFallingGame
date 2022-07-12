﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs {
    [Serializable]
    public sealed class LevelInfo {
        public int Id;
        public int TargetScore;
        public int TargetBalls;
    }
    
    [CreateAssetMenu(fileName = "ProgressConfig", menuName = "ScriptableObjects/Configs/ProgressConfig", order = 3)]
    public class ProgressConfig : ScriptableObject {
        public List<LevelInfo> Levels = new List<LevelInfo>();
        public bool IsEndlessGameplayEnabled; // ToDo
        public int MaxLevels => Levels.Count;
    }
}