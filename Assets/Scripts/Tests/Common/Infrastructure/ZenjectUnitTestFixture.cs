using NUnit.Framework;
using Zenject;

namespace TicTacToe.Tests.Common.Infrastructure
{
    public abstract class ZenjectUnitTestFixture
    {
        protected DiContainer GlobalContainer { get; private set; }

        [OneTimeSetUp]
        public virtual void SetupGlobalContainer()
        {
            GlobalContainer = new DiContainer();
        }
    }
}
