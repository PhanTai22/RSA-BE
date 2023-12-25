using System.Diagnostics;

namespace Cafetown.Common
{

    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho dev
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public object? MoreInfo { get; set; }

        /// <summary>
        /// TraceID
        /// </summary>
        public string? TraceID { get; set; }

        #region Constructor
        public ErrorResult()
        {
            ErrorCode = default;
        }

        public ErrorResult(ErrorCode errorCode, string devMsg, string userMsg, object? moreInfo, string? traceID = null)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceID = traceID;
        }
        #endregion
    }
}
