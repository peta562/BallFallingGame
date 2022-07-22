using System;
using System.Collections.Generic;
using Core.Bonuses;
using Core.Bonuses.BonusEntity;
using UnityEngine;

namespace Configs {
    [Serializable]
    public class BonusDescription {
        public Sprite Sprite;
        public float Health;
        public float Scale;
    }
    
    [Serializable]
    public class AddLiveBonusDescription : BonusDescription {
        public AddLiveBonus Prefab;
        public int LiveToAdd;
    }
    
    [Serializable]
    public class KillAllBallsBonusDescription : BonusDescription {
        public KillAllBallsBonus Prefab;
    }
    
    [Serializable]
    public class MultiplyScoreBonusDescription : BonusDescription {
        public MultiplyScoreBonus Prefab;
        public int Multiplier;
        public float MultiplierTime;
    }
    
    [CreateAssetMenu(fileName = "BonusConfig", menuName = "ScriptableObjects/Configs/BonusConfig", order = 4)]
    public class BonusConfig : ScriptableObject {
        public AddLiveBonusDescription AddLiveBonusDescription;
        public KillAllBallsBonusDescription KillAllBallsBonusDescription;
        public MultiplyScoreBonusDescription MultiplyScoreBonusDescription;
    }
}