using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnButton : MonoBehaviour
{
    [SerializeField] private RectTransform m_parent;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TileButton x = TileManager.Instance.GetNewRandomMapTile(BIOM.WOODS);
            x.transform.SetParent(m_parent, false);
        }
    }
}
