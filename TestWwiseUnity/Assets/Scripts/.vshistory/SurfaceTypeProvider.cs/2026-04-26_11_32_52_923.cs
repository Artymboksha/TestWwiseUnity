using UnityEngine;

public class SurfaceTypeProvider : MonoBehaviour
{
    public enum SurfaceType
    {
        Ground,
        Water,
        Snow
    }

    public SurfaceType surfaceType = SurfaceType.Ground;
}