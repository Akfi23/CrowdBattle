using UnityEngine;

namespace _Client_.Scripts.Utils.Extensions
{
    public static partial class Extensions
    {
        public static Vector3 GetRandomPointInArea(in Vector3 size,Transform spawnAreaTransform)
        {
            Vector3 halfSize = size * 0.5f;
            float localX = Random.Range(-halfSize.x, halfSize.x);
            float localY = Random.Range(-halfSize.y, halfSize.y);
            float localZ = Random.Range(-halfSize.z, halfSize.z);
            Vector3 localPoint = new Vector3(localX, localY, localZ);

            Quaternion yRotation = Quaternion.Euler(0f, spawnAreaTransform.eulerAngles.y, 0f);
            return spawnAreaTransform.position + yRotation * localPoint;
        }
    }
}