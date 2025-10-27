using DiceRollGameToBeTested.Game;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceRollGameToBeTested.UserCommunication;
using DiceRollGameToBeTested;

namespace DiceRollGameTests;

[TestFixture]
public class GuessingGameTests
{
    private Mock<IDice> _diceMock;
    private Mock<IUserCommunication> _userCommunicationMock;
    private GuessingGame _cut;

    [SetUp]
    public void Setup()
    {
        _diceMock = new Mock<IDice>();
        _userCommunicationMock = new Mock<IUserCommunication>();
        _cut = new GuessingGame(_diceMock.Object, _userCommunicationMock.Object);
    }


    [Test]
    public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnFirstTry()
    {
        const int NumberOnDie = 3;

        _diceMock.Setup(mock => mock.Roll()).Returns(NumberOnDie);
        _userCommunicationMock
            .Setup(mock => mock.ReadInteger(It.IsAny<string>()))
            .Returns(NumberOnDie);


        var gameResult = _cut.Play();

        Assert.AreEqual(GameResult.Victory, gameResult);
    }


    [Test]
    public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnThirdTry()
    {
        SetupUserGuessingTheNumberOnTheThirdTry();


        var gameResult = _cut.Play();

        Assert.AreEqual(GameResult.Victory, gameResult);
    }

    [Test]
    public void Play_ShallReturnLoss_IfTheUserNeverGuessesTheNumber()
    {
        const int NumberOnDie = 3;
        const int UserNumber= 1;

        _diceMock.Setup(mock => mock.Roll()).Returns(NumberOnDie);
        _userCommunicationMock
            .Setup(mock => mock.ReadInteger(It.IsAny<string>()))
            .Returns(UserNumber);


        var gameResult = _cut.Play();

        Assert.AreEqual(GameResult.Loss, gameResult);
    }

    [Test]
    public void Play_ShallReturnLoss_IfTheUserGuessesTheNumberOnFourthTry()
    {
        const int NumberOnDie = 3;

        _diceMock.Setup(mock => mock.Roll()).Returns(NumberOnDie);
        _userCommunicationMock
            .SetupSequence(mock => mock.ReadInteger(It.IsAny<string>()))
            .Returns(1)
            .Returns(2)
            .Returns(5)
            .Returns(NumberOnDie);


        var gameResult = _cut.Play();

        Assert.AreEqual(GameResult.Loss, gameResult);
    }

    [TestCase(GameResult.Victory,"You win!")]
    [TestCase(GameResult.Loss, "You lose :(")]
    public void PrintResult_ShallShowProperMessageForGameResult(GameResult gameResult, string expectedMessage)
    {
        _cut.PrintResult(gameResult);
        _userCommunicationMock.Verify(mock => mock.ShowMessage(expectedMessage));
    }

    [Test]
    public void Play_ShallShowWelcomeMessage()
    {
        var gameResult = _cut.Play();

        _userCommunicationMock.Verify(mock => mock
        .ShowMessage("Dice rolled. Guess what number it shows in 3 tries."),
        Times.Once());
    }

    [Test]
    public void Play_ShallAskForNumberThreeTimes_IfTheUserGuessesTheNumberOnThirdTry()
    {
        SetupUserGuessingTheNumberOnTheThirdTry();

        var gameResult = _cut.Play();

        _userCommunicationMock.Verify(mock => mock
                             .ReadInteger(Resource.EnterNumberMessage),
                             Times.Exactly(3));
    }

    [Test]
    public void Play_ShallShowWrongNumberTwice_IfTheUserGuessesTheNumberOnThirdTry()
    {
        SetupUserGuessingTheNumberOnTheThirdTry();

        var gameResult = _cut.Play();

        _userCommunicationMock.Verify(mock => mock
                             .ShowMessage("Wrong number."),
                             Times.Exactly(2));
    }

    private void SetupUserGuessingTheNumberOnTheThirdTry()
    {
        const int NumberOnDie = 3;

        _diceMock.Setup(mock => mock.Roll()).Returns(NumberOnDie);
        _userCommunicationMock
            .SetupSequence(mock => mock.ReadInteger(It.IsAny<string>()))
            .Returns(1)
            .Returns(2)
            .Returns(NumberOnDie);
    }
}
