using Battleship.Core;


var quitGameKey = "q";

var game = new Game();
game.StartNewGame();

Console.WriteLine("Let's play...");
Console.WriteLine("Type the selected location in the format '[Column][Row]', e.g. 'A3' and press enter to make the move.");
Console.WriteLine($"You can finish the game at any moment by pressing '{quitGameKey}'. Good luck!");

while(true)
{
    var coordinates = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(coordinates))
    {
        Console.WriteLine("Type the selected location in the format '[Column][Row]', e.g. 'A3' and press enter to make the move.");
    }
    else if (coordinates == quitGameKey)
    {
        Console.WriteLine("See you next time!");
        break;
    } 
    else { 
        try
        {
            var moveResult = game.Shoot(coordinates);
            Console.WriteLine($"Result: '{moveResult}'");
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Have you tried shooting non-existing location? {ex.Message}");
            Console.WriteLine($"Let's try again!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong. Please, contact developer to help you out or try again!");
        }
    }
}
