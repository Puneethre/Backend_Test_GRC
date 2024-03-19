using AutoMapper;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using System.Net;

namespace GRCServices.Services
{
    public class AssignmentMasterService : IAssignmentMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        //private readonly ILogger<CustomerService> _logger; ILogger<CustomerService> logger
        public AssignmentMasterService(GRCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> AddAssignment(AddAssignmentMasterDto newAssignmentMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //var count = _context.ClientRoleMasters.Where(c => c.RoleName == newRoleMasterdto.RoleName).Count();
                    //if (count != 0)
                    //{
                    //    Exception e = new Exception("Role Name Sholud be Unique");
                    //    throw new Exception("", e);
                    //}

                    //-------Update Customer Main Table-------
                    AssignmentMaster newAssignment = _mapper.Map<AssignmentMaster>(newAssignmentMasterdto);

                    svcResponse.Data = new List<GetAssignmentMasterDto>();
                    svcResponse.Errors = Array.Empty<string>();

                    int nRecs = _context.AssignmentMasters.Count();
                    if (nRecs > 0)
                        newAssignment.Id = _context.AssignmentMasters.Max(c => c.Id) + 1;
                    else
                        newAssignment.Id = 1;   //Start with 1.

                    _context.AssignmentMasters.Add(newAssignment);
                    await _context.SaveChangesAsync();

                    svcResponse.Message = "Assignment Created Successfully...!";
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = "Error Occured While Adding Role..";
                    svcResponse.Errors = new String[] { ex.InnerException.Message };
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> GetAllAssignments()
        {
            var svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            try
            {
                List<GetAssignmentMasterDto> returnlist = new List<GetAssignmentMasterDto>();

                var custlist = _context.AssignmentMasters
                      //.Where(c => c.Active == 'Y' && c.RoleTypeId != 0)
                      .OrderByDescending(c => c.Id)
                      .ToList();

                foreach (var recs in custlist)
                {
                    GetAssignmentMasterDto dto = new GetAssignmentMasterDto();
                    dto.Id = recs.Id;
                    dto.ActivityName = _context.ActivityMasters.Where(a => a.Id == recs.ActivityId).Select(a => a.ActivityName).FirstOrDefault();
                    dto.User = _context.ClientUserInfos.Where(c => c.Id == recs.UserId).Select(c => c.Name).FirstOrDefault();
                    dto.StartDate = recs.StartDate;
                    dto.EndDate = recs.EndDate;
                    dto.Doerstatus = recs.Doerstatus;
                    dto.AuditCheck = recs.AuditCheck;
                    dto.ApprovalStatus = recs.ApprovalStatus;
                    dto.ApprovalDate = recs.ApprovalDate;
                    dto.Approver = _context.ClientUserInfos.Where(c => c.Id == recs.ApproverId).Select(c => c.Name).FirstOrDefault();
                    dto.DoerComments = recs.DoerComments;
                    dto.ApproverComments = recs.ApproverComments;
                    dto.EvidenceDetails = recs.EvidenceDetails;
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

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> DeleteAssignment(int id)
        {
            ServiceResponse<List<GetAssignmentMasterDto>> svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            try
            {
                AssignmentMaster? tobedeletedcf = _context.AssignmentMasters
                       .FirstOrDefault(c => c.Id == id);

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Data = new List<GetAssignmentMasterDto>();

                if (tobedeletedcf != null)
                {
                    //----Deleting in Assignment Table--------
                    _context.AssignmentMasters.Remove(tobedeletedcf);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    svcResponse.Message = "Record not found!";
                }

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Message = "Assignment Deleted Successfully...!";
            }
            catch (Exception ex)
            {
                svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                svcResponse.Message = ex.Message;
            }
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetAssignmentMasterDto>>> UpdateAssignment(UpdateAssignmentMasterDto updatedAssignment, int AssignmentId)
        {
            //Log.Information("Entering UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            ServiceResponse<List<GetAssignmentMasterDto>> svcResponse = new ServiceResponse<List<GetAssignmentMasterDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    svcResponse.Data = new List<GetAssignmentMasterDto>();
                    svcResponse.Errors = new String[] { };

                    var tobeupdated = _context.AssignmentMasters
                        .FirstOrDefault(c => c.Id == AssignmentId);

                    if (tobeupdated != null)
                    {
                        // --- Update Address Table ---- 
                        tobeupdated.ActivityId = updatedAssignment.ActivityId;
                        tobeupdated.UserId = updatedAssignment.UserId;
                        tobeupdated.StartDate = updatedAssignment.StartDate;
                        tobeupdated.EndDate = updatedAssignment.EndDate;
                        tobeupdated.Doerstatus = updatedAssignment.Doerstatus;
                        tobeupdated.AuditCheck = updatedAssignment.AuditCheck;
                        tobeupdated.ApprovalStatus = updatedAssignment.ApprovalStatus;
                        tobeupdated.ApprovalDate = updatedAssignment.ApprovalDate;
                        tobeupdated.ApproverId = updatedAssignment.ApproverId;
                        tobeupdated.DoerComments = updatedAssignment.DoerComments;
                        tobeupdated.ApproverComments = updatedAssignment.ApproverComments;
                        tobeupdated.EvidenceDetails = updatedAssignment.EvidenceDetails;

                        _context.AssignmentMasters.Update(tobeupdated);
                        await _context.SaveChangesAsync();

                        svcResponse.Message = "Assignment Updated Successfully...!";
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
                    svcResponse.Message = "Error Occured While Updating Customer...";
                    svcResponse.Errors = new string[] { ex.InnerException.Message };
                    //Log.Error("In Exception: UpdateUserService-", ex.Message);
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            //Log.Information("Exiting UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            return svcResponse;
        }
    }
}
