using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEventManager : MonoBehaviour
{
    public static TileEventManager Instance { get; private set; }

    private void Awake()
    {
        #region Singleton
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        #endregion
    }

    public void StartEvent(EVENTTYPE _type)
    {
        switch (_type)
        {
            case EVENTTYPE.FIGHT:
                //TODO: Start fight + UI
                Debug.Log("Start FIGHT event");
                break;
            case EVENTTYPE.LOOT:
                //TODO: Item system give loot + UI
                Debug.Log("Start LOOT event");
                break;
            case EVENTTYPE.REST:
                //TODO: Player give health + UI
                Debug.Log("Start REST event");
                break;
            case EVENTTYPE.NPC:
                //TODO: Planed
                Debug.Log("Start NPC event");
                break;
            case EVENTTYPE.NONE:
            default:
                Debug.Log("Start NONE event");
                EndEvent();
                break;
        }
    }

    public void EndEvent()
    {
        Debug.Log("End event");
        TileManager.Instance.StartNextTileTurn();
    }
}
