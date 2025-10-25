public class Enemy : BaseEnemy
{
    public override void OnDestroy()
    {
        base.OnDestroy();
        gameManager.IncreaseDefeatedEnemies();
    }
}
