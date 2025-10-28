using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int defeatedEnemies { get; private set; }
    public int defeatedBosses { get; private set; }
    [SerializeField] private int _resources = 800;
    public int resources { get => _resources; private set => _resources = value; }

    public delegate void ModifiedResources();
    public event ModifiedResources OnModifyResources;

    public void ModifyResources(int modification)
    {
        resources += modification;
        if (OnModifyResources != null)
        {
            OnModifyResources();
        }
    }

    public void ResetValues()
    {
        defeatedEnemies = 0;
        defeatedBosses = 0;
    }

    public void IncreaseDefeatedBosses()
    {
        defeatedBosses++;
    }

    public void IncreaseDefeatedEnemies()
    {
        defeatedEnemies++;
    }
}
