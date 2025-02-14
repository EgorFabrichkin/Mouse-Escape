namespace Mouse_Escape.Scripts.Game.GameRoot.SceneParams
{
    public class MainMenuEnterParams : SceneEnterParams
    {
        public string LevelID { get; }

        public MainMenuEnterParams(string sceneName, string levelID) : base(sceneName)
        {
            LevelID = levelID;
        }
    }
}