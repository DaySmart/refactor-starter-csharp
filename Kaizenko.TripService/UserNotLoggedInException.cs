using System;

namespace Kaizenko.TripService
{
    [Serializable]
    public class UserNotLoggedInException : System.Exception
    {
        public override string ToString()
        {
            return nameof(UserNotLoggedInException);
        }
    }
}
