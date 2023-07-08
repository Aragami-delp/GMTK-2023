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

    public void StartEvent(TileWorld _tileWorld, EVENTTYPE eventtype)
    {
        switch (eventtype)
        {
            case EVENTTYPE.FIGHT:
                //TODO: Start fight + UI
                Debug.Log("Start FIGHT event");
                FightManager.Instance.StartFight(TileManager.Instance.GetCurrentTile().CurrentBiom);
                break;
            case EVENTTYPE.LOOT:
                //TODO: Item system give loot + UI
                Debug.Log("Start LOOT event");
                break;
            case EVENTTYPE.REST:
                //TODO: Player give health + UI
                Debug.Log("Start REST event");
                break;
            case EVENTTYPE.INTERACTION:
                //TODO: Planed
                Debug.Log("Start INTERACTION event");
                RunNpcAction(_tileWorld);
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

    private void RunNpcAction(TileWorld _tileWorld)
    {
        InteractionSO chosenNpcAction = _tileWorld.GetTileSo().interactions[_tileWorld.GetTileSo().getChosenInteraction()];

        if (chosenNpcAction.PlayerReactions.Length != 0)
        {
            return;
        }

        PlayerReactionSO playerReactionSo =
            chosenNpcAction.PlayerReactions[UnityEngine.Random.Range(0, chosenNpcAction.PlayerReactions.Length)];
        
        StartEvent(_tileWorld, playerReactionSo.Eventtype);
    }
}
