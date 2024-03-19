using AutoMapper;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.AccessControl;

namespace GRCServices.Services
{
    public class ActivityMasterService : IActivityMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        //private readonly ILogger<ActivityMasterService> _logger;, ILogger<ActivityMasterService> logger
        public ActivityMasterService(GRCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> AddActivity(AddActivityMasterDto newActivityMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //var count = _context.ActivityMasters.Where(c => c.RoleName == newActivityMasterdto.RoleName).Count();
                    //if (count != 0)
                    //{
                    //    Exception e = new Exception("Role Name Sholud be Unique");
                    //    throw new Exception("", e);
                    //}

                    //-------Update Customer Main Table-------
                    ActivityMaster newActivity = _mapper.Map<ActivityMaster>(newActivityMasterdto);

                    svcResponse.Data = new List<GetActivityMasterDto>();
                    svcResponse.Errors = Array.Empty<string>();

                    int nRecs = _context.ActivityMasters.Count();
                    if (nRecs > 0)
                        newActivity.Id = _context.ActivityMasters.Max(c => c.Id) + 1;
                    else
                        newActivity.Id = 1;   //Start with 1.

                    _context.ActivityMasters.Add(newActivity);
                    await _context.SaveChangesAsync();

                    svcResponse.Message = "Activity Created Successfully...!";
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = "Error Occured While Adding Activity..";
                    svcResponse.Errors = new String[] { ex.InnerException.Message };
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> GetAllActivites()
        {
            var svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            try
            {
                List<GetActivityMasterDto> returnlist = new List<GetActivityMasterDto>();

                var Activtieslist = _context.ActivityMasters
                      //.Where(c => c.Active == 'Y')
                      .ToList();

                foreach (var recs in Activtieslist)
                {
                    GetActivityMasterDto dto = new GetActivityMasterDto();
                    dto.Id = recs.Id;
                    dto.ActivityName = recs.ActivityName;
                    dto.ActivityDescr = recs.ActivityDescr;
                    dto.DoerRole = recs.DoerRole;
                    dto.Frequency = recs.Frequency;
                    dto.Duration = recs.Duration;
                    dto.RefDocument = recs.RefDocument;
                    dto.OutputDocument = recs.OutputDocument;
                    dto.TriggeringActivity = recs.TriggeringActivity;
                    dto.ApproverRole = recs.ApproverRole;
                    dto.Auditable = recs.Auditable;
                    dto.HelpRef = recs.HelpRef;
                    returnlist.Add(dto);
                }
                svcResponse.Errors = new string[] { };
                svcResponse.Data = returnlist;
            }
            catch (Exception ex)
            {
                svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                svcResponse.Errors = new string[] { ex.InnerException.Message };
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> UpdateActivity(UpdateActivityMasterDto updatedActivity, int ActivityId)
        {
            ServiceResponse<List<GetActivityMasterDto>> svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    svcResponse.Data = new List<GetActivityMasterDto>();
                    svcResponse.Errors = new String[] { };

                    var tobeupdated = _context.ActivityMasters
                        .FirstOrDefault(c => c.Id == ActivityId);

                    if (tobeupdated != null)
                    {
                        // --- Update Address Table ---- 
                        tobeupdated.ActivityName = updatedActivity.ActivityName;
                        tobeupdated.ActivityDescr = updatedActivity.ActivityDescr;
                        tobeupdated.Frequency = updatedActivity.Frequency;
                        tobeupdated.Duration = updatedActivity.Duration;
                        tobeupdated.DoerRole = updatedActivity.DoerRole;
                        tobeupdated.RefDocument = updatedActivity.RefDocument;
                        tobeupdated.OutputDocument = updatedActivity.OutputDocument;
                        tobeupdated.TriggeringActivity = updatedActivity.TriggeringActivity;
                        tobeupdated.ApproverRole = updatedActivity.ApproverRole;
                        tobeupdated.Auditable = updatedActivity.Auditable;
                        tobeupdated.HelpRef = updatedActivity.HelpRef;

                        _context.ActivityMasters.Update(tobeupdated);
                        await _context.SaveChangesAsync();

                        svcResponse.Message = "Activity Updated Successfully...!";
                    }
                    else
                    {
                        svcResponse.Message = "You are Sent Null Values Please enter data";
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = "Error Occured While Updating Activity...";
                    svcResponse.Errors = new string[] { ex.InnerException.Message };
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetActivityMasterDto>>> DeleteActivity(int id)
        {
            ServiceResponse<List<GetActivityMasterDto>> svcResponse = new ServiceResponse<List<GetActivityMasterDto>>();
            try
            {
                ActivityMaster? tobedeletedcf = _context.ActivityMasters
                       .FirstOrDefault(c => c.Id == id);

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Data = new List<GetActivityMasterDto>();

                if (tobedeletedcf != null)
                {
                    //----Deleting in Queue Table--------
                    _context.ActivityMasters.Remove(tobedeletedcf);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    svcResponse.Message = "Record not found!";
                }

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Message = "Activity Deleted Successfully...!";
            }
            catch (Exception ex)
            {
                svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                svcResponse.Message = ex.Message;
            }
            return svcResponse;
        }
    }
}
