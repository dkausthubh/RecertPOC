namespace   HackathonProject.Services
{
    public interface IDatabricksService
    {
        Task<List<recert_users>> GetusersAsync();
        Task<List<recert_applications>> GetapplicationsAsync();
        Task<List<recert_user_access>> Getuser_accessAsync();
        Task<List<recert_requests>> GetrequestsAsync();
    }
}
