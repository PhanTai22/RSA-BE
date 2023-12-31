﻿using Cafetown.Common;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private readonly IBaseDL<T> _baseDL;
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="record"></param>
        /// <param name="recordID"></param>
        /// <returns>bool kiểm tra có trùng hay không</returns>
        /// Modified by: TTTuan 5/1/2023
        public virtual ServiceResponse CheckDuplicateCode(Guid? recordID, T record)
        {
            return new ServiceResponse { StatusResponse = StatusResponse.Done };
        }
        #endregion

        #region Method
        /// <summary>
        /// Xoá 1 bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int DeleteRecordByID(Guid recordID)
        {
            return _baseDL.DeleteRecordByID(recordID);
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int DeleteRecordsByIDs(string recordIDs)
        {
            return _baseDL.DeleteRecordsByIDs(recordIDs);
        }

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi trong bảng</returns>
        /// Modified by: TTTuan 5/1/2023
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        public PagingResult<T> GetRecordsByFilter(string? keyword, int filter, int pageSize, int pageNumber)
        {
            return _baseDL.GetRecordsByFilter(keyword, filter, pageSize, pageNumber);
        }

        /// <summary>
        /// API Lấy mã  mới
        /// </summary>
        /// <returns>Mã mới</returns>
        /// Modified by: TTTuan (23/12/2022)
        public string GetNewCode()
        {
            return _baseDL.GetNewCode();
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi</param>
        /// <returns>Thông tin của bản ghi theo ID</returns>
        /// Modified by: TTTuan 5/1/2023
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="newRecord"></param>
        /// <returns>ServiceResponse</returns>
        /// Modified by: TTTuan 5/1/2023
        public ServiceResponse InsertRecord(T newRecord)
        {
            // Validate
            var validateResult = new ServiceResponse
            {
                StatusResponse = StatusResponse.Done
            };

            if (validateResult.StatusResponse == StatusResponse.Done)
            {
                var checkDuplicateCode = CheckDuplicateCode(null, newRecord);

                if (checkDuplicateCode.StatusResponse == StatusResponse.Done)
                {
                    var numberOfAffectedRows = _baseDL.InsertRecord(newRecord);

                    if (numberOfAffectedRows > 0)
                    {
                        return new ServiceResponse
                        {
                            StatusResponse = StatusResponse.Done,
                        };
                    }
                    else
                    {
                        return new ServiceResponse
                        {
                            StatusResponse = StatusResponse.Failed,
                        };
                    }
                }
                else
                {
                    return checkDuplicateCode;
                }
            }
            else
            {
                return validateResult;
            }
        }

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>ServiceResponse</returns>
        /// Modified by: TTTuan 5/1/2023
        public ServiceResponse UpdateRecordByID(Guid recordID, T record)
        {
            // Validate
            var validateResult = ValidateData(record);

            if (validateResult.StatusResponse == StatusResponse.Done)
            {
                var checkDuplicateCode = CheckDuplicateCode(recordID, record);

                if (checkDuplicateCode.StatusResponse == StatusResponse.Done)
                {
                    var numberOfAffectedRows = _baseDL.UpdateRecordByID(recordID, record);

                    if (numberOfAffectedRows > 0)
                    {
                        return new ServiceResponse
                        {
                            StatusResponse = StatusResponse.Done,
                        };
                    }
                    else
                    {
                        return new ServiceResponse
                        {
                            StatusResponse = StatusResponse.Failed,
                        };
                    }
                }
                else
                {
                    return checkDuplicateCode;
                }
            }
            else
            {
                return validateResult;
            }
        }

        /// <summary>
        /// Validate dữ liệu đầu vào
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Đối tượng ServiceResponse mỗ tả thành công hay thất bại</returns>
        /// Created by: TTTuan (23/12/2022)
        public virtual ServiceResponse ValidateData(T record)
        {
            var errorMessages = new List<string>();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);

                var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    errorMessages.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                }

                var maxLengthAttribute = (MaxLengthAttribute?)Attribute.GetCustomAttribute(property, typeof(MaxLengthAttribute));
                if (maxLengthAttribute != null && propertyValue?.ToString()?.Trim().Length > maxLengthAttribute.MaxLength)
                {
                    errorMessages.Add(maxLengthAttribute.ErrorMessage);
                }

                var regexAttribute = (RegexAttribute?)Attribute.GetCustomAttribute(property, typeof(RegexAttribute));
                if (regexAttribute != null && propertyValue != null && propertyValue?.ToString()?.Trim().Length > 0)
                {
                    if (!Regex.IsMatch(propertyValue.ToString(), regexAttribute.Pattern, RegexOptions.IgnoreCase))
                        errorMessages.Add(regexAttribute.ErrorMessage);
                }
            }
            if (errorMessages.Count > 0)
            {
                return new ServiceResponse
                {
                    StatusResponse = StatusResponse.Invalid,
                    Data = new ErrorResult
                    {
                        ErrorCode = ErrorCode.InvalidInput
                    }
                };
            }

            return new ServiceResponse { StatusResponse = StatusResponse.Done };
        }
        #endregion
    }
}
