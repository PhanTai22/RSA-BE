using Cafetown.BL;
using Cafetown.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field
        private readonly IEmployeeBL _employeeBL;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        [HttpGet("login")]
        public IActionResult login([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                var employee = _employeeBL.login(username, password);

                if (employee != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employee);
                } 
                else 
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.InvalidInput
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception
                });
            }
        }

        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        [HttpGet("sign")]
        public IActionResult sign([FromQuery] Guid employeeID, [FromQuery] string signature)
        {
            try
            {
                var success = _employeeBL.VoteAndEncryptSignature(employeeID, signature);

                if (success != null)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.InvalidInput
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception
                });
            }
        }
        #endregion
    }
}
