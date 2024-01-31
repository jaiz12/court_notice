using DTO.Models;
using DTO.Models.Common;
using DTO.Models.EmployeeOperation;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Common
{
    public interface ICommonService
    {
        public T GetById<T>(string spName, string prmInitial, dynamic id) where T : class;
        public Task<object> GetByIdAsync<T>(string spName, string prmInitial, dynamic id) where T : class;
        public dynamic GetListById(string spName, string prmInitial, dynamic id);
        public Task<dynamic> GetDatasetByIdAsync(string spName, string prmInitial, dynamic id);
        public Task<DataSet> GetDataSetByIdAsync(string spName, string prmInitial, dynamic id, PaginationEntityDTO pagination = null);

        public List<T> GetListById<T>(string spName, string prmInitial, dynamic id) where T : class;
        public Task<List<T>> GetListByIdAsync<T>(string spName, string prmInitial, dynamic id, PaginationEntityDTO pagination = null) where T : class;
        public Task<List<T>> GetListByIdUsingSearchFilterAsync<T>(string spName, string prmInitial, dynamic id, SearchFilterDTO searchFilter = null) where T : class;

        public DataResponse PostOrUpdateEmployeeProfile(string spName, dynamic item, bool isUpdate);
        public DataResponse PostOrUpdate(string spName, dynamic item, bool isUpdate);
        public Task<DataResponse> PostOrUpdateAsync(string spName, dynamic item, bool isUpdate);
        public DataResponse DeleteById(string spName, string prmInitial, dynamic id);

        public Task<bool> DeleteFileFromDirectoryUsingFilePath(string filePath);

    }
}
