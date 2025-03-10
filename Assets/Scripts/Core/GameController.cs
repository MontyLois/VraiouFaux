using UnityEngine;

namespace VraiOuFaux.Core
{
    public static class GameController
    {
        public static GameDatabase GameDatabase { get; private set; }
        public static GameMetrics GameMetrics { get; private set; }
        
        //allow to load the class when the game start and before the scene load
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Load()
        {
            //generate the database
            GameDatabase = new GameDatabase();
            GameMetrics = Resources.Load<GameMetrics>("GameMetrics");
            
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }
}