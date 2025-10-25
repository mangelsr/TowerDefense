public class Enemy3 : BaseEnemy
{
    public override void OnDestroy()
    {
        base.OnDestroy();
        gameManager.IncreaseDefeatedEnemies();
    }
}
