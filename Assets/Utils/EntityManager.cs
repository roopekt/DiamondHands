using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager instance;
    List<EntityBase> entites = new();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public EntityBase SpawnFromPrefab(GameObject entityPrefab)
    {
        GameObject entityParent = GameManager.instance.entityPool.PoolItem(entityPrefab);
        if (entityParent?.TryGetComponent(out EntityBase entity) ?? false)
        {
            Spawn(entity);
            return entity;
        }
        return null;
    }
    public void Spawn(EntityBase entity)
    {
        if (!entites.Contains(entity))
        {
            entity.Spawn();
            entites.Add(entity);
        }
    }

    public void Despawn(EntityBase entity) { 
        if (entites.Contains(entity))
        {
            entity.Despawn();
            entites.Remove(entity);
            //GameManager.instance.entityPool.DeactivateObject(entity.game);
        }
    }
}

