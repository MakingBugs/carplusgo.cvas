namespace CarPlusGo.CVAS.Mobile.TransportCapacity.OrderMap.Dto
{
    public class HeatDataDto
    {
        public int[][] MapData { get; set; }

        public int[] RangeList { get; set; }

        public double[] LngExtent { get; set; }

        public double[] LatExtent { get; set; }

        public int[] CellCount { get; set; }
    }
}
