using UnityEngine;
using System.Collections;

public class BlockUnit : MonoBehaviour
{

    protected Vector3 _position = Vector3.zero;

    public virtual Vector3 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
            transform.position = new Vector3(_position.x, _position.z + 0.5f, _position.y);
        }
    }

    public void SetPositionWithoutMove(Vector3 newPos)
    {
        _position = new Vector3(newPos.x, newPos.y, _position.z);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
