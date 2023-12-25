using Dapper;
using Cafetown.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        #region Field
        private readonly IConnectionDL _connectionDL;
        #endregion

        #region Constructor
        public EmployeeDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }
        #endregion


        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        public Employee login(string username, string password)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.LOGIN, typeof(Employee).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$Username", username);
            parameters.Add("$Password", password);

            Employee employee = new Employee();

            // Khởi tạo kết nối đến DB
            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                employee = _connectionDL.QueryFirstOrDefault<Employee>(connection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return employee;
        }

        public int VoteAndEncryptSignature(Guid employeeID, string privateKey, string signature)
        {
            return 9;
        }
    }
}
