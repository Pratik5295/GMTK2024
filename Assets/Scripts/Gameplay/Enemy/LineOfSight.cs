using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _angle;
    [SerializeField] private float _height;
    [SerializeField] private Color _meshColor = Color.red;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private LayerMask _oclussionLayers;
    private List<GameObject> _objectsWithinSensor = new List<GameObject>();
    public List<GameObject> ObjectsWithingSensor {get
    {
        _objectsWithinSensor.RemoveAll(obj => !obj);
        return _objectsWithinSensor;
    }}
    private Collider[] _colliders = new Collider[50];
    private Mesh _mesh;
    private int _count;
    private float _scanInterval;
    [SerializeField] private int _scanFrequency;
    private float _scanTimer;
    private void Start()
    {
        _scanInterval = 1.0f / _scanFrequency;
    }
    private void Update()
    {
        _scanTimer -= Time.deltaTime;
        if(_scanTimer <= _scanInterval)
        {
            _scanTimer += _scanInterval;
            Scan();
        }
    }
    private void Scan()
    {
        _count = Physics.OverlapSphereNonAlloc(transform.position, _distance, _colliders, _layers, QueryTriggerInteraction.Collide);

        _objectsWithinSensor.Clear();
        for(int i = 0; i < _count; i++)
        {
            GameObject obj = _colliders[i].gameObject;
            if(IsInSight(obj) || IsMakingTooMuchNoise(obj))
            {
                _objectsWithinSensor.Add(obj);
            }
        }
    }
    private bool IsMakingTooMuchNoise(GameObject obj)
    {
        if(obj.tag == IStringDefinitions.PLAYER_TAG)
        {
            PlayerMovement playerMovement = obj.transform.parent.GetComponent<PlayerMovement>();
            float noiseCheck = 3;
            if(playerMovement.MoveSpeed > noiseCheck)
            {
                return true;
            } 
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }
    private bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if(direction.y < 0  || direction.y > _height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);

        if(deltaAngle > _angle)
        {
            return false;
        }

        //checks if there is any object between the player and enemy, like a wall or something
        origin.y += _height / 2;
        if(Physics.Linecast(origin, dest, _oclussionLayers))
        {
            return false;
        }
        return true;
    }
    private Mesh CreateWedgeMesh()
    {
        Mesh mesh =  new Mesh();

        int segments = 10;
        int numOfTriangles = (segments * 4) + 2 + 2;
        int numOfVertices = numOfTriangles * 3;

        Vector3[] vertices = new Vector3[numOfVertices];
        int[] triangles = new int[numOfVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * Vector3.forward * _distance;
        Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * Vector3.forward * _distance;

        Vector3 topCenter = bottomCenter + Vector3.up * _height;
        Vector3 topRight = bottomRight + Vector3.up * _height;
        Vector3 topLeft = bottomLeft + Vector3.up * _height;

        int vert = 0;

        //left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -_angle;
        float deltaAngle = (_angle * 2) / segments;
        for(int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, -currentAngle, 0) * Vector3.forward * _distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * _distance;

            topRight = bottomRight + Vector3.up * _height;
            topLeft = bottomLeft + Vector3.up * _height;

            //far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numOfVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void OnValidate()
    {
        //if we change the line of sight on the inspector this method will be called in order to ensure the changes are applied
        _mesh = CreateWedgeMesh();
        _scanInterval = 1.0f / _scanFrequency;
    }
    private void OnDrawGizmos()
    {
        if(_mesh)
        {
            Gizmos.color = _meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
        }
        Gizmos.DrawWireSphere(transform.position, _distance);
        
        for(int i = 0; i < _count; i++)
        {
            Gizmos.DrawSphere(_colliders[i].transform.position, 0.2f);
        } 
        
        Gizmos.color = Color.green;
        foreach(var obj in _objectsWithinSensor)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }
}
