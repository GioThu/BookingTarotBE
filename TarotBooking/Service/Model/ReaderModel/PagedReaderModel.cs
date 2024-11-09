using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.ReaderModel
{
    public class PagedReaderModel
    {
        public List<ReaderInfor> Readers { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
