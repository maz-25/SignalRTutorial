namespace SignalRSample
{
    //static details for race voting, on je to napravio zato što želi spremit glasove kao konstante u ovu klasu
    //glasanje preko urla, spremljene u dictionary
    public static class SD
    {
        static SD()
        {
            DealthyHallowRace = new Dictionary<string, int>();
            DealthyHallowRace.Add(Cloak, 0);
            DealthyHallowRace.Add(Stone, 0);
            DealthyHallowRace.Add(Wand, 0);
        }

        public const string Wand = "wand";
        public const string Stone = "stone";
        public const string Cloak = "cloak";

        public static Dictionary<string, int> DealthyHallowRace;


    }
}
