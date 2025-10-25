public class Boss : BaseEnemy
{
    public override void OnDestroy()
    {
        base.OnDestroy();
        gameManager.IncreaseDefeatedBosses();
    }
}
