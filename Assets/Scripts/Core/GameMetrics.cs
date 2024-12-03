using NaughtyAttributes;
using UnityEngine;

namespace VraiOuFaux.Core
{
    [CreateAssetMenu(order = 1, menuName = "CIRC/GameMetrics", fileName = "NewGameMetrics")]
    public class GameMetrics : ScriptableObject
    {
        public GameMetrics Global => GameController.GameMetrics;
        
        [field: SerializeField, Foldout("ChoiceColors")] public Color MascotColorNormal { get; private set; }
        [field: SerializeField, Foldout("ChoiceColors")] public Color MascotColorGood { get; private set; }
        [field: SerializeField, Foldout("ChoiceColors")] public Color MascotColorBad { get; private set; }
    }
}