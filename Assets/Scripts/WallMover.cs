using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMover : MonoBehaviour
{
    public GameObject wallPrefab;
    public PlayerController player;
    public Transform wallParent;

    public float wallDistance;

    private PlatformGenerator.ObjectPool pool;

    private float yPos;
    [SerializeField]
    [ReadOnly]
    private int steps = 0;

    private void Start()
    {
        pool = new PlatformGenerator.ObjectPool(6, wallPrefab, wallParent, true);
        yPos = wallPrefab.transform.position.y;
        for (int i = 0; i < 6; i++)
            Generate();
    }

    private void Generate()
    {
        var wall = pool.Get();
        wall.transform.SetParent(wallParent, false);
        var position = wall.transform.localPosition;
        position.y = yPos;
        wall.transform.localPosition = position;
        yPos += wallDistance;
        steps++;
    }

    private void Update()
    {
        if (steps * 3 - player.GetMaxRoundedPlatformPosition() - 6 < 5)
            Generate();
    }
}
