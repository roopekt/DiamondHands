using UnityEngine;

[RequireComponent(typeof(EntityManager))]
[RequireComponent(typeof(ObjectPool))]
public class GameManager : ComponentHolder
{
    public static GameManager instance;
    public EntityManager entityManager;
    public ObjectPool entityPool;
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
    void Reset()
    {
        FindComponent(ref entityManager);
        FindComponent(ref entityPool);
    }
}
