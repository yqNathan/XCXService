using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDD.Common
{
    /// <summary>
    /// 表示结果
    /// </summary>
    public class ResultEntity
    {
        public ResultEntity()
        {

        }
        public ResultEntity(ResultStatus Status, string Message, object Data)
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
        }
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    /// <summary>
    /// 结果-泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultEntity<T>
    {
        public ResultEntity()
        {

        }

        public ResultEntity(ResultStatus Status, string Message, T Data)
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
        }
        public ResultEntity(ResultStatus Status, string Message)
        {
            this.Status = Status;
            this.Message = Message;
        }
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    /// <summary>
    /// 表示返回状态
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 同步了，前端需做业务处理
        /// </summary>
        IsSync = 10001,
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 0,
        /// <summary>
        /// 错误
        /// </summary>
        Error = -1,
        /// <summary>
        /// 无权限
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// 等待
        /// </summary>
        Wait = 1010,
        /// <summary>
        /// 缺少必要参数
        /// </summary>
        Lack_of_Parameter = 1000,
        /// <summary>
        /// 未达标准
        /// </summary>
        NOT_STANDARD = -1002
    }
}
