using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // reference to the game manager instance (SINGLETON)

    public InputReader inputReader;// reference to the input reader INPORTANT FOR INPUTS TO WORK PROPERLY IN THE GAME

    [Header("Player")]
    public Animator staffAnimator; // reference to the staff animator (animator of player weapon) used for puzzle resolution

    public ObjectPool<Bullet> fireBulletPool; // object pool for fire bullets
    public ObjectPool<Bullet> waterBulletPool; // object pool for water bullets

    public Transform poolParent, playerPosition; // parent for the object pools and player position

    [Header("Puzzle")]
    public bool puzzle1, puzzle2, puzzle3; // Flags to check if the puzzle is completed
    private bool _puzzle1done, _puzzle2done, _puzzle3done;// Flags to check if the puzzle is completed
    private float _puz1flag, _puz2flag, _puz3flag, _totalPuz1Peds, _totalPuz2Peds, _totalPuz3Peds;// Flags to check if the puzzle is completed



    public Material puzzle1Mat, puzzle2Mat, puzzle3Mat;//Materials for the puzzles Reference

    public List<Pedestal> puzz1Ped, puzz2Ped, puzz3Ped;

    public ShieldShader Shield; //Shader for the shield effect Reference


    void Awake()
    {
        if (instance == null) { instance = this; } //Initialize the singleton
        else { Destroy(this); }
    }

    private void Start()
    {
        //intialized the object pools
        //---------------------------------------------------//
        fireBulletPool = new ObjectPool<Bullet>("FireBullet", poolParent);
        waterBulletPool = new ObjectPool<Bullet>("WaterBullet", poolParent);
        //---------------------------------------------------//

        //initialize Material Keywords
        //---------------------------------------------------//
        puzzle1Mat.DisableKeyword("_EMISSION");
        puzzle2Mat.DisableKeyword("_EMISSION");
        puzzle3Mat.DisableKeyword("_EMISSION");
        //---------------------------------------------------//

        //initialize the flags
        //---------------------------------------------------//
        _totalPuz1Peds = puzz1Ped.Count;
        _totalPuz2Peds = puzz2Ped.Count;
        _totalPuz3Peds = puzz3Ped.Count;
        //---------------------------------------------------//
    }

    private void Update()
    {
        CheckCompletePuzzle();

        CheckShield();
    }
    private void CheckCompletePuzzle() 
    {
        if (puzz1Ped != null)
        {
            foreach (Pedestal ped in puzz1Ped)
            {
                if (ped.completed) { _puz1flag += 1; puzz1Ped.Remove(ped); }
                if (_puz1flag >= _totalPuz1Peds) { puzzle1 = true; }
            }
        }
        if (puzz2Ped != null)
        {
            foreach (Pedestal ped in puzz2Ped)
            {
                if (ped.completed) { _puz2flag += 1; puzz2Ped.Remove(ped); }
                if (_puz2flag >= _totalPuz2Peds) { puzzle2 = true; }
            }
        }
        if (puzz3Ped != null)
        {
            foreach (Pedestal ped in puzz3Ped)
            {
                if (ped.completed) { _puz3flag += 1; puzz3Ped.Remove(ped); }
                if (_puz3flag >= _totalPuz3Peds) { puzzle3 = true; }
            }
        }
    }

    private void CheckShield()
    {
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            Shield.OpenCloseShield();
        }
    }
}
