using BAL.Auth.AuthService;
using BAL.Services.Common;
using BAL.Services.ContentManagementSystem.ContentManagementSystem;
using BAL.Services.ContentManagementSystem.ContentMessageManagementSystem;
using BAL.Services.Customization.DashboardSkinService;
using BAL.Services.Dashboard;
using BAL.Services.EmployeeOperations.EmployeeAwardService;
using BAL.Services.EmployeeOperations.EmployeeBirthday;
using BAL.Services.EmployeeOperations.EmployeeExtensionService;
using BAL.Services.EmployeeOperations.EmployeePrintCV;
using BAL.Services.EmployeeOperations.EmployeePrintCV.Utility;
using BAL.Services.EmployeeOperations.EmployeePromotionService;
using BAL.Services.Image;
using BAL.Services.Master.BloodGroupService;
using BAL.Services.Master.BoardsService;
using BAL.Services.Master.BranchOfficeService;
using BAL.Services.Master.CasteService;
using BAL.Services.Master.Common;
using BAL.Services.Master.CompanyService;
using BAL.Services.Master.Course;
using BAL.Services.Master.DesignationService;
using BAL.Services.Master.DistrictService;
using BAL.Services.Master.DivisionService;
using BAL.Services.Master.ExitTypeService;
using BAL.Services.Master.GenderService;
using BAL.Services.Master.IdentificationTypeServices;
using BAL.Services.Master.MaritalStatusService;
using BAL.Services.Master.MasterServices.AppointmentStatus;
using BAL.Services.Master.MasterServices.Community;
using BAL.Services.Master.MasterServices.Qualification;
using BAL.Services.Master.MasterServices.ResidentialStatus;
using BAL.Services.Master.Menu;
using BAL.Services.Master.ModeOfTrainingService;
using BAL.Services.Master.NationalityService;
using BAL.Services.Master.PlaceOfPostingService;
using BAL.Services.Master.ReligionService;
using BAL.Services.Master.SpecializationService;
using BAL.Services.Master.State;
using BAL.Services.Master.StreamService;
using BAL.Services.Master.TrainingTypeService;
using BAL.Services.Reports.EmployeeDetailsReportService;
using BAL.Services.RolesCompanyPermission;
using Common.Configuration;
using DinkToPdf;
using DinkToPdf.Contracts;
using DTO.Models.Master;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using BAL.Services.Dashboard;
using BAL.Services.ContentManagementSystem.ContentManagementSystem;
using BAL.Services.ContentManagementSystem.ContentMessageManagementSystem;
using BAL.Services.BirthdayWish;
using BAL.Services.EmployeeOperations.Employee_AppointmentStatusService;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<bIAuthService, bAuthService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRolesCompanyPermissionService, RolesCompanyPermissionService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IEmployeeDetailsReportService, EmployeeDetailsReportService>();
            //** master controllers **//
            services.AddScoped<ICasteService, CasteService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IMaritalStatus, MaritalStatus>();
            services.AddScoped<IReligion, Religion>();
            services.AddScoped<ISpecialization, Specialization>();
            services.AddScoped<IModeOfTraining, ModeOfTraining>();
            services.AddScoped<IBoardsService, BoardsService>();
            services.AddScoped<IMaritalStatus, MaritalStatus>();
            services.AddScoped<IReligion, Religion>();
            services.AddScoped<IEmployeeBirthdayService, EmployeeBirthdayService>();
            services.AddScoped<IEmployee_AppointmentStatusService, Employee_AppointmentStatusService>();


            /* Added By : Pranai Giri */
            services.AddScoped<DatabaseLockConfiguration>();
            services.AddScoped<IMasterCommonService, MasterCommonService>();
            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<Services.Master.MasterServices.AppointmentStatus.IAppointmentStatusService, Services.Master.MasterServices.AppointmentStatus.AppointmentStatusService>();
            services.AddScoped<IResidentialStatusService, ResidentialStatusService>();
            services.AddScoped<IQualificationService, QualificationService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IEmployeePrintCVService, EmployeePrintCVService>();
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


            /* Added By : Mukesh Shah */
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ITrainingTypeService, TrainingTypeService>();
            services.AddScoped<IBloodGroupService, BloodGroupService>();
            services.AddScoped<INationalityService, NationalityService>();
            services.AddScoped<IDivisionService, DivisionService>();
            services.AddScoped<IStreamService, StreamService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IDashboardSkinService, DashboardSkinService>();
            services.AddScoped<IPlaceOfPostingService, PlaceOfPostingService>();
            services.AddScoped<IEmployeeAwardService, EmployeeAwardService>();
            services.AddScoped<Services.EmployeeOperations.EmployeePromotionService.IEmployeePromotionService, Services.EmployeeOperations.EmployeePromotionService.EmployeePromotionService>();
            services.AddScoped<IEmployeeExtension_TerminationService, EmployeeExtension_TerminationService>();
            services.AddScoped<IContentManagementSystemService, ContentManagementSystemService>();
            services.AddScoped<IContentMessageManagementSystemService, ContentMessageManagementSystemService>();

            /* Added By : Sandeep Chauhan */
            services.AddScoped<ICasteService, CasteService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IIdentificationTypeService, IdentificationTypeService>();
            services.AddScoped<IExitTypeService, ExitTypeService>();
            services.AddScoped<IInServiceTrainingService, InServiceTrainingService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IDashboardService, DashboardService>();


            return services;
        }
    }
}
