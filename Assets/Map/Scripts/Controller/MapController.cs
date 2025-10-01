using UnityEngine;
using Utils.DesignPattern.Singleton;

public enum PositionType
{
    None = 0,
    BottomLeft = 1,
    UpperRight = 2
}

namespace Map.Controller
{
    public class MapController : SingletonMono<MapController>
    {
        [Header("Map Controller Setting")]
        [SerializeField] private GameObject startObject;
        [SerializeField] private GameObject endObject;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] public int maxTryRatio = 10;

        private Vector3 _minPos;
        private Vector3 _maxPos;

        public int MaxTryCount = 0;

        private void Start()
        {
            this.SetInit();
        }

        private void SetInit()
        {
            Vector3 startPos = GetPosition(startObject, PositionType.BottomLeft);
            Vector3 endPos = GetPosition(endObject, PositionType.UpperRight);

            this._minPos = new Vector3(startPos.x, 0f, startPos.z);
            this._maxPos = new Vector3(endPos.x, 0f, endPos.z);
        }

        public Vector3 GetPosition(GameObject go, PositionType type)
        {
            Bounds bound = go.GetComponent<MeshRenderer>().bounds;
            Vector3 pos = new Vector3(-1000, -1000) ; 
            if (type == PositionType.BottomLeft)
            {
                pos = bound.min;
            }
            else
            {
                pos = bound.max;
            }
            return pos;
        }

        public Vector3 GetEmptyPosFollowR(Vector3 parentPos, float r)
        {
            if (this.MaxTryCount <= 0)
            {
                return new Vector3(-1000f, -1000f, -1000f);
            }

            this.MaxTryCount--;
            Vector3 result = new Vector3(-1000f, -1000f, -1000f);
            float x = Random.Range(_minPos.x, _maxPos.x);
            float z = Random.Range(_minPos.z, _maxPos.z);
            
            Vector3 pos = new Vector3(x, 0f, z);
            pos.y = 100f;
            
            float distance = Vector3.Distance(parentPos, pos);
            
            if (Physics.Raycast(pos, Vector3.down, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.tag.ToString() != "Ground")
                {
                    result = GetEmptyPosFollowR(parentPos, r);
                }
                result = hitInfo.point;
            }

            if (result != new Vector3(-1000f, -1000f, -1000f))
            {
                if (Vector3.Distance(pos, parentPos) < r)
                {
                    result = GetEmptyPosFollowR(parentPos, r);
                }
            }

            return result;

        }

        public bool CheckRange(Vector3 pos)
        {
            float minX = Mathf.Min(_minPos.x, _maxPos.x);
            float maxX = Mathf.Max(_minPos.x, _maxPos.x);

            float minY = Mathf.Min(_minPos.y, _maxPos.y);
            float maxY = Mathf.Max(_minPos.y, _maxPos.y);

            float minZ = Mathf.Min(_minPos.z, _maxPos.z);
            float maxZ = Mathf.Max(_minPos.z, _maxPos.z);

            return (pos.x >= minX && pos.x <= maxX) &&
                   (pos.y >= minY && pos.y <= maxY) &&
                   (pos.z >= minZ && pos.z <= maxZ);
        }
    }
}
