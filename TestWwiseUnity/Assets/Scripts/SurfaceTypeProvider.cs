using UnityEngine;

public class SurfaceTypeProvider : MonoBehaviour
{
    public enum SurfaceType
    {
        Ground,
        Water,
        Metal
    }

    public SurfaceType surfaceType = SurfaceType.Ground;
}