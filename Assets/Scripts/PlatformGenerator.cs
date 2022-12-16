using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField]
    private int poolSize, fullFrequency, decorFrequency, fullLimit;
    [SerializeField]
    private float minX, maxX, platformDistance, minSize, maxSize;
    [SerializeField]
    private GameObject smallPlatformPrefab, mediumPlatformPrefab, largePlatformPrefab, fullPlatformPrefab;
    [SerializeField]
    private Transform platformParent;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private FloatValue minimumHeight;

    private ObjectPool smallPlatformPool, mediumPlatformPool, largePlatformPool, fullPlatformPool;

    private int platformsGenerated;

    private void Start()
    {
        smallPlatformPool = new ObjectPool(poolSize, smallPlatformPrefab, platformParent, true);
        mediumPlatformPool = new ObjectPool(poolSize, mediumPlatformPrefab, platformParent, true);
        largePlatformPool = new ObjectPool(poolSize, largePlatformPrefab, platformParent, true);
        fullPlatformPool = new ObjectPool(poolSize / 2, fullPlatformPrefab, platformParent);

        for (int i = 0; i < 10; i++)
            Generate();
    }

    private float updateTimer;
    private bool deathMarch;
    private void Update()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer > 0.25f)
        {
            var playerHeight = player.GetMaxRoundedPlatformPosition();
            if (playerHeight > 4)
                minimumHeight.SetValue(Mathf.Max(minimumHeight, (playerHeight - 4) * platformDistance));
            while (playerHeight + 5 > platformsGenerated)
                Generate();

            updateTimer -= .25f;
        }
    }

    public void Generate()
    {
        bool fullPlatform = (platformsGenerated + 1) % 50 == 0 && platformsGenerated + 1 <= fullLimit;
        GameObject platform;
        if(fullPlatform)
            platform = fullPlatformPool.Get();
        else
        {
            var random = Random.Range(0, 3);
            switch(random)
            {
                case 0:
                    platform = smallPlatformPool.Get();
                    break;
                case 1:
                    platform = mediumPlatformPool.Get();
                    break;
                case 2:
                    platform = largePlatformPool.Get();
                    break;
                default:
                    platform = mediumPlatformPool.Get();
                    break;
            }
        }
        platform.transform.parent = platformParent;
        var platformControl = platform.GetComponent<Platform>();
        if(platformControl != null)
        {
            if ((platformsGenerated + 1) % 10 == 0)
                platformControl.SetLabel($"{platformsGenerated + 1}");
            else
                platformControl.DisableLabel();
            //if (!fullPlatform)
            //    platformControl.SetLength(Random.Range(minSize, maxSize));
        }
        platform.transform.localPosition = new Vector3(fullPlatform ? 0 : Random.Range(minX, maxX), (platformsGenerated + 1) * platformDistance);
        platformsGenerated++;
    }


    public class ObjectPool
    {
        private List<GameObject> activePool;
        private List<GameObject> freePool;
        private int poolSize;
        private GameObject prefab;
        private Transform parent;

        public ObjectPool(int poolSize, GameObject prefab, Transform parent = null, bool shouldInitialize = false)
        {
            this.poolSize = poolSize;
            this.prefab = prefab;
            activePool = new List<GameObject>();
            freePool = new List<GameObject>();
            if (shouldInitialize)
                Init();
        }

        public GameObject Get()
        {
            if (freePool.Count == 0)
            {
                if (activePool.Count < poolSize)
                {
                    var newOb = Instantiate(prefab, parent, false);
                    if (parent != null)
                        newOb.transform.parent = parent;
                    activePool.Add(newOb);
                    return newOb;
                }
                else
                {
                    var ob = activePool[0];
                    activePool.RemoveAt(0);
                    activePool.Add(ob);
                    ob.SetActive(true);
                    return ob;
                }
            }
            else
            {
                var ob = freePool[0];
                activePool.Add(ob);
                freePool.RemoveAt(0);
                ob.SetActive(true);
                return ob;
            }
        }

        public void Free(GameObject ob)
        {
            var index = activePool.IndexOf(ob);
            if (index != -1)
            {
                activePool.RemoveAt(index);
                freePool.Add(ob);
                ob.SetActive(false);
            }
        }

        private void Init()
        {
            for(int i = 0; i < poolSize; i++)
            {
                GameObject ob = Instantiate(prefab, parent, true);
                if (parent != null)
                    ob.transform.parent = parent;
                ob.SetActive(false);
                freePool.Add(ob);
            }
        }
    }
}


