using System;
using Core.PlayableObjects.Balls;
using Core.PlayableObjects.Bonuses;
using UnityEngine;
using Utils;

namespace Configs {
    [Serializable]
    public class PlayableObjectDescription {
        public Sprite Sprite;
    }

    [Serializable]
    public class BallDescription : PlayableObjectDescription {
        public Ball Prefab;
        public FloatRange Scale = new FloatRange(0.5f, 1.2f);
        public FloatRange Health = new FloatRange(10f, 50f);
    }
    
    [Serializable]
    public class AddLivePlayableObjectDescription : PlayableObjectDescription {
        public AddLiveBonus Prefab;
        public float Health;
        public float Scale;
        public int LivesToAdd;
    }
    
    [Serializable]
    public class KillAllBallsPlayableObjectDescription : PlayableObjectDescription {
        public KillAllBallsBonus Prefab;
        public float Health;
        public float Scale;
    }
    
    [Serializable]
    public class MultiplyScorePlayableObjectDescription : PlayableObjectDescription {
        public MultiplyScoreBonus Prefab;
        public float Health;
        public float Scale;
        public int Multiplier;
        public float MultiplierTime;
    }
    
    [CreateAssetMenu(fileName = "PlayableObjectsConfig", menuName = "ScriptableObjects/Configs/PlayableObjectsConfig", order = 2)]
    public class PlayableObjectsConfig : ScriptableObject {
        public BallDescription BallDescription;
        public AddLivePlayableObjectDescription AddLiveBonusDescription;
        public KillAllBallsPlayableObjectDescription KillAllBallsBonusDescription;
        public MultiplyScorePlayableObjectDescription MultiplyScoreBonusDescription;
    }
}