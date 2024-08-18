using UnityEngine;

[DefaultExecutionOrder(4)]
public class UIHealthHandler : MonoBehaviour
{
    [Header("Reference to Health Blob")]
    [SerializeField] private GameObject healthBlob;
    [SerializeField] private MeshRenderer blobMesh;

    [Header("Health Blob Visualization Materials")]
    [SerializeField] private Material fullHpMat;
    [SerializeField] private Material halfHpMat;
    [SerializeField] private Material emptyHpMat;
    [SerializeField] private Material defaultHpMat;

    private void Start()
    {
        healthBlob = GetComponent<GameObject>();
        blobMesh = GetComponent<MeshRenderer>();

        blobMesh.material = fullHpMat;

        
    }
}
