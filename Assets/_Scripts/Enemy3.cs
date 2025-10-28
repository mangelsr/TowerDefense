public class Enemy3 : BaseEnemy
{
    protected override void OnEnemyDeath()
    {
        base.OnEnemyDeath();
        gameManager.IncreaseDefeatedEnemies();
    }
}
