using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class Employee : BaseEntity
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [PrimaryKey]
        public Guid? EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Mã nhân viên không được để trống")]
        [MaxLength("Mã nhân viên phải nhỏ hơn hoặc bằng 20 ký tự", 20)]
        [ExcelColumnName("Mã nhân viên")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Tên nhân viên không được để trống")]
        [MaxLength("Tên nhân viên phải nhỏ hơn hoặc bằng 100 ký tự", 100)]
        [ExcelColumnName("Tên nhân viên")]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [ExcelColumnName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [ExcelColumnName("Giới tính")]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số CCCD
        /// </summary>

        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CCCD
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CCCD
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ nhà
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [Regex("Số điện thoại không hợp lệ", @"^\d{10}$")]
        public string? Phone { get; set; }

        /// <summary>
        /// Địa chỉ Email
        /// </summary>
        [Regex("Email không đúng định dạng", @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$")]
        public string? Email { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string? Password { get; set; }

        public int voteStatus { get; set; } = 0;

        public string? privateKey { get; set; }

        public string? signature { get; set; }
    }
}
