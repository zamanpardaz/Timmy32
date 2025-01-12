using Timmy32.Models;

namespace Timmy32
{
    public class User
    {
        public long Id { get; set; }
        public int Privilege { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int Style { get; set; }
        public UserBio Bio { get; set; }
    }
}
