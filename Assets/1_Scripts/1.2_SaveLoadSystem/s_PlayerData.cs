
// the following allows it to be saved in files
[System.Serializable]
public class s_PlayerData
{
    // PlayerData is like a package where we will take the goodies from

    #region<Data Initializers>
    // the level players were up to
    public int level;

    // the player's current amount of money
    public int currency;

    // future booleans to check what the apartment should look like
        // make a method that loads the proper furniture when loading into the HUB

    // if needed any other future data to be saved

    #endregion

    public s_PlayerData(s_Player playerInfo)
    {
        // take the game's information and store it in the script_PlayerData class' variables
        this.level = GameManager.instance.level;
        this.currency = GameManager.instance.currency;
    }
}
