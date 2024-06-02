namespace NhaKhoaQuangVu.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IEndpointConventionBuilder MapEmployeeDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapControllerRoute(
            name: "employeeArea",
            pattern: "{area:exists}/{controller=LichHen}/{action=Index}/{id?}"
        );
    }
}