using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TicTacToe.Tests.Common
{
    public abstract class ZenjectUnitTestFixture
    {
        protected DiContainer Container { get; private set; }

        [OneTimeSetUp]
        public virtual void Setup()
        {
            var itemsContainer = new GameObject("Test Objects Container");
            SceneManager.MoveGameObjectToScene(itemsContainer, SceneManager.GetActiveScene());
            
            Container = new DiContainer
            {
                DefaultParent = itemsContainer.transform
            };
        }
    }
}
