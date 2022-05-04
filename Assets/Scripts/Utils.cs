using UnityEngine;

namespace Assets.Scripts
{
    public static class Utils
    {
        public static Vector2 Round(this Vector2 vector)
        {
            Vector2 newVector;
            newVector.x = Mathf.RoundToInt(vector.x);
            newVector.y = Mathf.RoundToInt(vector.y);
            return newVector;
        } 
        public static Vector3 Round(this Vector3 vector)
        {
            Vector3 newVector;
            newVector.x = Mathf.RoundToInt(vector.x);
            newVector.y = Mathf.RoundToInt(vector.y);
            newVector.z = Mathf.RoundToInt(vector.z);
            return newVector;
        }
    }
}
