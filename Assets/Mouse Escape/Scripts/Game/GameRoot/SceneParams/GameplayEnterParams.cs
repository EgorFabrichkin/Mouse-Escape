namespace Mouse_Escape.Scripts.Game.GameRoot.SceneParams
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public string LevelID { get; }

        public GameplayEnterParams(string sceneName, string levelID) : base(sceneName)
        {
            LevelID = levelID;
        }
    }
}