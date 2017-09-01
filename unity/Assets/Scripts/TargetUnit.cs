using UnityEngine;
using System.Collections;

public class TargetUnit : BlockUnit
{
    private SpringJoint _springJoint;
    private SpringJoint SpringJoint
    {
        get
        {
            if (_springJoint == null)
                _springJoint = GetComponent<SpringJoint>();
            if (_springJoint == null)
                Debug.LogError("SpringJoint丢失！");
            return _springJoint;
        }
    }

    public override Vector3 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
            transform.position = new Vector3(_position.x, _position.z + 0.05f, _position.y);
            SpringJoint.connectedAnchor = transform.position;
        }
    }

    private void Awake()
    {
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
