using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DestructibleObject))]
public class BuildingEnemyPoolToAttack : MonoBehaviour
{
    [SerializeField] private int maxNumberEnemies = 10;
    private Dictionary<int, Transform> enemies;

    private void Start()
    {
        enemies = new Dictionary<int, Transform>();
		}

    public void SetEnemyToAttackBuilding (Transform enemy)
    {
        if(enemies.Count < maxNumberEnemies)
        {
            if(!enemies.ContainsKey(enemy.transform.GetInstanceID()))
                enemies.Add(enemy.GetInstanceID(), enemy);         
        }
    }

    public void RemoveEnemyFromAttackingBuilding (Transform enemy)
    {
        if(enemies.Count > 0)
        {
            if(enemies.ContainsKey(enemy.GetInstanceID()))
            {
                enemies.Remove(enemy.GetInstanceID());
            }
        }
    }

    public void RemoveEnemiesNull()
    {
        if(enemies.Count > 0)
        {
            var nullables = enemies.Keys.Where(key => enemies[key] == null).ToList();
            foreach(var nullable in nullables)
            {
                enemies.Remove(nullable);
            }
        }
    }

    public void ResetBuilding ()
    {
        enemies.Clear();
    }

    public void SetNewPoolToAttackBuilding (int value)
    {
        maxNumberEnemies = value;
    }

    public int GetEnemiesInBuilding => enemies.Count;

    
}
