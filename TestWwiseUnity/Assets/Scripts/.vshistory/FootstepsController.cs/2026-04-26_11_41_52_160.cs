using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    [Header("Wwise Settings")]
    [SerializeField] private AK.Wwise.Event footstepEvent;

    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 1.5f;

    public void PlayFootstepSound()
    {
        SurfaceTypeProvider.SurfaceType surface = GetSurfaceType();

        footstepEvent?.Post(gameObject);

        Debug.Log("Footstep on: " + surface);
    }

    private SurfaceTypeProvider.SurfaceType GetSurfaceType()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.2f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            SurfaceTypeProvider provider = hit.collider.GetComponent<SurfaceTypeProvider>();

            if (provider != null)
                return provider.surfaceType;
        }

        return SurfaceTypeProvider.SurfaceType.Ground;
    }
}