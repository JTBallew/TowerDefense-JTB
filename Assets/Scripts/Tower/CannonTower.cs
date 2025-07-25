using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject cannonPrefab;

    protected override void Update()
    {
        base.Update();
    }
    protected override void FireAt(Enemy target)
    {
        //add firing code
    }

    protected override Enemy GetTargetEnemy()
    {
        throw new System.NotImplementedException();
    }
}
