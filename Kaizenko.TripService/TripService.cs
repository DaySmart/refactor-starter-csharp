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

        /// <summary>
        /// Gets Trips for the User if the Logged In User is a friend of the User
        /// </summary>
        /// <param name="user">User to get Trips for</param>
        /// <param name="loggedInUser">Currently logged in user</param>
        /// <returns>A List of Trips for the user if the user and loggedInUser are friends</returns>
        public List<Trip> GetTripsByUser(User user, User loggedInUser)
        {
            if (loggedInUser == null)
                throw new UserNotLoggedInException();
            if (user.IsFriend(loggedInUser))
                return _tripDao.GetTripsByUser(user);
            return new List<Trip>();
        }
    }
}
