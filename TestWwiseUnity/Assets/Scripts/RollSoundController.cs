using UnityEngine;

public class RollSoundController : MonoBehaviour
{
    [Header("Wwise Settings")]
    [SerializeField] private AK.Wwise.Event rollEvent;

    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 1.5f;

    public void PlayRollSound()
    {
        SurfaceTypeProvider.SurfaceType surface = GetSurfaceType();

        SetSurfaceSwitch(surface);

        rollEvent?.Post(gameObject);

        Debug.Log("Roll on: " + surface);
    }

    private void SetSurfaceSwitch(SurfaceTypeProvider.SurfaceType surface)
    {
        AkUnitySoundEngine.SetSwitch(
            "surface",
            surface.ToString(),
            gameObject
        );
    }

    private SurfaceTypeProvider.SurfaceType GetSurfaceType()
    {
        Ray ray = new Ray(
            transform.position + Vector3.up * 0.2f,
            Vector3.down
        );

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            SurfaceTypeProvider provider =
                hit.collider.GetComponent<SurfaceTypeProvider>();

            if (provider != null)
                return provider.surfaceType;
        }

        return SurfaceTypeProvider.SurfaceType.Ground;
    }
}
