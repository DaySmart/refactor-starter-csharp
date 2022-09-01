using System.Collections.Generic;

namespace Kaizenko.TripService
{
    public class TripService
    {
        private readonly TripDAO _tripDao;

        public TripService() : this(new TripDAO())
        {

        }

        public TripService(TripDAO tripDao)
        {
            _tripDao = tripDao;
        }

        public List<Trip> GetTripsByUser(User loggedInUser, User user)
        {
            if (loggedInUser == null)
                throw new UserNotLoggedInException();
            if (user.IsFriend(loggedInUser))
                return _tripDao.FindTripsByUser(user);
            return new List<Trip>();
        }
    }
}
