using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.Tests.Common.Infrastructure
{
    public class ZenjectPlayModeUnitTestFixture : ZenjectUnitTestFixture
    {
        [SetUp]
        public override void SetupTestContainer()
        {
            base.SetupTestContainer();
            
            var itemsContainer = new GameObject("Test Container");
            SceneManager.MoveGameObjectToScene(itemsContainer, SceneManager.GetActiveScene());
            Container.DefaultParent = itemsContainer.transform;
        }
        
        [TearDown]
        public override void ClearTestContainer()
        {
            base.ClearTestContainer();
            Object.DestroyImmediate(Container.DefaultParent.gameObject);
        }
    }
}