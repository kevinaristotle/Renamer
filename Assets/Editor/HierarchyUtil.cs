using UnityEngine;

public sealed class HierarchyUtil : Object
{
    public static GameObject[] GetGameObjectsActiveInHierarchy()
    {
        GameObject[] gameObjects;
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        GameObject[] allGameObjectsActiveInHierarchy = new GameObject[allGameObjects.Length];

        int objectsActiveInHieararchyCount = 0;
        for (int i = 0; i < allGameObjects.Length; i++)
        {
            if (allGameObjects[i].activeInHierarchy)
            {
                allGameObjectsActiveInHierarchy[objectsActiveInHieararchyCount] = allGameObjects[i];
                objectsActiveInHieararchyCount++;
            }
        }

        gameObjects = new GameObject[objectsActiveInHieararchyCount];
        for (int i = 0; i < objectsActiveInHieararchyCount; i++)
        {
            gameObjects[i] = allGameObjectsActiveInHierarchy[i];
        }

        return gameObjects;
    }
}
