public class Boss : BaseEnemy
{
    protected override void OnEnemyDeath()
    {
        base.OnEnemyDeath();
        gameManager.IncreaseDefeatedBosses();
    }
}
