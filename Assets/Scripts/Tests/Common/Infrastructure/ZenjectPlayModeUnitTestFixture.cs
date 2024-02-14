using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TicTacToe.Tests.Common.Infrastructure
{
    public class ZenjectPlayModeUnitTestFixture : ZenjectUnitTestFixture
    {
        protected DiContainer Container { get; private set; }
        
        [SetUp]
        public virtual void SetupTestContainer()
        {
            var itemsContainer = new GameObject("Test Container");
            SceneManager.MoveGameObjectToScene(itemsContainer, SceneManager.GetActiveScene());
            
            Container = GlobalContainer.CreateSubContainer();
            Container.DefaultParent = itemsContainer.transform;
        }
        
        [TearDown]
        public void ClearTestContainer()
        {
            Container.UnbindAll();
            Object.DestroyImmediate(Container.DefaultParent.gameObject);
        }
    }
}