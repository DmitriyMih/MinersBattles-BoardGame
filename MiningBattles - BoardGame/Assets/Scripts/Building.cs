using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer mainRender;
    public float scale = 0.125f;
    public Vector2Int size = Vector2Int.one;

    public void SetTransparent(bool available)
    {
        if (available)
            mainRender.material.color = Color.yellow;
        else
            mainRender.material.color = Color.red;
    }

    public void SetNormalCOlor()
    {
        mainRender.material.color = Color.white;
    }

    public void OnDrawGizmosSelected()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if ((x + y) % 2 == 0)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.yellow;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y) * scale, new Vector3(1, 1f, 1) * scale);
            }
        }
    }

}
