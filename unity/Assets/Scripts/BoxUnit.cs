using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BoxUnit : BlockUnit {

    [Range(0.0f, 1.0f)]
    [Header("方块翻转的速度")]
    public float m_Duration = 0.4f;

    public bool IsMoving = false;

    private MapVector2 positionInMap = new MapVector2();

    private Transform ts;

	// Use this for initialization
	void Start () {
        ts = transform;
        IsMoving = false;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public MapVector2 PositionInMap
    {
        get
        {
            return positionInMap;
        }
        set
        {
            positionInMap = value;
            SetPositionWithoutMove(new Vector3(value.x, value.y, Position.z));
        }
    }

    public void MoveDirection(Direction direction)
    {
        if (IsMoving)
            return;
        if (!canMove(direction))
        {
            return;
        }
        IsMoving = true; //start move to direction
        Vector3 axis = Vector3.up;
        Vector3 aroundPointSlef = Vector3.zero;
        switch (direction)
        {
            case Direction.Left:
                axis = Vector3.forward;
                aroundPointSlef = new Vector3(-0.5f, -0.5f, 0);
                PositionInMap += new MapVector2(-1, 0);
                break;
            case Direction.Up:
                axis = Vector3.right;
                aroundPointSlef = new Vector3(0, -0.5f, 0.5f);
                PositionInMap += new MapVector2(0, 1);
                break;
            case Direction.Right:
                axis = Vector3.back;
                aroundPointSlef = new Vector3(0.5f, -0.5f, 0);
                PositionInMap += new MapVector2(1, 0);
                break;
            case Direction.Down:
                axis = Vector3.left;
                aroundPointSlef = new Vector3(0, -0.5f, -0.5f);
                PositionInMap += new MapVector2(0, -1);
                break;
        }
        StartCoroutine(rotateAround(aroundPointSlef, axis, 90f, m_Duration));
    }

    public bool canMove(Direction direction)
    {
        MapVector2 tryMovePos = GetPositionAfterTryMove(direction);
        if (tryMovePos.x < 0 || tryMovePos.x >= MapManager.I.MapSize.x ||
            tryMovePos.y < 0 || tryMovePos.y >= MapManager.I.MapSize.y)
        {
            return false;
        }
        BoxUnit box = BoxManager.I.ExistBoxAtPos(tryMovePos);
        if (box != null)
        {
            Debug.Log("运动方向上有box");
            if (!box.canMove(direction))
                return false;
        }
        ElementType element = MapManager.I.GetElementAt(tryMovePos.x, tryMovePos.y);
        if (element == ElementType.Wall)
            return false;
        return true;
    }

    public MapVector2 GetPositionAfterTryMove(Direction direction)
    {
        MapVector2 rtn = new MapVector2();
        switch (direction)
        {
            case Direction.Left:
                rtn = new MapVector2(-1, 0);
                break;
            case Direction.Up:
                rtn = new MapVector2(0, 1);
                break;
            case Direction.Right:
                rtn = new MapVector2(1, 0);
                break;
            case Direction.Down:
                rtn = new MapVector2(0, -1);
                break;
        }
        rtn.x += Mathf.RoundToInt(Position.x);
        rtn.y += Mathf.RoundToInt(Position.y);
        return rtn;
    }

    IEnumerator rotateAround(Vector3 aroundPointSelf, Vector3 axis, float angle, float duration)
    {
        float totalTime = 0.0f;
        var originPos = ts.position;
        float rotateDeltaThisFrame = 0.0f;
        //如果持续时间为0，则为立即播放
        if (m_Duration == 0.0f)
        {
            ts.RotateAround(originPos + aroundPointSelf, axis, angle);
            yield return null;
        }
        else
        {
            while (true)
            {
                float deltaTime = Time.deltaTime;
                if (totalTime + deltaTime > duration)
                    deltaTime = duration - totalTime;
                totalTime += deltaTime;

                rotateDeltaThisFrame = deltaTime / duration * angle;
                ts.RotateAround(originPos + aroundPointSelf, axis, rotateDeltaThisFrame);
                if (Mathf.Approximately(totalTime, duration))
                {
                    IsMoving = false;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
