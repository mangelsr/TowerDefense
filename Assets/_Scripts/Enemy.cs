public class Enemy : BaseEnemy
{
    protected override void OnEnemyDeath()
    {
        base.OnEnemyDeath();
        gameManager.IncreaseDefeatedEnemies();
    }
}
