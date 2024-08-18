using UnityEngine;

[DefaultExecutionOrder(4)]
public class UIHealthHandler : MonoBehaviour
{
    [Header("Reference to Health Blob")]
    [SerializeField] private GameObject healthBlob;
    [SerializeField] private MeshRenderer blobMesh;

    [Header("Health Blob Visualization Materials")]
    [SerializeField] private Material healthBlobMat;

    [Range(0f, 1f)]
    [SerializeField] private float health;

    private void Start()
    {
        healthBlob = GetComponent<GameObject>();
        blobMesh = GetComponent<MeshRenderer>();

        blobMesh.material = healthBlobMat;
    }

    private void Update()
    {
        healthBlobMat.SetFloat("_OverlayAmount", health);
    }

    public void SetHealth()
    {

    }
}
