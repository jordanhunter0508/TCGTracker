using DataAccessFakes;
using DataDomain;
using LogicLayer;

namespace LogicLayerTests;

[TestClass]
public class GameManagerTests
{
    private GameManager _gameManager;

    [TestInitialize]
    public void TestSetup()
    { 
        _gameManager = new GameManager(new GameAccessorFakes());
    }


    [TestMethod]
    public void TestGetAllGamesReturnsFullList()
    {
        // arrange
        const int expectedCount = 3;
        List<Game> actual;

        // act
        actual = _gameManager.GetAllGames();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }
}
