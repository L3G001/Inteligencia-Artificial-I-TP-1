using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InputReader _inputReader;

    [Header("Player")]
    public Animator staffAnimator;

    public ObjectPool<Bullet> fireBulletPool;
    public ObjectPool<Bullet> waterBulletPool;
    public Transform poolParent;

    [Header("Puzzle")]
    public bool puzzle1, puzzle2, puzzle3;
    private bool _puzzle1done, _puzzle2done, _puzzle3done;
    public Material puzzle1Mat, puzzle2Mat, puzzle3Mat;
    public ShieldShader Shield;
    public float puzflag;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        fireBulletPool = new ObjectPool<Bullet>("FireBullet", poolParent);
        waterBulletPool = new ObjectPool<Bullet>("WaterBullet", poolParent);
        puzzle1Mat.DisableKeyword("_EMISSION");
        puzzle2Mat.DisableKeyword("_EMISSION");
        puzzle3Mat.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (puzflag >= 4) { puzzle1 = true; }
        if (puzflag >= 8) { puzzle2 = true; }
        if (puzflag >= 10) { puzzle3 = true; }
        if (puzzle1 && !_puzzle1done) 
        { 
            Shield.OpenCloseShield(); 
            puzzle1Mat.EnableKeyword("_EMISSION");
            _puzzle1done = true;
        }
        if (puzzle2 && !_puzzle2done) 
        { 
            Shield.OpenCloseShield(); 
            puzzle2Mat.EnableKeyword("_EMISSION");
            _puzzle2done = true;
        }
        if (puzzle3 && !_puzzle3done) 
        { 
            Shield.OpenCloseShield(); 
            puzzle3Mat.EnableKeyword("_EMISSION");
            _puzzle3done = true;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Shield.OpenCloseShield();
        }
    }
}
