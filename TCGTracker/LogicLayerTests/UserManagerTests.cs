using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTests;

[TestClass]
public class UserManagerTests
{
    private IUserManager _userManager = null;

    [TestInitialize]
    public void TestSetup()
    {
        _userManager = new UserManager(new UserAccessorFakes());
    }

    [TestMethod]
    public void TestHashSha256ReturnsCorrectHashValue()
    {
        // arrange
        const string password = "newuser";
        const string expectedValue = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
        string actual = null;

        // act
        actual = _userManager.HashSha256(password);

        // assert
        Assert.AreEqual(expectedValue, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestHashSha256ThrowsArgumentExceptionWithNullPassword()
    {
        // arrange
        const string password = null;
        string actual = null;

        // act
        actual = _userManager.HashSha256(password);

        // assert
        // nothing to do
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestHashSha256ThrowsArgumentExceptionWithBlankPassword()
    {
        // arrange
        const string password = "";
        string actual = null;

        // act
        actual = _userManager.HashSha256(password);

        // assert
        // nothing to do
    }

    [TestMethod]
    public void TestAuthenticateUserReturnsTrueWithValidInputs()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "newuser";
        const bool expectedValue = true;
        bool actual = false;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actual);
    }

    [TestMethod]
    public void TestAuthenticateUserReturnsFalseWithInvalidEmail()
    {
        // arrange
        const string email = "fails";
        const string password = "newuser";
        const bool expectedValue = false;
        bool actual = true;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actual);
    }

    [TestMethod]
    public void TestAuthenticateUserReturnsFalseWithInvalidPassword()
    {
        // arrange
        const string email = "testuser@test.com";
        const string password = "fails";
        const bool expectedValue = false;
        bool actual = true;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actual);
    }

    [TestMethod]
    public void TestAuthenticateUserWithInactiveUser()
    {
        // arrange
        const string email = "testuser5@test.com";
        const string password = "newuser";
        const bool expectedValue = false;
        bool actual = true;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        Assert.AreEqual(expectedValue, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAuthenticateUserThrowsArgumentExceptionWithNullEmail()
    {
        // arrange
        const string email = null;
        const string password = "newuser";
        const bool expectedValue = false;
        bool actual = true;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAuthenticateUserThrowsArgumentExceptionWithBlankEmail()
    {
        // arrange
        const string email = "";
        const string password = "newuser";
        const bool expectedValue = false;
        bool actual = true;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAuthenticateUserThrowsArgumentExceptionWithNullPassword()
    {
        // arrange
        const string email = "testuser5@test.com";
        const string password = null;
        const bool expectedValue = false;
        bool actual = true;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAuthenticateUserThrowsArgumentExceptionWithBlankPassword()
    {
        // arrange
        const string email = "testuser5@test.com";
        const string password = "";
        const bool expectedValue = false;
        bool actual = true;

        // act
        actual = _userManager.AuthenticateUser(email, password);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetUserByEmailReturnsCorrectUser()
    {
        // arrange
        const string email = "testuser1@test.com";
        User expectedUser = new User()
        {
            UserID = 1,
            GivenName = "test",
            Surname = "user",
            Email = "testuser1@test.com",
            Active = true,
        };
        User actual;

        // act
        actual = _userManager.GetUserByEmail(email);

        // assert
        Assert.AreEqual(expectedUser.UserID, actual.UserID);
        Assert.AreEqual(expectedUser.GivenName, actual.GivenName);
        Assert.AreEqual(expectedUser.Surname, actual.Surname);
        Assert.AreEqual(expectedUser.Email, actual.Email);
        Assert.AreEqual(expectedUser.Active, actual.Active);
    }

    [TestMethod]
    public void TestGetUserByEmailReturnsNullWithInvalidEmail()
    {
        // arrange
        const string email = "fails";
        User expected = null;
        User actual;

        // act
        actual = _userManager.GetUserByEmail(email);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetUserByEmailThrowsArgumentExceptionWithNullEmail()
    {
        // arrange
        const string email = null;
        User actual;

        // act
        actual = _userManager.GetUserByEmail(email);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetUserByEmailThrowsArgumentExceptionWithBlankEmail()
    {
        // arrange
        const string email = "";
        User actual;

        // act
        actual = _userManager.GetUserByEmail(email);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestGetRolesForUserReturnsCorrectListOfRoles()
    {
        // arrange
        const string email = "testuser1@test.com";
        const int expectedSize = 2;
        const string role1 = "testRole1";
        const string role2 = "testRole2";
        List<string> actual;

        // act
        actual = _userManager.GetRolesForUser(email);

        // assert
        Assert.AreEqual(expectedSize, actual.Count);
        Assert.AreEqual(role1, actual[0]);
        Assert.AreEqual(role2, actual[1]);
    }

    [TestMethod]
    public void TestGetRolesForUserWithNoRoles()
    {
        // arrange
        const string email = "testuser3@test.com";
        const int expectedSize = 0;
        List<string> actual;

        // act
        actual = _userManager.GetRolesForUser(email);

        // assert
        Assert.AreEqual(expectedSize, actual.Count);
    }

    [TestMethod]
    public void TestGetRolesForUserReturnsBlankListForInvalidEmail()
    {
        // arrange
        const string email = "testloser1@test.com";
        const int expectedSize = 0;
        List<string> actual;

        // act
        actual = _userManager.GetRolesForUser(email);

        // assert
        Assert.AreEqual(expectedSize, actual.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetRolesForUserThrowsArgumentExceptionWithNullEmail()
    {
        // arrange
        const string email = null;
        const int expectedSize = 0;
        List<string> actual;

        // act
        actual = _userManager.GetRolesForUser(email);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetRolesForUserThrowsArgumentExceptionWithBlankEmail()
    {
        // arrange
        const string email = "";
        const int expectedSize = 0;
        List<string> actual;

        // act
        actual = _userManager.GetRolesForUser(email);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestLogInUserReturnsCorrectUserVM()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "newuser";
        const int expectedID = 1;
        const int expectedRoleCount = 2;
        const string role1 = "testRole1";
        const string role2 = "testRole2";
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LoginUser(email, password);

        // assert
        Assert.AreEqual(expectedID, actualUserVM.UserID);
        Assert.AreEqual(expectedRoleCount, actualUserVM.Roles.Count);
        Assert.AreEqual(role1, actualUserVM.Roles[0]);
        Assert.AreEqual(role2, actualUserVM.Roles[1]);
    }

    [TestMethod]
    public void TestLogInUserReturnsNullWithInvalidEmail()
    {
        // arrange
        const string email = "fails";
        const string password = "newuser";
        UserVM expected = null;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LoginUser(email, password);

        // assert
        Assert.AreEqual(expected, actualUserVM);
    }

    [TestMethod]
    public void TestLogInUserReturnsNullWithInvalidPassword()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "fails";
        UserVM expected = null;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LoginUser(email, password);

        // assert
        Assert.AreEqual(expected, actualUserVM);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestLogInUserThowsArgumentExceptionWithNullEmail()
    {
        // arrange
        const string email = null;
        const string password = "newuser";
        UserVM expected = null;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LoginUser(email, password);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestLogInUserThowsArgumentExceptionWithBlankEmail()
    {
        // arrange
        const string email = "";
        const string password = "newuser";
        UserVM expected = null;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LoginUser(email, password);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestLogInUserThowsArgumentExceptionWithNullPassword()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = null;
        UserVM expected = null;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LoginUser(email, password);

        // assert
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestLogInUserThowsArgumentExceptionWithBlankPassword()
    {
        // arrange
        const string email = "testuser1@test.com";
        const string password = "";
        UserVM expected = null;
        UserVM actualUserVM = null;

        // act
        actualUserVM = _userManager.LoginUser(email, password);

        // assert
        // do nothing
    }
}
