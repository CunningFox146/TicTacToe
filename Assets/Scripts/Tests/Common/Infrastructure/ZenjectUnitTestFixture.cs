using NUnit.Framework;
using Zenject;

namespace TicTacToe.Tests.Common.Infrastructure
{
    public abstract class ZenjectUnitTestFixture
    {
        
        protected DiContainer Container { get; private set; }
        protected DiContainer GlobalContainer { get; private set; }

        [OneTimeSetUp]
        public virtual void SetupGlobalContainer()
        {
            GlobalContainer = new DiContainer();
        }
        
        [SetUp]
        public virtual void SetupTestContainer()
        {
            Container = GlobalContainer.CreateSubContainer();
        }
        
        
        [TearDown]
        public virtual void ClearTestContainer()
        {
            Container.UnbindAll();
        }
    }
}
