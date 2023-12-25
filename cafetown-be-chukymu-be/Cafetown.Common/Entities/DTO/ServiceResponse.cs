using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class ServiceResponse
    {
        /// <summary>
        /// Thành công hoặc thất bại
        /// </summary>
        public StatusResponse StatusResponse { get; set; }

        /// <summary>
        /// Data response khi thành công hoặc thất bại
        /// </summary>
        public ErrorResult? Data { get; set; }
    }
}
