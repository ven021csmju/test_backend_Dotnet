using System.Threading.Tasks;
using MyApi.DTOs;
using MyApi.Repositories;
using System.Linq;

namespace MyApi.Services;

public interface IDashboardService
{
    Task<DashboardSummaryResponse> GetSummaryAsync();
}

public class DashboardService : IDashboardService
{
    private readonly IEventRepository _eventRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly ICheckinRepository _checkinRepo;

    public DashboardService(IEventRepository eventRepo, IEmployeeRepository employeeRepo, ICheckinRepository checkinRepo)
    {
        _eventRepo = eventRepo;
        _employeeRepo = employeeRepo;
        _checkinRepo = checkinRepo;
    }

    public async Task<DashboardSummaryResponse> GetSummaryAsync()
    {
        var events = await _eventRepo.GetAllAsync();
        var employees = await _employeeRepo.GetAllAsync();
        var checkins = await _checkinRepo.GetAllAsync();

        var totalEvents = events.Count();
        var totalEmployees = employees.Count();
        var totalCheckins = checkins.Count();

        return new DashboardSummaryResponse
        {
            TotalEvents = totalEvents,
            TotalEmployees = totalEmployees,
            TotalCheckins = totalCheckins,
            AttendanceRate = totalEvents > 0 && totalEmployees > 0 ? (double)totalCheckins / (totalEvents * totalEmployees) * 100 : 0
        };
    }
}
