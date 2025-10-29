using PasswordGenerator;

var passwordGenerator = new PasswordGenerator.PasswordGenerator(new RandomWrapper());

for (int i = 0; i < 10; i++)
{
    Console.WriteLine(passwordGenerator.Generate(5, 10, true));
}
Console.ReadKey();




