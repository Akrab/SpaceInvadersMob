using System;
using System.Collections;
using System.Collections.Generic;
using SpaceInvadersMob.Game.Actors.Enemy;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game
{
    public interface IEnemyWave
    {
        void Create();
    }
    public class EnemyWave : MonoBehaviour, IEnemyWave
    {

        [SerializeField] private EnemyPoint[] _points;
        
        

        public void Create()
        {
            
        }
        
        [Serializable]
        private class EnemyPoint
        {
            [SerializeField] private Transform _point;
            [SerializeField] private EnemyType _enemyType;
            
            
            
        }
    }

  
}
