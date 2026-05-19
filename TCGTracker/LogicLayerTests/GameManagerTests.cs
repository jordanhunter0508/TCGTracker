using DataAccessFakes;
using DataDomain;
using LogicLayer;

namespace LogicLayerTests;

[TestClass]
public class GameManagerTests
{
    private GameManager _gameManager;
    private Game _newGame;

    [TestInitialize]
    public void TestSetup()
    { 
        _gameManager = new GameManager(new GameAccessorFakes());

        _newGame = new Game()
        { 
            Name = "New Name",
            Publisher = "Publisher",
            OfficialWebsite = "https://www.newGame.com",
        };
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

    [TestMethod]
    public void TestAddGameReturnsIDWithValidGame() 
    {
        // arrange
        const int expected = 4;
        int acutal = 0;

        // act
        acutal = _gameManager.AddGame(_newGame);

        // assert
        Assert.AreEqual(expected, acutal);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddGameThrowsArgumentNullExceptionWithNullGame() 
    {
        // arrange
        _newGame = null;
        int acutal = 0;

        // act
        acutal = _gameManager.AddGame(_newGame);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddGameThrowsApplicationExceptionWithNullName() 
    {
        // arrange
        _newGame.Name = null;
        int acutal = 0;

        // act
        acutal = _gameManager.AddGame(_newGame);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddGameThrowsApplicationExceptionWithNullPublisher() 
    {
        // arrange
        _newGame.Publisher = null;
        int acutal = 0;

        // act
        acutal = _gameManager.AddGame(_newGame);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddGameThrowsApplicationExceptionWithDuplicateName() 
    {
        // arrange
        int acutal = 0;

        // act
        acutal = _gameManager.AddGame(_newGame);
        acutal = _gameManager.AddGame(_newGame);

        // assert
        // do nothing
    }
}
