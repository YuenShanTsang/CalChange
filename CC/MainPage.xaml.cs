namespace CC;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    private void CalculateChangeButton_Clicked(object sender, EventArgs e)
    {
        // Check if valid numbers are entered for price and money given
        if (double.TryParse(PriceEntry.Text, out double price) && double.TryParse(MoneyGivenEntry.Text, out double moneyGiven))
        {
            double change = moneyGiven - price;

            // Check if change is non-negative
            if (change >= 0)
            {
                // Calculate the denominations required for the change
                Dictionary<string, int> denominations = CalculateDenominations(change);

                // Display the calculated denominations
                DisplayChange(denominations);
            }
            else
            {
                // Display an error message if money given is less than the price
                DisplayAlert("Invalid Input", "Money given is less than the price.", "OK");
            }
        }
        else
        {
            // Display an error message if invalid numbers are entered for price and money given
            DisplayAlert("Invalid Input", "Please enter valid numbers for price and money given.", "OK");
        }
    }

    private Dictionary<string, int> CalculateDenominations(double change)
    {
        // Round the change amount to the nearest 5 cents
        double roundedChange = Math.Round(change * 20) / 20;

        // Define the denominations and their corresponding names
        double[] denominations = { 100, 50, 20, 10, 5, 2, 1, 0.25, 0.1, 0.05 };
        string[] denominationNames = { "100 Dollar Bills", "50 Dollar Bills", "20 Dollar Bills", "10 Dollar Bills", "5 Dollar Bills", "2 Dollar Coins", "1 Dollar Coins", "25 Cent Coins", "10 Cent Coins", "5 Cent Coins" };

        Dictionary<string, int> result = new Dictionary<string, int>();

        // Calculate the count of each denomination
        foreach (double denomination in denominations)
        {
            int count = (int)(roundedChange / denomination);
            if (count > 0)
            {
                // Add the denomination and its count to the result dictionary
                result.Add(denominationNames[Array.IndexOf(denominations, denomination)], count);

                // Update the remaining change after deducting the count of the current denomination
                roundedChange -= count * denomination;
            }
        }

        return result;
    }

    private void DisplayChange(Dictionary<string, int> denominations)
    {
        ChangeLabel.Text = "Change:\n";

        // Display the denominations and their counts in the UI
        foreach (KeyValuePair<string, int> denomination in denominations)
        {
            // Append the denomination name and its count to the 'ChangeLabel' text
            ChangeLabel.Text += $"{denomination.Key}: {denomination.Value}\n";
        }
    }
}


