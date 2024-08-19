using UnityEngine;

[DefaultExecutionOrder(4)]
public class UIHealthHandler : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private PlayerHealth player;

    [Header("Reference to Health Blob")]
    [SerializeField] private GameObject healthBlob;
    [SerializeField] private MeshRenderer blobMesh;

    [Header("Health Blob Visualization Materials")]
    [SerializeField] private Material healthBlobMat;

    private void Start()
    {
        healthBlob = GetComponent<GameObject>();
        blobMesh = GetComponent<MeshRenderer>();
        blobMesh.material = healthBlobMat;

        if (player == null)
        {
            Debug.LogError("Assign Reference to Player Health script on Player");
        }
        else
        {
            player.OnPlayerDamageEvent += UpdateCurrentHealth;

            UpdateCurrentHealth(player.GetHealth,player.GetMaxHealth);
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.OnPlayerDamageEvent -= UpdateCurrentHealth;
        }
    }

    public void UpdateCurrentHealth(float _newHealth,float _maxHealth)
    {
        float percentage = _newHealth / _maxHealth;
        healthBlobMat.SetFloat(MetaConstants.MatShaderOverlay, percentage);
    }
}
