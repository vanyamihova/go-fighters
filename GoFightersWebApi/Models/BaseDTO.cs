using System;
namespace GoFightersWebApi.Models
{
    public class BaseDTO
    {
        public string Status { get; set; }
        public Object Data { get; set; }

        public BaseDTO(string status, Object data) {
            this.Status = status;
            this.Data = data;
        }
    }
}
