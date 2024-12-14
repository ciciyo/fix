using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public enum GAMESTATE
    {
        STARTING, PLAYING, PAUSED, ENDING
    }

    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager instance;

        public GAMESTATE gameState;


        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            StartGame();
        }


        public void StartGame()
        {
            gameState = GAMESTATE.PLAYING;
        }

        public void EndGame()
        {
            gameState = GAMESTATE.ENDING;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}