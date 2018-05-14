using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Logic
{

    public interface EffectDispatcher
    {
        void Dispatch(Effect effect);
        void Dispatch(Action effect);
    }

    public interface Effect
    {
        void Act();
    }

    public class GameMachine
    {
        public GameMachine(EffectDispatcher dispatcher): this(State.MENU, dispatcher)
        {
        }

        public GameMachine(State startingState, EffectDispatcher dispatcher)
        {
            GameState = startingState;
            Dispatcher = dispatcher;
        }

        private EffectDispatcher Dispatcher { get; set; }

        public State GameState { get; private set; }

        public void SetState(State state)
        {
            switch (state)
            {
                case State.MENU:
                    SetMenu();
                    break;
                case State.PAUSE:
                    SetPause();
                    break;
                case State.GAME:
                    SetGame();
                    break;
                case State.ENDING:
                    SetEnding();
                    break;
            }
        }

        private void SetMenu()
        {
            if (GameState != State.ENDING && GameState != State.PAUSE) throw new ArgumentException();
            GameState = State.MENU;
        }

        private void SetGame()
        {
            if (GameState != State.MENU && GameState != State.PAUSE) throw new ArgumentException();
            GameState = State.GAME;
            Dispatcher.Dispatch(delegate { Debug.Log("GameStarted"); });
        }

        private void SetPause()
        {
            if (GameState != State.GAME) throw new ArgumentException();
            GameState = State.PAUSE;
        }

        private void SetEnding()
        {
            if (GameState != State.GAME) throw new ArgumentException();
            GameState = State.ENDING;
        }

        public enum State
        {
            MENU,
            PAUSE,
            GAME,
            ENDING
        }


    }
}