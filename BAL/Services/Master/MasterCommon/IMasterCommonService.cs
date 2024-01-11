using DTO.Models;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.Common
{
    public interface IMasterCommonService
    {
        /// <summary>
        /// SELECT : Pass Stored Procedure Name to return master details which uses MasterDTO Model
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <author>Pranai Giri</author>
        /// <date>2023-11-02</date>
        public DataTable Get(string spName);

        /// <summary>
        /// SELECT : Pass Query to return master details which uses MasterDTO Model
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <author>Pranai Giri</author>
        /// <date>2023-11-02</date>
        public DataTable GetDataTableByQuery(string query);

        /// <summary>
        /// INSERT/UPDATE/ARCHIVE : Pass Stored Procedure Name and your Master Data which uses MasterDTO
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <author>Pranai Giri</author>
        /// <date>2023-11-02</date>
        public DataResponse PostOrUpdate(string spName, string? initialName, dynamic item, bool isUpdate);


        /// <summary>
        /// INSERT/UPDATE/ARCHIVE : Pass Stored Procedure Name and your Master Data which uses MasterDTO
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <author>Pranai Giri</author>
        /// <date>2023-11-02</date>
        public DataResponse Archive(string spName, dynamic item);


        /// <summary>
        /// DELETE : Pass Stored Procedure Name and Table id
        /// </summary>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="id">Table id</param>
        /// <author>Sandeep Chauhan</author>
        /// <date>09-11-2023</date>
        Task<DataResponse> Delete(string spName, string columnNameInitial, long id);

    }
}
