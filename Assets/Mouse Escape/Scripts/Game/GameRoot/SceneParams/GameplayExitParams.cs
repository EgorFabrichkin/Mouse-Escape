namespace Mouse_Escape.Scripts.Game.GameRoot.SceneParams
{
    public class GameplayExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; }
        
        public GameplayExitParams(SceneEnterParams targetSceneEnterParams)
        {
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}