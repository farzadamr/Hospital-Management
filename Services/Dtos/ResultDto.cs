using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    public class ResultDto
    {
        public ResultDto(bool isSuccess,string? Message)
        {
            this.iSuccess = isSuccess;
            this.Message = Message;
        }
        public bool iSuccess { get; private set; }
        public string? Message { get; private set; }
    }
    public class ResultDto<TData>
    {
        public ResultDto(TData Data,bool isSuccess,string? Message)
        {
            this.Data = Data;
            this.iSuccess = isSuccess;
            this.Message = Message;
        }
        public TData Data { get; private set; }
        public bool iSuccess { get; private set; }
        public string? Message { get; private set; }
    }
}
