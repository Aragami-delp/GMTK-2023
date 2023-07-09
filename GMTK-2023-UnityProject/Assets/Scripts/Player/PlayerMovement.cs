using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance { get; private set; }
    [SerializeField]
    private Animator animator;

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

        animator = GetComponent<Animator>();

    }

    public bool IsMoving { get; set; }

    [SerializeField]
    [Range(1,2)]
    private float movespeed = 1;

    public void MovePlayer() 
    {
        IsMoving = true;
        animator.SetBool("Walk",true);
        Debug.Log("Start moving");
    }

    private void Update()
    {
        if (IsMoving)
        { 
            transform.position += new Vector3(movespeed  * -1 , 0, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        IsMoving = false;
        animator.SetBool("Walk", false);

        if (_other.transform.parent.TryGetComponent<TileWorld>(out TileWorld _tileWorld))
        {
            TileEventManager.Instance.StartEvent(_tileWorld, _tileWorld.GetEvent);
        }
        Debug.Log("Stop moving");
    }

}
