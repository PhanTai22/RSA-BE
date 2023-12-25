using Cafetown.Common;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using Aspose.Cells;
using System.IO;
using System.Data;
using Aspose.Cells.Drawing.Texts;

namespace Cafetown.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field
        private readonly IEmployeeDL _employeeDL;
        #endregion

        #region Constructor
        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeeID"></param>
        /// <returns>bool kiểm tra có trùng hay không</returns>
        /// Modified by: TTTuan 5/1/2023
        public override ServiceResponse CheckDuplicateCode(Guid? employeeID, Employee employee)
        {
            var duplicateCode = _employeeDL.CheckDuplicateCode(employeeID, employee.EmployeeCode);

            if (duplicateCode == true)
            {
                return new ServiceResponse
                {
                    StatusResponse = StatusResponse.DuplicateCode,
                    Data = new ErrorResult()
                    {
                        ErrorCode = ErrorCode.DuplicateCode
                    }
                };
            }

            return new ServiceResponse { StatusResponse = StatusResponse.Done };
        }

        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        public Employee login(string username, string password)
        {
            return _employeeDL.login(username, password);
        }

        public int VoteAndEncryptSignature(Guid employeeID, string signature)
        {
            return 0;
        }
        #endregion
    }
}
