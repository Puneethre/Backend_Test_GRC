using AutoMapper;
using GRCServices.Data;
using GRCServices.Dto_s;
using GRCServices.Interfaces;
using GRCServices.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GRCServices.Services
{
    public class CustomerMasterService : ICustomerMaster
    {
        private readonly IMapper _mapper;
        private readonly GRCDbContext _context;
        //private readonly ILogger<CustomerService> _logger; ILogger<CustomerService> logger
        public CustomerMasterService(GRCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> AddCustomer(AddCustomerMasterDto newCustomerMasterdto)
        {
            var svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var count = _context.SysCustomerInfos.Where(c => c.CustomerName == newCustomerMasterdto.CustomerName).Count();
                    if (count != 0)
                    {
                        Exception e = new Exception("Customer Name Sholud be Unique");
                        throw new Exception("", e);
                    }

                    //-------Update Customer Main Table-------
                    SysCustomerInfo newCustomer = _mapper.Map<SysCustomerInfo>(newCustomerMasterdto);

                    svcResponse.Data = new List<GetCustomerMasterDto>();
                    svcResponse.Errors = Array.Empty<string>();

                    int nRecs = _context.SysCustomerInfos.Count();
                    if (nRecs > 0)
                        newCustomer.Id = _context.SysCustomerInfos.Max(c => c.Id) + 1;
                    else
                        newCustomer.Id = 1;   //Start with 1.


                    newCustomer.DbMapString = $"Server=192.168.29.128;Port=5432;Database={newCustomerMasterdto.CustomerName.Replace(" ", "").ToLower()};Username=GRC;Password=Welcome@0668;Include Error Detail=true";
                    _context.SysCustomerInfos.Add(newCustomer);
                    await _context.SaveChangesAsync();

                    var dbcontext = new GRCDbMasterContext();

                    var script = GenerateMigrationScript(dbcontext);

                    //script = script.Replace("CREATE DATABASE current_database", $"CREATE DATABASE {newCustomerMasterdto.CustomerName.Replace(" ", "").ToLower()}");

                    CreateDatabase(_context.Database.GetConnectionString(), newCustomerMasterdto.CustomerName.Replace(" ", "").ToLower());

                    ExecuteScript(script, newCustomer.DbMapString);

                    svcResponse.Message = "Customer Created Successfully...!";
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    svcResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    svcResponse.Message = "Error Occured While Adding Customer..";
                    svcResponse.Errors = new String[] { ex.InnerException.Message };
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return svcResponse;
        }


        static void CreateDatabase(string serverConnectionString, string newDatabaseName)
        {
            try
            {
                // Connect to the PostgreSQL server (without specifying a database name)
                using (var connection = new NpgsqlConnection(serverConnectionString))
                {
                    connection.Open();

                    // Create the new database
                    using (var command = new NpgsqlCommand($"CREATE DATABASE {newDatabaseName}", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("New database created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the new database: " + ex.Message);
            }
        }

        static string GenerateMigrationScript(DbContext dbContext)
        {
            return dbContext.Database.GenerateCreateScript();
        }

        static void ExecuteScript(string script, string connectionString)
        {
            try
            {
                // Connect to the PostgreSQL server
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute the modified script on the new database
                    using (var command = new NpgsqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Schema replicated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> GetAllCustomers()
        {
            var svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            try
            {
                List<GetCustomerMasterDto> returnlist = new List<GetCustomerMasterDto>();

                var custlist = _context.SysCustomerInfos
                      .OrderByDescending(c => c.Id)
                      .ToList();

                foreach (var recs in custlist)
                {
                    GetCustomerMasterDto dto = new GetCustomerMasterDto();
                    dto.Id = recs.Id;
                    dto.CustomerName = recs.CustomerName;
                    dto.Address = recs.Address;
                    dto.Description = recs.Description;
                    dto.City = recs.City;
                    dto.State = recs.State;
                    dto.Country = recs.Country;
                    dto.ContactName = recs.ContactName;
                    dto.ContactPhone = recs.ContactPhone;
                    dto.ContactEmail = recs.ContactEmail;
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

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> UpdateCustomer(UpdateCustomerMasterDto updatedCustomer,int Customerid)
        {
            //Log.Information("Entering UpdateUserService", DateTime.UtcNow.ToLongTimeString());
            ServiceResponse<List<GetCustomerMasterDto>> svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    svcResponse.Data = new List<GetCustomerMasterDto>();
                    svcResponse.Errors = new String[] { };

                    var tobeupdated = _context.SysCustomerInfos
                        .FirstOrDefault(c => c.Id == Customerid);

                    if (tobeupdated != null)
                    {
                        // --- Update Address Table ---- 
                       //tobeupdated.CustomerName = updatedCustomer.CustomerName;
                        tobeupdated.Description = updatedCustomer.Description;
                        tobeupdated.Address = updatedCustomer.Address;
                        tobeupdated.State = updatedCustomer.State;
                        tobeupdated.Country = updatedCustomer.Country;
                        tobeupdated.ContactName = updatedCustomer.ContactName;
                        tobeupdated.ContactPhone = updatedCustomer.ContactPhone;
                        tobeupdated.ContactEmail = updatedCustomer.ContactEmail;
                        tobeupdated.Description = updatedCustomer.Description;

                        _context.SysCustomerInfos.Update(tobeupdated);
                        await _context.SaveChangesAsync();

                        svcResponse.Message = "Customer Updated Successfully...!";
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
                    //Log.Error("In Exception: UpdateCustomerService-", ex.Message);
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            //Log.Information("Exiting UpdateCustomerService", DateTime.UtcNow.ToLongTimeString());
            return svcResponse;
        }

        public async Task<ServiceResponse<List<GetCustomerMasterDto>>> DeleteCustomer(int id)
        {
            ServiceResponse<List<GetCustomerMasterDto>> svcResponse = new ServiceResponse<List<GetCustomerMasterDto>>();
            try
            {
                SysCustomerInfo? tobedeletedcf = _context.SysCustomerInfos
                       .FirstOrDefault(c => c.Id == id);

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Data = new List<GetCustomerMasterDto>();

                if (tobedeletedcf != null)
                {
                    //----Deleting in Queue Table--------
                    _context.SysCustomerInfos.Remove(tobedeletedcf);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    svcResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    svcResponse.Message = "Record not found!";
                }

                svcResponse.Errors = Array.Empty<string>();
                svcResponse.Message = "Customer Deleted Successfully...!";
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
