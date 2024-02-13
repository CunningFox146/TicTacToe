using NUnit.Framework;
using Zenject;

namespace TicTacToe.Tests.Common
{
    public abstract class ZenjectUnitTestFixture
    {
        protected DiContainer Container { get; private set; }

        [OneTimeSetUp]
        public virtual void Setup()
        {
            Container = new DiContainer();
        }
    }
}
