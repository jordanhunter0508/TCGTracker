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

    [TestMethod]
    public void TestGetGameWithValidGameID()
    {
        // arrange
        const int gameID = 1;
        Game expected = new Game()
        {
            GameID = 1,
            Name = "Game Name 1",
            Publisher = "Publisher 1",
            OfficialWebsite = "https://wwww.website1.com",
            Active = true,
        };
        Game actual = null;

        // act
        actual = _gameManager.GetGame(gameID);

        // assert
        Assert.AreEqual(expected.GameID, actual.GameID);
        Assert.AreEqual(expected.Name, actual.Name);
        Assert.AreEqual(expected.OfficialWebsite, actual.OfficialWebsite);
    }

    [TestMethod]
    public void TestGetGameWithInvalidGameID()
    {
        // arrange
        const int gameID = 999;
        Game expected = null;
        Game actual = new Game();

        // act
        actual = _gameManager.GetGame(gameID);

        // assert
        Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    public void TestEditGameReturnsTrueWithValidGame()
    {
        // arrange
        _newGame.GameID = 1;
        bool expected = true;
        bool actual = false;
        // act
        actual = _gameManager.EditGame(_newGame);

        // assert
        Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    public void TestEditGameReturnsFalseWithInvalidGameID()
    {
        // arrange
        _newGame.GameID = 999;
        bool expected = false;
        bool actual = true;
        // act
        actual = _gameManager.EditGame(_newGame);

        // assert
        Assert.AreEqual(expected,actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestEditGameThrowsArgumentNullExceptionnWithNullGame() 
    {
        // arrange
        Game updatedGame = null;
        bool actual = false;

        // act
        actual = _gameManager.EditGame(updatedGame);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditGameThrowsApplicationExceptionWithNullName() 
    {
        // arrange
        _newGame.GameID = 1;
        _newGame.Name = "Game Name 3";
        bool actual = false;

        // act
        actual = _gameManager.EditGame(_newGame);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditGameThrowsApplicationExceptionWithNullPublisher() 
    {
        // arrange
        _newGame.GameID = 1;
        _newGame.Name = null;
        bool actual = false;

        // act
        actual = _gameManager.EditGame(_newGame);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditGameThrowsApplicationExceptionWithDuplicateName() 
    {
        // arrange
        _newGame.GameID = 1;
        _newGame.Publisher = null;
        bool actual = false;

        // act
        actual = _gameManager.EditGame(_newGame);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestActivateGameReturnsTrueWithValidGameID()
    { 
        // arrange
        const int gameID = 1;
        const bool active = false;
        const bool expected = true;
        bool actual = false;

        // act
        actual = _gameManager.ActivateGame(gameID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestActivateGameReturnsFalseWithInvalidGameID()
    { 
        // arrange
        const int gameID = 999;
        const bool active = false;
        const bool expected = false;
        bool actual = true;

        // act
        actual = _gameManager.ActivateGame(gameID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }
}
