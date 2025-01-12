using System.Collections.Generic;

namespace Timmy32.Models
{
    public class UserBio
    {
        public UserBio()
        {
            FingerIndexes = new List<int>();
        }
        public List<int> FingerIndexes { get; set; }
        public bool HasFace { get; set; }
    }
}