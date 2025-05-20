using System.Buffers.Text;

namespace OneBeyondApi.Model
{
    public class Fine
    {
        public Guid Id { get; set; }
        public float Price { get; set; }
        public DateTime FineDate { get; set; }
        public bool Outstanding { get; set; }
        public bool? FineRevoked { get; set; }

    }
}
