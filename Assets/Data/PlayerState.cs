public class PlayerState
{
    private static PlayerState instance = new PlayerState();

    public static PlayerState Instance
    {
        get { return instance; }
    }

    private PlayerState()
    {

    }

    public string Name { get; set; }

    public string CurrentQuestId { get; set; }
}

