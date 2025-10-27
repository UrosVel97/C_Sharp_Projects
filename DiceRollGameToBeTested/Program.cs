using DiceRollGameToBeTested.Game;
using DiceRollGameToBeTested.UserCommunication;

var random = new Random();
var dice = new Dice(random);
var userCommunication = new ConsoleUserCommunication();
var guessingGame = new GuessingGame(dice, userCommunication);

GameResult gameResult = guessingGame.Play();
guessingGame.PrintResult(gameResult);

Console.ReadKey();

