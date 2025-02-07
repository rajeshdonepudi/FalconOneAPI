namespace FalconOne.AI.Models.Users
{
    public class UserLoginData
    {
        public float AccessFailedCount { get; set; }
        public float SuccessfulLoginCount { get; set; }
        public float LastLoginAttemptDateNumeric { get; set; }
    }

    public class UserLoginAttemptData
    {
        public float TimeSinceLastLogin { get; set; }
        public float NumberOfFailedAttempts { get; set; }
        public float DayOfWeek { get; set; }
        public bool IsSucessfull { get; set; }
    }
}
