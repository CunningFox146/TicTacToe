using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.Tests.Common.Infrastructure
{
    public class ZenjectPlayModeUnitTestFixture : ZenjectUnitTestFixture
    {
        public override void SetupGlobalContainer()
        {
            base.SetupGlobalContainer();
            
            var itemsContainer = new GameObject("Global Container");
            GlobalContainer.DefaultParent = itemsContainer.transform;
        }
        
        
        [OneTimeTearDown]
        public virtual void TearDownGlobalContainer()
        {
            Object.DestroyImmediate(GlobalContainer.DefaultParent.gameObject);
        }

        public override void SetupTestContainer()
        {
            base.SetupTestContainer();
            
            var itemsContainer = new GameObject("Test Container");
            SceneManager.MoveGameObjectToScene(itemsContainer, SceneManager.GetActiveScene());
            Container.DefaultParent = itemsContainer.transform;
        }
        
        public override void TearDownTestContainer()
        {
            base.TearDownTestContainer();
            Object.DestroyImmediate(Container.DefaultParent.gameObject);
        }
    }
}