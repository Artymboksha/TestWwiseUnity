using UnityEngine;

public class SurfaceTypeProvider : MonoBehaviour
{
    public enum SurfaceType
    {
        Ground,
        Water,
        Metal,
        Snow
    }

    public SurfaceType surfaceType = SurfaceType.Ground;
}