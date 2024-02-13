using System.Collections;
using NUnit.Framework;
using TicTacToe.Infrastructure.SceneManagement;
using TicTacToe.Tests.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace TicTacToe.Tests.PlayModeTests
{
    public class GameplaySceneTest : ZenjectUnitTestFixture
    {
        [OneTimeSetUp]
        public void LoadBootstrap()
        {
            SceneManager.LoadScene((int)SceneIndex.Boot);
        }

        [UnitySetUp]
        public IEnumerator LoadGameplay()
        {
            yield return new WaitForSeconds(0.25f);
            SceneManager.LoadScene((int)SceneIndex.Gameplay);
            yield return new WaitForSeconds(0.25f);
        }
    }
}