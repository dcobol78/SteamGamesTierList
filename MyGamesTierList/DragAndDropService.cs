namespace MyGamesTierList
{
    public class DragAndDropService
    {
        public Game Data { get; set; }
        public string Zone { get; set; }
        
        public Tier TierData { get; set; }

        public int DropZoneCounter { get; set; }

        public void StartDrag(Game data, string zone, Tier tier)
        {
            DropZoneCounter = 0;
            this.TierData = tier;
            this.Data = data;
            this.Zone = zone;
        }

        public bool Accepts(string zone)
        {
            return this.Zone == zone;
        }
    }
}
